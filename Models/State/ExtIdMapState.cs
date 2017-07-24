using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tebaldi.MarketData.Models.State
{
    public class ExtIdMapState
    {
        public int Id { get; set; }
        public string ExtId { get; set; }
        public int TebBizAtivoId { get; set; }
        public string Ticker { get; set; }

        public SchemaStruct Schema;

        #region "Constructors"
        public ExtIdMapState()
        {
            Schema = new SchemaStruct();
            Schema.ObjectName = "TB_ExtId_Map";

            Schema.Id = "Id";
            Schema.ExtId = "EXT_ID";
            Schema.TebBizAtivoId = "TebaldiBiz_AtivoId";
            Schema.Ticker = "Ticker";
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
            public string ExtId;
            public string TebBizAtivoId;
            public string Ticker;
        }
        #endregion
    }
}
