using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tebaldi.MarketData.Models.State
{
    public class ImportProcessState
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public FeedState Feed { get; set; }
        public bool AutoQueue { get; set; }
        public bool Active { get; set; }

        readonly SchemaStruct _schema;
        public SchemaStruct Schema { get { return _schema; } }


        #region "Constructors"
        /// <summary>
        /// Constructor
        /// </summary>
        public ImportProcessState()
        {
            Feed = new FeedState();
            _schema = new SchemaStruct();
            _schema.ObjectName = "TB_ImportProcess";
            _schema.Id = "ProcessId";
            _schema.Name = "Name";
            _schema.FeedId = "FeedId";
            _schema.AutoQueue = "AutoQueue";
            _schema.Active = "Active";
        }
        #endregion

        #region "Schema Structure to return Object and Column Names"
        [Serializable]
        public struct SchemaStruct
        {
            public string ObjectName;
            public string Id;
            public string Name;
            public string FeedId;
            public string AutoQueue;
            public string Active;
        }
        #endregion
    }
}
