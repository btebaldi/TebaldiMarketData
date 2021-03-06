﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tebaldi.MarketData.Models.State
{
    public class ProcessQueueState
    {
        public int QueueId { get; set; }
        public ImportProcessState Process { get; set; }
        public DateTime DataAgendada { get; set; }
        public DateTime DataExecucao { get; set; }
        public DateTime DataReferencia { get; set; }
        public bool Executado { get; set; }
        public bool Success { get; set; }

        readonly SchemaStruct _schema;
        public SchemaStruct Schema { get { return _schema; } }

        #region "Constructors"
        /// <summary>
        /// Constructor
        /// </summary>
        public ProcessQueueState()
        {
            //DataExecucao = null;
            Process = new ImportProcessState();
            _schema = new SchemaStruct();
            _schema.ObjectName = "TB_ProcessQueue";
            _schema.QueueId = "QueueId";
            _schema.ProcessId = "ProcessId";
            _schema.DataAgendada = "DtAgendada";
            _schema.DataExecucao = "DtExecucao";
            _schema.DataReferencia = "DtReferencia";
            _schema.Executado = "Executado";
            _schema.Success = "Success";
        }
        #endregion

        #region "Schema Structure to return Object and Column Names"
        [Serializable]
        public struct SchemaStruct
        {
            public string ObjectName;
            public string QueueId;
            public string ProcessId;
            public string DataAgendada;
            public string DataExecucao;
            public string DataReferencia;
            public string Executado;
            public string Success;
        }
        #endregion
    }

}
