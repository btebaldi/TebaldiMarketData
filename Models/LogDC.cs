using DataCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tebaldi.MarketData.Models.State;


namespace Tebaldi.MarketData.Models
{
    public abstract class LogDC
    {

        private string mstrConnectString;
        public string ConnectString
        {
            get { return mstrConnectString; }
            set { mstrConnectString = value; }
        }

        #region "Data Retrieval Methods"
        public List<LogState> GetLogs()
        {
            IDbCommand cmd;
            string strSQL;
            Tebaldi.MarketData.Models.State.LogState obj = new LogState();


            strSQL = "procGetLogs";

            cmd = DataLayer.CreateCommand(strSQL, mstrConnectString);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add(DataLayer.CreateParameter("@ImpProcessId", DbType.Int32, impProcessId, mstrConnectString));

            DataSet ds = DataLayer.GetDataSet(cmd, mstrConnectString);


            List<LogState> lst = (from dr in ds.Tables[0].AsEnumerable()
                                  select new State.LogState()
                                  {
                                      Id = Convert.ToInt32(dr[obj.Schema.Id]),
                                      Date = Convert.ToDateTime(dr[obj.Schema.Date]),
                                      Level = Convert.ToString(dr[obj.Schema.Level]),
                                      Logger = Convert.ToString(dr[obj.Schema.Logger]),
                                      Message = Convert.ToString(dr[obj.Schema.Message]),
                                      ExceptionMessage = Convert.ToString(dr[obj.Schema.ExceptionMessage])
                                  }).ToList();

            return lst;
        }

        #endregion
    }
}
