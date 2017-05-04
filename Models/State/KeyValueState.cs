using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tebaldi.MarketData.Models.State
{
   public class KeyValueState
    {
        public int KeyValueId { get; set; }
        public int FeedId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public bool FeedSprecific { get; set; }

        readonly SchemaStruct _schema;
            public SchemaStruct Schema { get { return _schema; } }

            #region "Constructors"
            /// <summary>
            /// Constructor
            /// </summary>
            public KeyValueState()
            {
                _schema = new SchemaStruct();
                _schema.ObjectName = "TB_FeedKeyValue";
                _schema.Id = "KeyValueId";
                _schema.FeedId = "FeedId";
                _schema.Key = "Chave";
                _schema.Value = "Valor";
                _schema.FeedSpecific = "FeedSpecific";
            }
            #endregion

            #region "Schema Structure to return Object and Column Names"
            [Serializable]
            public struct SchemaStruct
            {
                public string ObjectName;
                public string Id;
                public string FeedId;
                public string Key;
                public string Value;
                public string FeedSpecific;
            }
            #endregion
        }
}
