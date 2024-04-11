using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

using MongoDB.Bson;
using MongoDB.Driver;

using EduPartners.MVVM.Model;

namespace EduPartners.Core
{
    public class Database
    {
        private static string ConnectionUri = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString + "?retryWrites=true&w=majority";
        private static string BackupConnectionUri = "mongodb+srv://FBLANS2024:Zu9QiBwFOlWXC5SS@managementbackup.zsqbmhw.mongodb.net/";

        private MongoClientSettings Settings = MongoClientSettings.FromConnectionString(ConnectionUri);
        private MongoClientSettings BackupSettings = MongoClientSettings.FromConnectionString(BackupConnectionUri);

        private MongoClient Client;
        private MongoClient BackupClient;

        private IMongoDatabase MongoDatabase;
        private IMongoDatabase MongoDatabaseBackup;

        private const string UserCollection = "Users";
        private const string SchoolCollection = "Schools";
        private const string PartnerCollection = "Partners";

        public Database()
        {
            Settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            BackupSettings.ServerApi = new ServerApi(ServerApiVersion.V1);

            Client = new MongoClient(Settings);
            BackupClient = new MongoClient(BackupSettings);

            MongoDatabase = Client.GetDatabase("SchoolPartnersInfo");
            MongoDatabaseBackup = BackupClient.GetDatabase("PartnersInfoBackup");

            try
            {
                BsonDocument result = Client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                Debug.WriteLine("Pinged your deployment. You successfully connected to MongoDB MAIN Database!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Could not connect to MAIN Database");
                Debug.WriteLine(ex); 
            }    
            
            try
            {
                BsonDocument result = BackupClient.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                Debug.WriteLine("Pinged your deployment. You successfully connected to MongoDB BACKUP Database!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Could not connect to BACKUP Database");
                Debug.WriteLine(ex); 
            }

            IMongoCollection<User> users = GetDBCollection<User>(UserCollection);
            IndexKeysDefinition<User> UnquieIndex = Builders<User>.IndexKeys.Ascending(user => user.Email);
            CreateIndexOptions Options = new CreateIndexOptions() { Unique = true };
            CreateIndexModel<User> model = new CreateIndexModel<User>(UnquieIndex, Options);
            users.Indexes.CreateOne(model);
        }

        private IMongoCollection<T> GetDBCollection<T>(string Collection)
        {
            return MongoDatabase.GetCollection<T>(Collection);
        }    
        
        private IMongoCollection<T> GetDBCollectionBackup<T>(string Collection)
        {
            return MongoDatabaseBackup.GetCollection<T>(Collection);
        }

        /// <summary>
        /// This function retrieves all users.
        /// </summary>
        /// <returns>
        /// A `Task` that will return a `List<User>` containing all users.
        /// </returns>
        public async Task<List<User>> GetUsers()
        {
            IMongoCollection<User> users = GetDBCollection<User>(UserCollection);
            IAsyncCursor<User> results = await users.FindAsync(_ => true);

            return results.ToList();
        }

        /// <summary>
        /// This function retrieves a list of users by their ID.
        /// </summary>
        /// <param name="UserId">The ID of the user being retrieved.</param>
        /// <returns>
        /// Returns a `Task` that will contain a list of Users that match the specified <paramref name="UserId"/>.
        /// </returns>
        public async Task<List<User>> GetUserById(string UserId) 
        {
            IMongoCollection<User> users = GetDBCollection<User>(UserCollection);
            IAsyncCursor<User> results = await users.FindAsync(user => user.Id == UserId);

            return results.ToList();
        }

        /// <summary>
        /// This function retrieves a list of users based on the provided user name.
        /// </summary>
        /// <param name="UsersName">The name of the user being retrieved.</param>
        /// <returns>
        /// Returns a `Task` that will contain a list of Users that match the specified <paramref name="UsersName"/>.
        /// </returns>
        public async Task<List<User>> GetUserByName(string UsersName) 
        {
            IMongoCollection<User> users = GetDBCollection<User>(UserCollection);
            IAsyncCursor<User> results = await users.FindAsync(user => user.Name == UsersName);

            return results.ToList();
        }

        /// <summary>
        /// This function retrieves a list of users based on their email address.
        /// </summary>
        /// <param name="UserEmail">The email of the user being retrieved retrieve.</param>
        /// <returns>
        /// Returns a `Task` that will contain a list of Users that match the specified <paramref name="UserEmail"/>.
        /// </returns>
        public async Task<List<User>> GetUserByEmail(string UserEmail)
        {
            IMongoCollection<User> users = GetDBCollection<User>(UserCollection);
            IAsyncCursor<User> results = await users.FindAsync(user => user.Email == UserEmail);

            return results.ToList();
        }

        /// <summary>
        /// This function creates a new user.
        /// </summary>
        /// <param name="CreateUser">
        /// The method takes a parameter of type `User`, which represents the user information
        /// needed to create a new user account.</param>
        /// <returns>
        /// Returns a `Task` object.
        /// </returns>
        public Task CreateUser(User CreateUser) 
        {
            IMongoCollection<User> users = GetDBCollection<User>(UserCollection);

            IMongoCollection<User> usersBackup = GetDBCollectionBackup<User>(UserCollection);
            usersBackup.InsertOneAsync(CreateUser);

            return users.InsertOneAsync(CreateUser);
        }

