using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tebaldi.MarketData.Models.State
{
    public class LogState
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
        public string ExceptionMessage { get; set; }

        readonly SchemaStruct _schema;
        public SchemaStruct Schema { get { return _schema; } }

        #region "Constructors"
        /// <summary>
        /// Constructor
        /// </summary>
        public LogState()
        {
            _schema = new SchemaStruct();
            _schema.ObjectName = "TB_LOG";
            _schema.Id = "id";
            _schema.Date = "log_date";
            _schema.UtcDate = "log_utcdate";
            _schema.Level = "Log_Level";
            _schema.Logger = "Logger";
            _schema.Message = "log_Message";
            _schema.ExceptionMessage = "Exception";
        }
        #endregion

        #region "Schema Structure to return Object and Column Names"
        [Serializable]
        public struct SchemaStruct
        {
            public string ObjectName;
            public string Id;
            public string Date;
            public string UtcDate;
            public string Level;
            public string Logger;
            public string Message;
            public string ExceptionMessage;
        }
        #endregion
    }
}

