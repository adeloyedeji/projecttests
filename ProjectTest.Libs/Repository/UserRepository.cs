using Microsoft.Azure.Cosmos;
using ProjectTest.Libs.Contracts;
using ProjectTest.Libs.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using User = ProjectTest.Libs.Models.User;

namespace ProjectTest.Libs.Repository
{
    public class UserRepository : IUser
    {
        private const string DatabaseId   = "project-test";
        private const string ContainerId  = "Users";
        private const string PartitionKey = "/Id";
        private readonly CosmosClient _cosmosClient;
        private readonly Database _database;
        private readonly Container _container;

        public UserRepository(CosmosClient cosmosClient)
        {
            _cosmosClient       = cosmosClient;
            var createDB        = _cosmosClient.CreateDatabaseIfNotExistsAsync(DatabaseId).Result;
            _database           = _cosmosClient.GetDatabase(DatabaseId);
            var createContainer = _database.CreateContainerIfNotExistsAsync(ContainerId, PartitionKey).Result;
            _container          = _database.GetContainer(ContainerId);
        }
        public async Task<User> CreateAsync(User user)
        {
            var partitionKey = new PartitionKey(user.Id);
            return await _container.CreateItemAsync<User>(user, partitionKey);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var partitionKey = new PartitionKey(id);
            var response = await _container.DeleteItemAsync<User>(id, partitionKey);

            return response.StatusCode == HttpStatusCode.OK ? true : false;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await GetAsync(id) == null ? false : true;
        }

        public async Task<User> GetAsync(string id)
        {
            var partitionKey = new PartitionKey(id);
            return await _container.ReadItemAsync<User>(id, partitionKey);
        }

        public async Task<User> UpdateAsync(User user)
        {
            var partitionKey = new PartitionKey(user.Id);
            return await _container.UpsertItemAsync<User>(user, partitionKey);
        }
    }
}
