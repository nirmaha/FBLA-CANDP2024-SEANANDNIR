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
        private MongoClientSettings Settings = MongoClientSettings.FromConnectionString(ConnectionUri);

        private MongoClient Client;
        private IMongoDatabase MongoDatabase;

        private const string UserCollection = "Users";
        private const string SchoolCollection = "Schools";
        private const string PartnerCollection = "Partners";

        public Database()
        {
            Settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            Client = new MongoClient(Settings);

            MongoDatabase = Client.GetDatabase("SchoolPartnersInfo");

            try
            {
                BsonDocument result = Client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                Debug.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Could not connect to Database");
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

       
        public async Task<List<User>> GetUsers()
        {
            IMongoCollection<User> users = GetDBCollection<User>(UserCollection);
            IAsyncCursor<User> results = await users.FindAsync(_ => true);

            return results.ToList();
        }

        public async Task<List<User>> GetUserById(string UserId) 
        {
            IMongoCollection<User> users = GetDBCollection<User>(UserCollection);
            IAsyncCursor<User> results = await users.FindAsync(user => user.Id == UserId);

            return results.ToList();
        }  
        
        public async Task<List<User>> GetUserByName(string UsersName) 
        {
            IMongoCollection<User> users = GetDBCollection<User>(UserCollection);
            IAsyncCursor<User> results = await users.FindAsync(user => user.Name == UsersName);

            return results.ToList();
        }
        public async Task<List<User>> GetUserByEmail(string UserEmail)
        {
            IMongoCollection<User> users = GetDBCollection<User>(UserCollection);
            IAsyncCursor<User> results = await users.FindAsync(user => user.Email == UserEmail);

            return results.ToList();
        }

        public Task CreateUser(User CreateUser) 
        {
            IMongoCollection<User> users = GetDBCollection<User>(UserCollection);

            return users.InsertOneAsync(CreateUser);
        }

        public Task UpdateUser(User UpdateUser)
        {
            IMongoCollection<User> users = GetDBCollection<User>(UserCollection);
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(u => u.Id, UpdateUser.Id);

            return users.ReplaceOneAsync(filter, UpdateUser, new ReplaceOptions {IsUpsert = true});
        }

        public Task DeleteUser(User DeleteUser)
        {
            IMongoCollection<User> users = GetDBCollection<User>(UserCollection);
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(u => u.Id, DeleteUser.Id);

            return users.DeleteOneAsync(filter);
        }

        public async Task<List<School>> GetSchools()
        {
            IMongoCollection<School> schools = GetDBCollection<School>(SchoolCollection);
            IAsyncCursor<School> results = await schools.FindAsync(_ => true);

            return results.ToList();
        }

        public async Task<List<School>> GetSchoolById(string SchoolId) 
        {
            IMongoCollection<School> schools = GetDBCollection<School>(SchoolCollection);
            IAsyncCursor<School> results = await schools.FindAsync(school => school.Id == SchoolId);

            return results.ToList();
        }    
        
        public async Task<List<School>> GetSchoolByName(string SchoolName) 
        {
            IMongoCollection<School> schools = GetDBCollection<School>(SchoolCollection);
            IAsyncCursor<School> results = await schools.FindAsync(school => school.Name == SchoolName);

            return results.ToList();
        }

        public Task CreateSchool(School CreateSchool) 
        {
            IMongoCollection<School> schools = GetDBCollection<School>(SchoolCollection);

            return schools.InsertOneAsync(CreateSchool);
        }

        public Task UpdateSchool(School UpdateSchool)
        {
            IMongoCollection<School> schools = GetDBCollection<School>(SchoolCollection);
            FilterDefinition<School> filter = Builders<School>.Filter.Eq(s => s.Id, UpdateSchool.Id);

            return schools.ReplaceOneAsync(filter, UpdateSchool, new ReplaceOptions {IsUpsert = true});
        }

        public Task DeleteSchool(School DeleteSchool)
        {
            IMongoCollection<School> schools = GetDBCollection<School>(SchoolCollection);
            FilterDefinition<School> filter = Builders<School>.Filter.Eq(s => s.Id, DeleteSchool.Id);

            return schools.DeleteOneAsync(filter);
        }

        public async Task<List<Partner>> GetPartners()
        {
            IMongoCollection<Partner> partners = GetDBCollection<Partner>(PartnerCollection);
            IAsyncCursor<Partner> results = await partners.FindAsync(_ => true);

            return results.ToList();
        }

        public async Task<List<Partner>> GetPartnerById(string PartnerId)
        {
            IMongoCollection<Partner> partners = GetDBCollection<Partner>(PartnerCollection);
            IAsyncCursor<Partner> results = await partners.FindAsync(partner => partner.Id == PartnerId);

            return results.ToList();
        }

        public async Task<List<Partner>> GetPartnerByName(string PartnerName)
        {
            IMongoCollection<Partner> partners = GetDBCollection<Partner>(PartnerCollection);
            IAsyncCursor<Partner> results = await partners.FindAsync(partner => partner.Name == PartnerName);

            return results.ToList();
        }

        public Task CreatePartner(Partner CreatePartner)
        {
            IMongoCollection<Partner> partners = GetDBCollection<Partner>(PartnerCollection);

            return partners.InsertOneAsync(CreatePartner);
        }

        public Task UpdatePartner(Partner UpdatePartner)
        {
            IMongoCollection<Partner> partners = GetDBCollection<Partner>(PartnerCollection);
            FilterDefinition<Partner> filter = Builders<Partner>.Filter.Eq(p => p.Id, UpdatePartner.Id);

            return partners.ReplaceOneAsync(filter, UpdatePartner, new ReplaceOptions { IsUpsert = true });
        }

        public Task DeletePartner(Partner DeletePartner)
        {
            IMongoCollection<Partner> partners = GetDBCollection<Partner>(PartnerCollection);
            FilterDefinition<Partner> filter = Builders<Partner>.Filter.Eq(p => p.Id, DeletePartner.Id);

            return partners.DeleteOneAsync(filter);
        }
    }
}