        /// <summary>
        /// This function updates a user speficed in <paramref name="UpdateUser"/>
        /// </summary>
        /// <param name="UpdateUser">This parameter of type `User`, which
        /// represents a user entity in your application. It contains the data that needs to be updated for a
        /// specific user.</param>
        /// <returns>
        /// Returns a `Task` object.
        /// </returns>
        public Task UpdateUser(User UpdateUser)
        {
            IMongoCollection<User> users = GetDBCollection<User>(UserCollection);
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(u => u.Id, UpdateUser.Id);

            IMongoCollection<User> usersBackup = GetDBCollectionBackup<User>(UserCollection);
            usersBackup.ReplaceOneAsync(filter, UpdateUser, new ReplaceOptions { IsUpsert = true });

            return users.ReplaceOneAsync(filter, UpdateUser, new ReplaceOptions {IsUpsert = true});
        }

        /// <summary>
        /// This function deletes a user speficed in <paramref name="DeleteUser"/>
        /// </summary>
        /// <param name="DeleteUser">This parameter of type `User`, which
        /// represents a user entity in your application. It contains the data to delete the
        /// specific user.</param>
        /// <returns>
        /// Returns a `Task` object.
        /// </returns>
        public Task DeleteUser(User DeleteUser)
        {
            IMongoCollection<User> users = GetDBCollection<User>(UserCollection);
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(u => u.Id, DeleteUser.Id);

            IMongoCollection<User> usersBackup = GetDBCollectionBackup<User>(UserCollection);
            usersBackup.DeleteOneAsync(filter);

            return users.DeleteOneAsync(filter);
        }

        /// <summary>
        /// This function retrieves all schools.
        /// </summary>
        /// <returns>
        /// A `Task` that will return a `List<School>` containing all schools.
        /// </returns>
        public async Task<List<School>> GetSchools()
        {
            IMongoCollection<School> schools = GetDBCollection<School>(SchoolCollection);
            IAsyncCursor<School> results = await schools.FindAsync(_ => true);

            return results.ToList();
        }

        /// <summary>
        /// This function retrieves a list of schools by their ID.
        /// </summary>
        /// <param name="SchoolId">The ID of the school being retrieved.</param>
        /// <returns>
        /// Returns a `Task` that will contain a list of Schools that match the specified <paramref name="SchoolId"/>.
        /// </returns>
        public async Task<List<School>> GetSchoolById(string SchoolId) 
        {
            IMongoCollection<School> schools = GetDBCollection<School>(SchoolCollection);
            IAsyncCursor<School> results = await schools.FindAsync(school => school.Id == SchoolId);

            return results.ToList();
        }

        /// <summary>
        /// This function retrieves a list of schools based on the provided school name.
        /// </summary>
        /// <param name="SchoolName">The name of the school being retrieved.</param>
        /// <returns>
        /// Returns a `Task` that will contain a list of Schools that match the specified <paramref name="SchoolName"/>.
        /// </returns>
        public async Task<List<School>> GetSchoolByName(string SchoolName) 
        {
            IMongoCollection<School> schools = GetDBCollection<School>(SchoolCollection);
            IAsyncCursor<School> results = await schools.FindAsync(school => school.Name == SchoolName);

            return results.ToList();
        }

        /// <summary>
        /// This function creates a new school.
        /// </summary>
        /// <param name="CreateSchool">
        /// The method takes a parameter of type `School`, which represents the school information
        /// needed to create a new school.</param>
        /// <returns>
        /// Returns a `Task` object.
        /// </returns>
        public Task CreateSchool(School CreateSchool) 
        {
            IMongoCollection<School> schools = GetDBCollection<School>(SchoolCollection);

            IMongoCollection<School> schoolsBackup = GetDBCollectionBackup<School>(SchoolCollection);
            schoolsBackup.InsertOneAsync(CreateSchool);

            return schools.InsertOneAsync(CreateSchool);
        }

        /// <summary>
        /// This function updates a school speficed in <paramref name="UpdateSchool"/>
        /// </summary>
        /// <param name="UpdateSchool">This parameter of type `School`, which
        /// represents a school entity in your application. It contains the data that needs to be updated for a
        /// specific school.</param>
        /// <returns>
        /// Returns a `Task` object.
        /// </returns>
        public Task UpdateSchool(School UpdateSchool)
        {
            IMongoCollection<School> schools = GetDBCollection<School>(SchoolCollection);
            FilterDefinition<School> filter = Builders<School>.Filter.Eq(s => s.Id, UpdateSchool.Id);

            IMongoCollection<School> schoolsBackup = GetDBCollectionBackup<School>(SchoolCollection);
            schoolsBackup.ReplaceOneAsync(filter, UpdateSchool, new ReplaceOptions { IsUpsert = true });

            return schools.ReplaceOneAsync(filter, UpdateSchool, new ReplaceOptions {IsUpsert = true});
        }

