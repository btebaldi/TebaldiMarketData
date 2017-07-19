using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tebaldi.MarketData.Models.State
{
    public class FeedTypeDefaultKeyState
    {
        public int ID { get; set; }
        public int FeedTypeId { get; set; }
        public string Key { get; set; }

        readonly SchemaStruct _schema;
        public SchemaStruct Schema { get { return _schema; } }

        #region "Constructors"
        /// <summary>
        /// Constructor
        /// </summary>
        public FeedTypeDefaultKeyState()
        {
            _schema = new SchemaStruct();
            _schema.ObjectName = "TB_FeedTypeDefaultKeys";
            _schema.Id = "DefaultKeyId";
            _schema.FeedTypeId = "FeedTypeId";
            _schema.Key = "Chave";
        }
        #endregion

        #region "Schema Structure to return Object and Column Names"
        [Serializable]
        public struct SchemaStruct
        {
            public string ObjectName;
            public string Id;
            public string FeedTypeId;
            public string Key;
        }
        #endregion
    }
}
