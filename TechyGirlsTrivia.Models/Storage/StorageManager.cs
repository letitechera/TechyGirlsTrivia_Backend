using Microsoft.WindowsAzure.Storage;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using TechyGirlsTrivia.Models.Storage.Tables;

namespace TechyGirlsTrivia.Models.Storage
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

            var account = CloudStorageAccount.Parse(conectionString);

            CloudTableClient tableClient = account.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("Participants");

            TableQuery<ParticipantsTableEntity> query = new TableQuery<ParticipantsTableEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, gameId));

            var result = table.ExecuteQuerySegmentedAsync(query, null).Result;

            return result.Results;
        }

        public async Task SaveAnswer(ParticipantsTableEntity participant)
        {
            //CloudStorageAccount
            var conectionString = Configuration.GetValue<string>("StorageConfig:StringConnection");
            var storageAccount = CloudStorageAccount.Parse(conectionString);

            //CloudTableClient
            var tableClient = storageAccount.CreateCloudTableClient();

            //CloudTable
            var table = tableClient.GetTableReference("Participants");
            await table.CreateIfNotExistsAsync();

            //TableOperation
            var insertOperation = TableOperation.InsertOrMerge(participant);

            await table.ExecuteAsync(insertOperation);
        }

        public List<ParticipantsTableEntity> SearchNames(string name)
        {
            //CloudStorageAccount
            var conectionString = Configuration.GetValue<string>("StorageConfig:StringConnection");

            var account = CloudStorageAccount.Parse(conectionString);

            CloudTableClient tableClient = account.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("Participants");

            TableQuery<ParticipantsTableEntity> query = new TableQuery<ParticipantsTableEntity>()
                .Where(TableQuery.GenerateFilterCondition("ParticipantName", QueryComparisons.Equal, name));

            var result = table.ExecuteQuerySegmentedAsync(query, null).Result;

            return result.Results;
        }

        public List<QuestionsTableEntity> GetQuestion(int questionId)
        {
            //CloudStorageAccount
            var conectionString = Configuration.GetValue<string>("StorageConfig:StringConnection");

            var account = CloudStorageAccount.Parse(conectionString);

            CloudTableClient tableClient = account.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("Questions");

            TableQuery<QuestionsTableEntity> query = new TableQuery<QuestionsTableEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, questionId + ""));

            var result = table.ExecuteQuerySegmentedAsync(query, null).Result;

            return result.Results;
        }

        public List<AnswersTableEntity> GetAnswers(int questionId)
        {
            //CloudStorageAccount
            var conectionString = Configuration.GetValue<string>("StorageConfig:StringConnection");

            var account = CloudStorageAccount.Parse(conectionString);

            CloudTableClient tableClient = account.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("Answers");

            TableQuery<AnswersTableEntity> query = new TableQuery<AnswersTableEntity>()
                .Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, questionId + ""));

            var result = table.ExecuteQuerySegmentedAsync(query, null).Result;

            return result.Results;
        }

        public List<CategoryTableEntity> GetCategory(int categoryId)
        {
            //CloudStorageAccount
            var conectionString = Configuration.GetValue<string>("StorageConfig:StringConnection");

            var account = CloudStorageAccount.Parse(conectionString);

            CloudTableClient tableClient = account.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("Categories");

            TableQuery<CategoryTableEntity> query = new TableQuery<CategoryTableEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, categoryId + ""));

            var result = table.ExecuteQuerySegmentedAsync(query, null).Result;

            return result.Results;
        }

        public async Task<string> LoadUserImage(IFormFile file)
        {
            var conectionString = Configuration.GetValue<string>("StorageConfig:StringConnection");
            var basePath = Configuration.GetValue<string>("StorageConfig:BaseStoragePath");

            if (CloudStorageAccount.TryParse(conectionString, out var storageAccount))
            {
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("userimages");
                await container.CreateIfNotExistsAsync();

                BlobContainerPermissions permissions = new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                };
                await container.SetPermissionsAsync(permissions);

                var guid = Guid.NewGuid();

                var newName = $"user_{guid}.jpg";
                var newBlob = container.GetBlockBlobReference(newName);
                await newBlob.UploadFromStreamAsync(file.OpenReadStream());

                return $"{basePath}/userimages/" + newName;
            }
            else
            {
                return "";
            }
        }
    }
}
