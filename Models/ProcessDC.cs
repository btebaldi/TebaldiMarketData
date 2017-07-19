using DataCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Tebaldi.MarketData.Models.State;

namespace Tebaldi.MarketData.Models
{
    public abstract class ProcessDC
    {
        private string mstrConnectString;
        public string ConnectString
        {
            get { return mstrConnectString; }
            set { mstrConnectString = value; }
        }



        #region "Data Retrieval Methods"
        public List<ProcessQueueState> GetQueue()
        {
            return GetQueue(DateTime.Now, false, true);
        }

        public List<ProcessQueueState> GetQueue(DateTime? dtAgendadaMax, bool? executado, bool? activeFeeds)
        {
            IDbCommand cmd;
            string strSQL;
            Tebaldi.MarketData.Models.State.ProcessQueueState obj = new ProcessQueueState();


            strSQL = "procGetProcessQueue";

            cmd = DataLayer.CreateCommand(strSQL, mstrConnectString);
            cmd.CommandType = CommandType.StoredProcedure;
            if (dtAgendadaMax != null)
            { cmd.Parameters.Add(DataLayer.CreateParameter("@dt_agd_max", DbType.DateTime, dtAgendadaMax, mstrConnectString)); }

            if (executado != null)
            { cmd.Parameters.Add(DataLayer.CreateParameter("@executado", DbType.Boolean, executado, mstrConnectString)); }

            if (activeFeeds != null)
            { cmd.Parameters.Add(DataLayer.CreateParameter("@activeFeeds", DbType.Boolean, activeFeeds, mstrConnectString)); }

            DataSet ds = DataLayer.GetDataSet(cmd, mstrConnectString);


            List<ProcessQueueState> lstQueue = (from dr in ds.Tables[0].AsEnumerable()
                                                select new State.ProcessQueueState()
                                                {
                                                    QueueId = Convert.ToInt32(dr[obj.Schema.QueueId]),
                                                    Process = new ImportProcessState
                                                    {
                                                        Id = Convert.ToInt32(dr[obj.Schema.ProcessId]),
                                                        Active = Convert.ToBoolean(dr[obj.Process.Schema.ObjectName + "." + obj.Process.Schema.Active]),
                                                        AutoQueue = Convert.ToBoolean(dr[obj.Process.Schema.ObjectName + "." + obj.Process.Schema.AutoQueue]),
                                                        Feed = new FeedState
                                                        {
                                                            ID = Convert.ToInt32(dr[obj.Process.Schema.ObjectName + "." + obj.Process.Schema.FeedId]),
                                                        },
                                                        Name = Convert.ToString(dr[obj.Process.Schema.ObjectName + "." + obj.Process.Schema.Name])
                                                    },
                                                    DataAgendada = Convert.ToDateTime(dr[obj.Schema.DataAgendada]),
                                                    DataExecucao = dr[obj.Schema.DataExecucao] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr[obj.Schema.DataExecucao]),
                                                    DataReferencia = Convert.ToDateTime(dr[obj.Schema.DataReferencia]),
                                                    Executado = Convert.ToBoolean(dr[obj.Schema.Executado]),
                                                    Success = Convert.ToBoolean(dr[obj.Schema.Success])
                                                }).ToList();

            FeedHandler feed_handller = new FeedHandler(mstrConnectString);
            foreach (ProcessQueueState item in lstQueue)
            {
                item.Process.Feed = feed_handller.GetFeed(item.Process.Feed.ID);
            }

            return lstQueue;
        }

        public ImportProcessState GetImportProcess(int id)
        {
            ImportProcessState obj = GetImportProcesses(id).Find(f => f.Id == id);
            return obj;
        }

        public List<ImportProcessState> GetImportProcess()
        {
            return GetImportProcesses(null);
        }

        private List<ImportProcessState> GetImportProcesses(int? id)
        {
            IDbCommand cmd;
            string strSQL;
            Tebaldi.MarketData.Models.State.ImportProcessState obj = new ImportProcessState();

            strSQL = "procGetProcess";

            cmd = DataLayer.CreateCommand(strSQL, mstrConnectString);
            cmd.CommandType = CommandType.StoredProcedure;
            if (id != null)
            {
                cmd.Parameters.Add(DataLayer.CreateParameter("@Id", DbType.Int32, id, mstrConnectString));
            }

            DataSet ds = DataLayer.GetDataSet(cmd, mstrConnectString);

            List<ImportProcessState> lst = (from dr in ds.Tables[0].AsEnumerable()
                                            select new State.ImportProcessState()
                                            {
                                                Id = Convert.ToInt32(dr[obj.Schema.Id]),
                                                Name = Convert.ToString(dr[obj.Schema.Name]),
                                                Feed = new FeedState { ID = Convert.ToInt32(dr[obj.Schema.FeedId]) },
                                                AutoQueue = Convert.ToBoolean(dr[obj.Schema.AutoQueue]),
                                                Active = Convert.ToBoolean(dr[obj.Schema.Active])
                                            }).ToList();


            FeedHandler feed_handller = new FeedHandler(mstrConnectString);
            foreach (ImportProcessState item in lst)
            {
                item.Feed = feed_handller.GetFeed(item.Feed.ID);
            }

            return lst;
        }

