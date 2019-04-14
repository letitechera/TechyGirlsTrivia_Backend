using Microsoft.WindowsAzure.Storage;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;

namespace TechyGirlsTrivia.WebAPI.Storage
{
    public class StorageManager: IStorageManager
    {
        public async Task StoreEntity(ITableEntity entity, string tableName)
        {
            //CloudStorageAccount
            var StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=triviadata;AccountKey=F1t/eggIoGscAMknEEEBki8npi5lPb+6GbeNJyrGWcPUSllIIE//N4U5lyCvH82tYyml6VeqGX1cSSWIzRfAng==;EndpointSuffix=core.windows.net";
            var storageAccount = CloudStorageAccount.Parse(StorageConnectionString);

            //CloudTableClient
            var tableClient = storageAccount.CreateCloudTableClient();

            //CloudTable
            var table = tableClient.GetTableReference(tableName);
            await table.CreateIfNotExistsAsync();

            //TableOperation
            var insertOperation = TableOperation.InsertOrReplace(entity);

            await table.ExecuteAsync(insertOperation);

        }

        //public List<string> GetAllGroupsNames()
        //{
        //    //CloudStorageAccount
        //    var storageAccount = CloudStorageAccount.Parse(
        //        CloudConfigurationManager.GetSetting("StorageConnectionString"));

        //    //CloudTableClient
        //    var tableClient = storageAccount.CreateCloudTableClient();

        //    //CloudTable
        //    var table = tableClient.GetTableReference("Group");
        //    table.CreateIfNotExists();

        //    // Construct the query operation for all entities where PartitionKey="Name".
        //    var query = new TableQuery<GroupTableEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Name"));

        //    return table.ExecuteQuery(query).Select(n => n.RowKey).ToList();
        //    //return table.ExecuteQuery(query).Where(t => !t.RowKey.ToLower().Contains("test")).Select(n => n.RowKey).ToList();

        //}

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