        /// <summary>
        /// This function deletes a school speficed in <paramref name="DeleteSchool"/>
        /// </summary>
        /// <param name="DeleteSchool">This parameter of type `School`, which
        /// represents a school entity in your application. It contains the data to delete the
        /// specific school.</param>
        /// <returns>
        /// Returns a `Task` object.
        /// </returns>
        public Task DeleteSchool(School DeleteSchool)
        {
            IMongoCollection<School> schools = GetDBCollection<School>(SchoolCollection);
            FilterDefinition<School> filter = Builders<School>.Filter.Eq(s => s.Id, DeleteSchool.Id);

            IMongoCollection<School> schoolsBackup = GetDBCollectionBackup<School>(SchoolCollection);
            schoolsBackup.DeleteOneAsync(filter);

            return schools.DeleteOneAsync(filter);
        }

        /// <summary>
        /// This function retrieves all partners.
        /// </summary>
        /// <returns>
        /// A `Task` that will return a `List<Partner>` containing all partners.
        /// </returns>
        public async Task<List<Partner>> GetPartners()
        {
            IMongoCollection<Partner> partners = GetDBCollection<Partner>(PartnerCollection);
            IAsyncCursor<Partner> results = await partners.FindAsync(_ => true);

            return results.ToList();
        }

        /// <summary>
        /// This function retrieves a list of partners by their ID.
        /// </summary>
        /// <param name="PartnerId">The ID of the partner being retrieved.</param>
        /// <returns>
        /// Returns a `Task` that will contain a list of Partners that match the specified <paramref name="PartnerId"/>.
        /// </returns>
        public async Task<List<Partner>> GetPartnerById(string PartnerId)
        {
            IMongoCollection<Partner> partners = GetDBCollection<Partner>(PartnerCollection);
            IAsyncCursor<Partner> results = await partners.FindAsync(partner => partner.Id == PartnerId);

            return results.ToList();
        }

        /// <summary>
        /// This function retrieves a list of partners based on the provided partner name.
        /// </summary>
        /// <param name="PartnerName">The name of the partner being retrieved.</param>
        /// <returns>
        /// Returns a `Task` that will contain a list of Partners that match the specified <paramref name="PartnerName"/>.
        /// </returns>
        public async Task<List<Partner>> GetPartnerByName(string PartnerName)
        {
            IMongoCollection<Partner> partners = GetDBCollection<Partner>(PartnerCollection);
            IAsyncCursor<Partner> results = await partners.FindAsync(partner => partner.Name == PartnerName);

            return results.ToList();
        }

        /// <summary>
        /// This function creates a new partner.
        /// </summary>
        /// <param name="CreatePartner">
        /// The method takes a parameter of type `Partner`, which represents the partner information
        /// needed to create a new partner.</param>
        /// <returns>
        /// Returns a `Task` object.
        /// </returns>
        public Task CreatePartner(Partner CreatePartner)
        {
            IMongoCollection<Partner> partners = GetDBCollection<Partner>(PartnerCollection);

            IMongoCollection<Partner> partnersBackup = GetDBCollectionBackup<Partner>(PartnerCollection);
            partnersBackup.InsertOneAsync(CreatePartner);

            return partners.InsertOneAsync(CreatePartner);
        }

        /// <summary>
        /// This function updates a partner speficed in <paramref name="UpdatePartner"/>
        /// </summary>
        /// <param name="UpdatePartner">This parameter of type `Partner`, which
        /// represents a partner entity in your application. It contains the data that needs to be updated for a
        /// specific partner.</param>
        /// <returns>
        /// Returns a `Task` object.
        /// </returns>

        public Task UpdatePartner(Partner UpdatePartner)
        {
            IMongoCollection<Partner> partners = GetDBCollection<Partner>(PartnerCollection);
            FilterDefinition<Partner> filter = Builders<Partner>.Filter.Eq(p => p.Id, UpdatePartner.Id);

            IMongoCollection<Partner> partnersBackup = GetDBCollectionBackup<Partner>(PartnerCollection);
            partnersBackup.ReplaceOneAsync(filter, UpdatePartner, new ReplaceOptions { IsUpsert = true });

            return partners.ReplaceOneAsync(filter, UpdatePartner, new ReplaceOptions { IsUpsert = true });
        }

        /// <summary>
        /// This function deletes a partner speficed in <paramref name="DeletePartner"/>
        /// </summary>
        /// <param name="DeletePartner">This parameter of type `Partner`, which
        /// represents a partner entity in your application. It contains the data to delete the
        /// specific partner.</param>
        /// <returns>
        /// Returns a `Task` object.
        /// </returns>
        public Task DeletePartner(Partner DeletePartner)
        {
            IMongoCollection<Partner> partners = GetDBCollection<Partner>(PartnerCollection);
            FilterDefinition<Partner> filter = Builders<Partner>.Filter.Eq(p => p.Id, DeletePartner.Id);

            IMongoCollection<Partner> partnersBackup = GetDBCollectionBackup<Partner>(PartnerCollection);
            partnersBackup.DeleteOneAsync(filter);

            return partners.DeleteOneAsync(filter);
        }
    }
}
