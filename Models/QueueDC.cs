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

        #endregion

    }
}