        #endregion

        #region "Queue Modification Methods"

        public int Save(State.ProcessQueueState queueState)
        {
            List<ProcessQueueState> lst = new List<ProcessQueueState>();
            lst.Add(queueState);

            return Save(lst);
        }

        public int Save(List<ProcessQueueState> lstqueueState)
        {
            IDbCommand cmd;
            string strSQL;

            // Check Business Rules
            foreach (State.ProcessQueueState item in lstqueueState)
            {
                Validate(item);
            }

            strSQL = "procGravaQueue";

            cmd = DataLayer.CreateCommand(strSQL, mstrConnectString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(DataLayer.CreateParameter("@queueList", DbType.String, ParseToXml(lstqueueState), mstrConnectString));

            return DataLayer.ExecuteSQL(cmd);
        }

        public virtual void Validate(ProcessQueueState item)
        {
            string strMsg = string.Empty;

            if (item.Process.Id <= 0)
            { strMsg += "FeedId Id nao pode ser menor ou igual a zero." + Environment.NewLine; }

            if (strMsg != string.Empty)
            { throw new TebaldiMarketDataException(strMsg); }
        }

        protected string ParseToXml(List<ProcessQueueState> list)
        {
            XmlDocument xmlDoc = new XmlDocument();
            int nodeContador = 1;

            XmlElement root = xmlDoc.CreateElement("ROOT");
            xmlDoc.AppendChild(root);

            foreach (ProcessQueueState item in list)
            {
                XmlElement xmlAtivo = xmlDoc.CreateElement("Queue");
                xmlAtivo.SetAttribute("NodeId", nodeContador.ToString());

                xmlAtivo.SetAttribute("QueueId", item.QueueId.ToString());

                xmlAtivo.SetAttribute("ProcessId", item.Process.Id.ToString());
                xmlAtivo.SetAttribute("DtAgendada", item.DataAgendada.ToString("s"));

                if (item.DataExecucao != DateTime.MinValue) { xmlAtivo.SetAttribute("DtExecucao", item.DataExecucao.ToString("s")); }

                xmlAtivo.SetAttribute("DtReferencia", item.DataReferencia.ToString("s"));
                xmlAtivo.SetAttribute("Executado", item.Executado.ToString());
                xmlAtivo.SetAttribute("Success", item.Success.ToString());

                root.AppendChild(xmlAtivo);

                nodeContador++;
            }

            return xmlDoc.OuterXml;
        }

        #endregion

        #region "Import Process" Modification Methods

        public int Save(State.ImportProcessState importState)
        {
            List<ImportProcessState> lst = new List<ImportProcessState>();
            lst.Add(importState);

            return Save(lst);
        }

        public int Save(List<ImportProcessState> lstImportState)
        {
            IDbCommand cmd;
            string strSQL;

            // Check Business Rules
            foreach (State.ImportProcessState item in lstImportState)
            {
                Validate(item);
            }

            strSQL = "procGravaImportProcess";

            cmd = DataLayer.CreateCommand(strSQL, mstrConnectString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(DataLayer.CreateParameter("@importList", DbType.String, ParseToXml(lstImportState), mstrConnectString));

            return DataLayer.ExecuteSQL(cmd);
        }


        public virtual void Validate(ImportProcessState item)
        {
            string strMsg = string.Empty;

            if (item.Feed.ID <= 0)
            { strMsg += "FeedId Id nao pode ser menor ou igual a zero." + Environment.NewLine; }

            if (strMsg != string.Empty)
            { throw new TebaldiMarketDataException(strMsg); }
        }

        protected string ParseToXml(List<ImportProcessState> list)
        {
            XmlDocument xmlDoc = new XmlDocument();
            int nodeContador = 1;

            XmlElement root = xmlDoc.CreateElement("ROOT");
            xmlDoc.AppendChild(root);

            foreach (ImportProcessState item in list)
            {
                XmlElement xmlAtivo = xmlDoc.CreateElement("ImportProcess");
                xmlAtivo.SetAttribute("NodeId", nodeContador.ToString());

                xmlAtivo.SetAttribute("ProcessId", item.Id.ToString());

                xmlAtivo.SetAttribute("Name", item.Name);
                xmlAtivo.SetAttribute("FeedId", item.Feed.ID.ToString());
                xmlAtivo.SetAttribute("AutoQueue", item.AutoQueue.ToString());
                xmlAtivo.SetAttribute("Active", item.Active.ToString());

                root.AppendChild(xmlAtivo);

                nodeContador++;
            }

            return xmlDoc.OuterXml;
        }

        #endregion

    }
}
