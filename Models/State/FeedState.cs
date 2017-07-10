﻿using System;
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
        public FeedTypeStateEnum Type { get; set; }
        public bool Active { get; set; }

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
            _schema.Active = "Active";
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
            public string Active;
        }
        #endregion
    }
}
