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
    public abstract class QueueDC
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
            IDbCommand cmd;
            string strSQL;
            Tebaldi.MarketData.Models.State.ProcessQueueState obj = new ProcessQueueState();


            strSQL = "procGetProcessQueue";

            cmd = DataLayer.CreateCommand(strSQL, mstrConnectString);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add(DataLayer.CreateParameter("@ImpProcessId", DbType.Int32, impProcessId, mstrConnectString));

            DataSet ds = DataLayer.GetDataSet(cmd, mstrConnectString);

            List<ProcessQueueState> lstQueue = (from dr in ds.Tables[0].AsEnumerable()
                                                select new State.ProcessQueueState()
                                                {
                                                    QueueId = Convert.ToInt32(dr[obj.Schema.QueueId]),
                                                    FeedId = Convert.ToInt32(dr[obj.Schema.FeedId]),
                                                    DataExecucao = Convert.ToDateTime(dr[obj.Schema.DataExecucao]),
                                                    DataReferencia = Convert.ToDateTime(dr[obj.Schema.DataReferencia]),
                                                    Executado = Convert.ToBoolean(dr[obj.Schema.Executado]),
                                                    Success = Convert.ToBoolean(dr[obj.Schema.Success])
                                                }).ToList();
            return lstQueue;
        }
        #endregion

        #region "Data Modification Methods"

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

            if (item.FeedId <= 0)
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

                xmlAtivo.SetAttribute("FeedId", item.FeedId.ToString());
                xmlAtivo.SetAttribute("DtExecucao", item.DataExecucao.ToString("s"));
                xmlAtivo.SetAttribute("DtReferencia", item.DataReferencia.ToString("s"));
                xmlAtivo.SetAttribute("Executado", item.Executado.ToString());
                xmlAtivo.SetAttribute("Success", item.Success.ToString());

                root.AppendChild(xmlAtivo);

                nodeContador++;
            }

            return xmlDoc.OuterXml;
        }

        #endregion

    }
}
