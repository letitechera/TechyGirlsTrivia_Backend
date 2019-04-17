using Microsoft.WindowsAzure.Storage;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using TechyGirlsTrivia.WebAPI.Storage.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage.Blob;
using System;

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

            var account = CloudStorageAccount.Parse(conectionString);

            CloudTableClient tableClient = account.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("Participants");

            TableQuery<ParticipantsTableEntity> query = new TableQuery<ParticipantsTableEntity>()
                .Where(TableQuery.GenerateFilterCondition("ParticipantName", QueryComparisons.Equal, name));

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
