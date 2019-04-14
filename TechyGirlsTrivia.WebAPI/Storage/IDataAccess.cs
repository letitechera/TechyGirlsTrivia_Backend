using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechyGirlsTrivia.WebAPI.Storage
{
    public interface IDataAccess
    {
        Task StoreEntity(ITableEntity entity, string tableName);
    }
}
