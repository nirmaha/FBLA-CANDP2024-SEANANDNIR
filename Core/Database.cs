using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

using MongoDB.Driver;
using MongoDB.Bson;

using EduPartners.MVVM.Model;

namespace EduPartners.Core
{
    public class Database
    {
        private const string ConnectionUri = "mongodb+srv://FBLANS2024:8D7T6rAW0Ihy30E9@management.wvovlna.mongodb.net/?retryWrites=true&w=majority";
        private MongoClientSettings Settings = MongoClientSettings.FromConnectionString(ConnectionUri);

        private MongoClient Client;
        private IMongoDatabase MongoDatabase;

        private const string UserCollection = "Users";
        private const string SchoolCollection = "Schools";

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

        public async Task<List<User>> GetUser(string UserId) 
        {
            IMongoCollection<User> users = GetDBCollection<User>(UserCollection);
            IAsyncCursor<User> results = await users.FindAsync(user => user.Id == UserId);

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

        public async Task<List<School>> GetSchool(string SchoolId) 
        {
            IMongoCollection<School> schools = GetDBCollection<School>(SchoolCollection);
            IAsyncCursor<School> results = await schools.FindAsync(school => school.Id == SchoolId);

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

    }
}
