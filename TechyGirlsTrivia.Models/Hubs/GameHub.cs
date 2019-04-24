using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;
using TechyGirlsTrivia.Models;
using TechyGirlsTrivia.Models.Storage.Tables;

namespace TechyGirlsTrivia.Models.Hubs
{
    public class GameHub : Hub
    {
        private readonly IConfiguration Configuration;

        public GameHub(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public async Task BroadcastStart(bool data) => await Clients.All.SendAsync("broadcastStart", data);

        public async Task BroadcastAnswer(UserAnswer data)
        {
            await Clients.All.SendAsync("broadcastAnswer", data);

            var userScore = new ParticipantsTableEntity
            {
                PartitionKey = data.GameId,
                RowKey = data.ParticipantId,
                TotalScore = data.Score,
                Time = data.Time,
            };

            //CloudStorageAccount
            var conectionString = Configuration.GetValue<string>("StorageConfig:StringConnection");
            var storageAccount = CloudStorageAccount.Parse(conectionString);

            //CloudTableClient
            var tableClient = storageAccount.CreateCloudTableClient();

            //CloudTable
            var table = tableClient.GetTableReference("Participants");
            await table.CreateIfNotExistsAsync();

            //TableOperation
            var insertOperation = TableOperation.InsertOrMerge(userScore);

            await table.ExecuteAsync(insertOperation);
        }

        
    }
}
