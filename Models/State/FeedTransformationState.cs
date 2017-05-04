using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tebaldi.MarketData.Models.State
{
    public class FeedTransformationState
    {
        public int TransformationId { get; set; }
        public int FeedId { get; set; }
        public int ExecuteOrder { get; set; }
        public string OriginalValue { get; set; }
        public string OriginalColumn { get; set; }

        public string NewValue { get; set; }
        public string NewColumn { get; set; }

        readonly SchemaStruct _schema;
        public SchemaStruct Schema { get { return _schema; } }

        #region "Constructors"
        public FeedTransformationState()
        {
            _schema = new SchemaStruct();
            _schema.ObjectName = "TB_FEED_TRANSFORMATION";
            _schema.TransformationId = "TransformationId";
            _schema.FeedId = "FeedId";
            _schema.ExecuteOrder = "ExecuteOrder";
            _schema.OriginalValue = "OriginalValue";
            _schema.OriginalColumn = "OriginalColumn";
            _schema.NewValue = "NewValue";
            _schema.NewColumn = "NewColumn";
        }
        #endregion

        #region "Schema Structure to return Object and Column Names"
        [Serializable]
        public struct SchemaStruct
        {
            public string ObjectName;
            public string TransformationId;
            public string FeedId;
            public string ExecuteOrder;
            public string OriginalValue;
            public string OriginalColumn;
            public string NewValue;
            public string NewColumn;
        }
        #endregion
    }
}
