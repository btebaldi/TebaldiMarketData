using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tebaldi.MarketData.Models.State
{
    public class FeedFilterState
    {
        public int Id { get; set; }
        public int FeedId { get; set; }
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }

        public SchemaStruct Schema;

        #region "Constructors"
        public FeedFilterState()
        {
            Schema = new SchemaStruct();
            Schema.ObjectName = "TB_FeedFilter";

            Schema.Id = "ID";
            Schema.FeedId = "FeedId";
            Schema.ColumnName = "ColumnName";
            Schema.ColumnValue = "ColumnValue";
        }
        #endregion

        #region "Schema Structure to return Object and Column Names"
        [Serializable]
        public struct SchemaStruct
        {
            // Nome da tabela
            public string ObjectName;

            // Nome das colunas
            public string Id;
            public string FeedId;
            public string ColumnName;
            public string ColumnValue;
        }
        #endregion

    }
}
