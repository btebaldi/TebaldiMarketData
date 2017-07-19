using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tebaldi.MarketData.Models.State
{
    public class FeedState
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public FeedTypeEnum Type { get; set; }

        public List<KeyValueState> _keyValues;
        public List<KeyValueState> KeyValues
        {
            get { return _keyValues; }
            internal set { _keyValues = value; }
        }

        private List<FeedTransformationState> _transformations;
        public List<FeedTransformationState> Transformations
        {
            get { return _transformations; }
            internal set { _transformations = value; }
        }

        private List<FeedFilterState> _filter;
        public List<FeedFilterState> Filter
        {
            get { return _filter; }
            internal set { _filter = value; }
        }

        public string GetValue(string key)
        { return KeyValues.Find(c => c.Key == key).Value; }

        readonly SchemaStruct _schema;
        public SchemaStruct Schema { get { return _schema; } }

        #region "Constructors"
        /// <summary>
        /// Constructor
        /// </summary>
        public FeedState()
        {
            Name = "";

            _schema = new SchemaStruct();
            _schema.ObjectName = "TB_Feed";
            _schema.FeedId = "FeedId";
            _schema.Name = "Name";
            _schema.FeedTypeId = "FeedTypeId";
        }
        #endregion

        #region "Schema Structure to return Object and Column Names"
        [Serializable]
        public struct SchemaStruct
        {
            public string ObjectName;
            public string FeedId;
            public string Name;
            public string FeedTypeId;
        }
        #endregion
    }
}
