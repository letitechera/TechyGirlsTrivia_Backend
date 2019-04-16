using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechyGirlsTrivia.Models.Models;

namespace TechyGirlsTrivia.WebAPI.Storage
{
    public interface IDataAccess
    {
        Task StoreEntity(ITableEntity entity, string tableName);
        IEnumerable<Participant> GetParticipants(string gameId);
        bool AlreadyExists(string name);
    }
}
