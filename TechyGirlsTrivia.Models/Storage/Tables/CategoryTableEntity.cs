using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechyGirlsTrivia.Models.Storage.Tables
{
    public class CategoryTableEntity: TableEntity
    {
        public CategoryTableEntity(string categoryId, string categoryName)
        {
            this.PartitionKey = categoryId;
            this.RowKey = categoryName;
        }
        public string CategoryLogo { get; set; }

        public CategoryTableEntity() { }
    }
}
