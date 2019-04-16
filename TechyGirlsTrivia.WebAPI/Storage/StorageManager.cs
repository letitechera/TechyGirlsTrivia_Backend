using Microsoft.WindowsAzure.Storage;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using TechyGirlsTrivia.WebAPI.Storage.Tables;

namespace TechyGirlsTrivia.WebAPI.Storage
{
    public class StorageManager : IStorageManager
    {
        private readonly IConfiguration Configuration;

        public StorageManager(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task StoreEntity(ITableEntity entity, string tableName)
        {
            //CloudStorageAccount
            var conectionString = Configuration.GetValue<string>("StorageConfig:StringConnection");
            var storageAccount = CloudStorageAccount.Parse(conectionString);

            //CloudTableClient
            var tableClient = storageAccount.CreateCloudTableClient();

            //CloudTable
            var table = tableClient.GetTableReference(tableName);
            await table.CreateIfNotExistsAsync();

            //TableOperation
            var insertOperation = TableOperation.InsertOrReplace(entity);

            await table.ExecuteAsync(insertOperation);

        }

        public List<ParticipantsTableEntity> GetAllParticipants(string gameId)
        {
            //CloudStorageAccount
            var conectionString = Configuration.GetValue<string>("StorageConfig:StringConnection");
            var basePath = Configuration.GetValue<string>("StorageConfig:BaseStoragePath");

            var account = CloudStorageAccount.Parse(conectionString);

            CloudTableClient tableClient = account.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("Participants");

            TableQuery<ParticipantsTableEntity> query = new TableQuery<ParticipantsTableEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, gameId));

            var result = table.ExecuteQuerySegmentedAsync(query, null).Result;

            return result.Results;
        }

        public List<ParticipantsTableEntity> SearchNames(string name)
        {
            //CloudStorageAccount
            var conectionString = Configuration.GetValue<string>("StorageConfig:StringConnection");
            var basePath = Configuration.GetValue<string>("StorageConfig:BaseStoragePath");

            var account = CloudStorageAccount.Parse(conectionString);

            CloudTableClient tableClient = account.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("Participants");

            TableQuery<ParticipantsTableEntity> query = new TableQuery<ParticipantsTableEntity>()
                .Where(TableQuery.GenerateFilterCondition("ParticipantName", QueryComparisons.Equal, name));

            var result = table.ExecuteQuerySegmentedAsync(query, null).Result;

            return result.Results;
        }

        //public List<GroupTableEntity> GetScoresByGroup(string groupName)
        //{
        //    //CloudStorageAccount
        //    var storageAccount = CloudStorageAccount.Parse(
        //        CloudConfigurationManager.GetSetting("StorageConnectionString"));

        //    //CloudTableClient
        //    var tableClient = storageAccount.CreateCloudTableClient();

        //    //CloudTable
        //    var table = tableClient.GetTableReference("GroupScore");
        //    table.CreateIfNotExists();

        //    // Construct the query operation for all entities where PartitionKey="groupName".
        //    var query = new TableQuery<GroupTableEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, groupName));

        //    return table.ExecuteQuery(query).ToList();
        //}
    }
}
