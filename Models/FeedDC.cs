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
    public abstract class FeedDC
    {
        private string mstrConnectString;
        public string ConnectString
        {
            get { return mstrConnectString; }
            set { mstrConnectString = value; }
        }

        #region "Data Retrieval Methods"

        #region Feed State Retrival
        public List<FeedState> GetFeeds()
        {
            return GetFeeds(null);
        }

        public FeedState GetFeed(int Id)
        {
            FeedState feed = GetFeeds(Id).Find(f => f.ID == Id);
            return feed;
        }

        private List<FeedState> GetFeeds(int? feedId)
        {
            IDbCommand cmd;
            State.FeedState obj = new State.FeedState();

            string strSQL;
            strSQL = "procGetFeed";

            cmd = DataLayer.CreateCommand(strSQL, mstrConnectString);
            cmd.CommandType = CommandType.StoredProcedure;
            if (feedId != null)
            {
                cmd.Parameters.Add(DataLayer.CreateParameter("@FeedId", DbType.Int32, feedId, mstrConnectString));
            }

            DataSet ds = DataLayer.GetDataSet(cmd, mstrConnectString);

            // Set table names, just for information
            ds.Tables[0].TableName = "TB_Feed";
            ds.Tables[1].TableName = "TB_FeedKeyValue";
            ds.Tables[2].TableName = "TB_FeedTransformation";
            ds.Tables[3].TableName = "TB_FeedFilter";

            List<FeedState> lstFeeds = (from dr in ds.Tables["TB_Feed"].AsEnumerable()
                                        select new State.FeedState()
                                        {
                                            ID = Convert.ToInt32(dr[obj.Schema.FeedId]),
                                            Name = dr[obj.Schema.Name].ToString(),
                                            Type = (FeedTypeEnum)dr[obj.Schema.FeedTypeId],
                                        }).ToList();



            State.KeyValueState objKV = new State.KeyValueState();
            State.FeedTransformationState objTR = new State.FeedTransformationState();
            State.FeedFilterState objFL = new State.FeedFilterState();
            foreach (FeedState feed in lstFeeds)
            {

                feed.KeyValues = (from dr in ds.Tables["TB_FeedKeyValue"].AsEnumerable()
                                  where dr.Field<int>("FeedId") == feed.ID
                                  select new State.KeyValueState()
                                  {
                                      KeyValueId = Convert.ToInt32(dr[objKV.Schema.Id]),
                                      FeedId = Convert.ToInt32(dr[objKV.Schema.FeedId]),
                                      FeedSpecific = Convert.ToBoolean(dr[objKV.Schema.Id]),
                                      Key = Convert.ToString(dr[objKV.Schema.Key]),
                                      Value = Convert.ToString(dr[objKV.Schema.Value])
                                  }).ToList();

                feed.Transformations = (from dr in ds.Tables["TB_FeedTransformation"].AsEnumerable()
                                        where dr.Field<int>("FeedId") == feed.ID
                                        select new State.FeedTransformationState()
                                        {
                                            TransformationId = Convert.ToInt32(dr[objTR.Schema.TransformationId]),
                                            FeedId = Convert.ToInt32(dr[objTR.Schema.FeedId]),
                                            ExecuteOrder = Convert.ToInt32(dr[objTR.Schema.ExecuteOrder]),
                                            OriginalValue = Convert.ToString(dr[objTR.Schema.OriginalValue]),
                                            OriginalColumn = Convert.ToString(dr[objTR.Schema.OriginalColumn]),
                                            NewValue = Convert.ToString(dr[objTR.Schema.NewValue]),
                                            NewColumn = Convert.ToString(dr[objTR.Schema.NewColumn])
                                        }).ToList();

                feed.Filter = (from dr in ds.Tables["TB_FeedFilter"].AsEnumerable()
                               where dr.Field<int>("FeedId") == feed.ID
                               select new State.FeedFilterState()
                               {
                                   Id = Convert.ToInt32(dr[objFL.Schema.Id]),
                                   FeedId = Convert.ToInt32(dr[objFL.Schema.FeedId]),
                                   ColumnName = Convert.ToString(dr[objFL.Schema.ColumnName]),
                                   ColumnValue = Convert.ToString(dr[objFL.Schema.ColumnValue])
                               }).ToList();
            }

            return lstFeeds;
        }

        #endregion

        #region Feed Key Value Retrival
        public List<KeyValueState> GetFeedKeyValues(int feedId)
        {
            IDbCommand cmd;
            State.KeyValueState obj = new State.KeyValueState();

            string strSQL;
            strSQL = "procGetKeyValue";

            cmd = DataLayer.CreateCommand(strSQL, mstrConnectString);
            cmd.Parameters.Add(DataLayer.CreateParameter("@FeedId", DbType.Int32, feedId, mstrConnectString));
            cmd.CommandType = CommandType.StoredProcedure;

            DataSet ds = DataLayer.GetDataSet(cmd, mstrConnectString);

            List<KeyValueState> lst = (from dr in ds.Tables[0].AsEnumerable()
                                       select new State.KeyValueState()
                                       {
                                           KeyValueId = Convert.ToInt32(dr[obj.Schema.Id]),
                                           FeedId = Convert.ToInt32(dr[obj.Schema.FeedId]),
                                           Key = Convert.ToString(dr[obj.Schema.Key]),
                                           Value = Convert.ToString(dr[obj.Schema.Value]),
                                           FeedSpecific = Convert.ToBoolean(dr[obj.Schema.FeedSpecific])
                                       }).ToList();

            return lst;
        }

        public KeyValueState GetKeyValueById(int keyValueId)
        {
            IDbCommand cmd;
            State.KeyValueState obj = new State.KeyValueState();

            string strSQL;
            strSQL = "procGetKeyValueById";

            cmd = DataLayer.CreateCommand(strSQL, mstrConnectString);
            cmd.Parameters.Add(DataLayer.CreateParameter("@KeyValueId", DbType.Int32, keyValueId, mstrConnectString));
            cmd.CommandType = CommandType.StoredProcedure;

            DataSet ds = DataLayer.GetDataSet(cmd, mstrConnectString);

            List<KeyValueState> lst = (from dr in ds.Tables[0].AsEnumerable()
                                       select new State.KeyValueState()
                                       {
                                           KeyValueId = Convert.ToInt32(dr[obj.Schema.Id]),
                                           FeedId = Convert.ToInt32(dr[obj.Schema.FeedId]),
                                           Key = Convert.ToString(dr[obj.Schema.Key]),
                                           Value = Convert.ToString(dr[obj.Schema.Value]),
                                           FeedSpecific = Convert.ToBoolean(dr[obj.Schema.FeedSpecific])
                                       }).ToList();

            return lst[0];
        }
        #endregion

        #region Feed Filter Retrival
        public List<FeedFilterState> GetFeedFilters(int feedId)
        {
            IDbCommand cmd;
            Tebaldi.MarketData.Models.State.FeedFilterState obj = new State.FeedFilterState();

            string strSQL;
            strSQL = "procGetFilter";

            cmd = DataLayer.CreateCommand(strSQL, mstrConnectString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(DataLayer.CreateParameter("@FeedId", DbType.Int32, feedId, mstrConnectString));

            DataSet ds = DataLayer.GetDataSet(cmd, mstrConnectString);

            List<FeedFilterState> lstFilter = (from dr in ds.Tables[0].AsEnumerable()
                                               select new State.FeedFilterState()
                                               {
                                                   Id = Convert.ToInt32(dr[obj.Schema.Id]),
                                                   FeedId = Convert.ToInt32(dr[obj.Schema.FeedId]),
                                                   ColumnName = Convert.ToString(dr[obj.Schema.ColumnName]),
                                                   ColumnValue = Convert.ToString(dr[obj.Schema.ColumnValue])
                                               }).ToList();
            return lstFilter;
        }

        public FeedFilterState GetFilterById(int filterId)
        {
            IDbCommand cmd;
            Tebaldi.MarketData.Models.State.FeedFilterState obj = new State.FeedFilterState();

            string strSQL;
            strSQL = "procGetFilterById";

            cmd = DataLayer.CreateCommand(strSQL, mstrConnectString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(DataLayer.CreateParameter("@FilterId", DbType.Int32, filterId, mstrConnectString));

            DataSet ds = DataLayer.GetDataSet(cmd, mstrConnectString);

            List<FeedFilterState> lstFilter = (from dr in ds.Tables[0].AsEnumerable()
                                               select new State.FeedFilterState()
                                               {
                                                   Id = Convert.ToInt32(dr[obj.Schema.Id]),
                                                   FeedId = Convert.ToInt32(dr[obj.Schema.FeedId]),
                                                   ColumnName = Convert.ToString(dr[obj.Schema.ColumnName]),
                                                   ColumnValue = Convert.ToString(dr[obj.Schema.ColumnValue])
                                               }).ToList();
            return lstFilter[0];
        }
        #endregion

        #region Feed Transformation Retrival
        public List<FeedTransformationState> GetFeedTransformations(int feedId)
        {
            IDbCommand cmd;
            Tebaldi.MarketData.Models.State.FeedTransformationState obj = new State.FeedTransformationState();

            string strSQL;
            strSQL = "procGetFeedTransformations";

            cmd = DataLayer.CreateCommand(strSQL, mstrConnectString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(DataLayer.CreateParameter("@FeedId", DbType.Int32, feedId, mstrConnectString));

            DataSet ds = DataLayer.GetDataSet(cmd, mstrConnectString);

            List<FeedTransformationState> lstFeedTransf = (from dr in ds.Tables[0].AsEnumerable()
                                                           select new State.FeedTransformationState()
                                                           {
                                                               OriginalColumn = Convert.ToString(dr[obj.Schema.OriginalColumn]),
                                                               OriginalValue = Convert.ToString(dr[obj.Schema.OriginalValue]),
                                                               NewColumn = Convert.ToString(dr[obj.Schema.NewColumn]),
                                                               NewValue = Convert.ToString(dr[obj.Schema.NewValue]),
                                                               ExecuteOrder = Convert.ToInt32(dr[obj.Schema.ExecuteOrder]),
                                                               FeedId = Convert.ToInt32(dr[obj.Schema.FeedId]),
                                                               TransformationId = Convert.ToInt32(dr[obj.Schema.TransformationId])
                                                           }).ToList();

            return lstFeedTransf;
        }

        public FeedTransformationState GetTransformationById(int transformationId)
        {
            IDbCommand cmd;
            Tebaldi.MarketData.Models.State.FeedTransformationState obj = new State.FeedTransformationState();

            string strSQL;
            strSQL = "procGetTransformationsById";

            cmd = DataLayer.CreateCommand(strSQL, mstrConnectString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(DataLayer.CreateParameter("@TransformationdId", DbType.Int32, transformationId, mstrConnectString));

            DataSet ds = DataLayer.GetDataSet(cmd, mstrConnectString);

            List<FeedTransformationState> lst = (from dr in ds.Tables[0].AsEnumerable()
                                                 select new State.FeedTransformationState()
                                                 {
                                                     TransformationId = Convert.ToInt32(dr[obj.Schema.TransformationId]),
                                                     FeedId = Convert.ToInt32(dr[obj.Schema.FeedId]),
                                                     ExecuteOrder = Convert.ToInt32(dr[obj.Schema.ExecuteOrder]),

                                                     OriginalColumn = Convert.ToString(dr[obj.Schema.OriginalColumn]),
                                                     OriginalValue = Convert.ToString(dr[obj.Schema.OriginalValue]),

                                                     NewColumn = Convert.ToString(dr[obj.Schema.NewColumn]),
                                                     NewValue = Convert.ToString(dr[obj.Schema.NewValue])
                                                 }).ToList();
            return lst[0];
        }
        #endregion

        #region Feed Type
        public List<FeedTypeDefaultKeyState> GetFeedTypeDefaults(int? feedTypeId)
        {
            IDbCommand cmd;
            State.FeedTypeDefaultKeyState obj = new State.FeedTypeDefaultKeyState();

            string strSQL;
            strSQL = "procGetFeedTypeDefaultValues";

            cmd = DataLayer.CreateCommand(strSQL, mstrConnectString);
            if (feedTypeId != null)
            {
                cmd.Parameters.Add(DataLayer.CreateParameter("@FeedTypeId", DbType.Int32, feedTypeId, mstrConnectString));
            }

            cmd.CommandType = CommandType.StoredProcedure;

            DataSet ds = DataLayer.GetDataSet(cmd, mstrConnectString);

            List<FeedTypeDefaultKeyState> lstFeeds = (from dr in ds.Tables[0].AsEnumerable()
                                                      select new State.FeedTypeDefaultKeyState()
                                                      {
                                                          ID = Convert.ToInt32(dr[obj.Schema.Id]),
                                                          FeedTypeId = Convert.ToInt32(dr[obj.Schema.FeedTypeId]),
                                                          Key = Convert.ToString(dr[obj.Schema.Key])
                                                      }).ToList();

            return lstFeeds;
        }

        #endregion

        #endregion

        #region "Data Modification Methods"

        #region Feed State Modification

        public int Save(FeedState feed)
        {
            List<FeedState> lstfeed = new List<FeedState>();
            lstfeed.Add(feed);

            return Save(lstfeed);
        }

        public int Save(List<FeedState> lstFfeed)
        {
            IDbCommand cmd;
            string strSQL;

            // Check Business Rules
            foreach (FeedState feed in lstFfeed)
            {
                Validate(feed);
            }

            strSQL = "procGravaFeedInfo";

            cmd = DataLayer.CreateCommand(strSQL, mstrConnectString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(DataLayer.CreateParameter("@FeedInfoXML", DbType.String, ParseToXml(lstFfeed), mstrConnectString));

            return DataLayer.ExecuteSQL(cmd);
        }

        public virtual void Validate(FeedState item)
        {
            string strMsg = string.Empty;

            if (item.ID < 0)
            { strMsg += "Feedi Id nao pode ser menor ou igual a zero." + Environment.NewLine; }

            if (String.IsNullOrEmpty(item.Name))
            { strMsg += "Feed Name nao pode ser nulo." + Environment.NewLine; }

            if (strMsg != string.Empty)
            { throw new TebaldiMarketDataException(strMsg); }
        }

        protected string ParseToXml(List<FeedState> lstFfeed)
        {
            XmlDocument xmlDoc = new XmlDocument();
            int nodeContador = 1;

            XmlElement root = xmlDoc.CreateElement("ROOT");
            xmlDoc.AppendChild(root);

            foreach (FeedState feed in lstFfeed)
            {
                XmlElement xmlObj = xmlDoc.CreateElement("FeedInfo");
                xmlObj.SetAttribute("NodeId", nodeContador.ToString());
                xmlObj.SetAttribute("FeedId", feed.ID.ToString());
                xmlObj.SetAttribute("Name", feed.Name.ToString());

                xmlObj.SetAttribute("FeedTypeId", ((int)feed.Type).ToString());

                root.AppendChild(xmlObj);

                nodeContador++;
            }

            return xmlDoc.OuterXml;
        }

        public int Delete(FeedState feed)
        {
            List<FeedState> lst = new List<FeedState>();
            lst.Add(feed);

            return Delete(lst);
        }

        public int Delete(List<FeedState> FeedList)
        {
            IDbCommand cmd;
            string strSQL;

            strSQL = "procDeleteFeed";

            cmd = DataLayer.CreateCommand(strSQL, mstrConnectString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(DataLayer.CreateParameter("@FeedInfoXML", DbType.String, ParseToXml(FeedList), mstrConnectString));

            return DataLayer.ExecuteSQL(cmd);
        }

        #endregion

        #region Feed Filter Modification

        public int Save(FeedFilterState feedFilter)
        {
            List<FeedFilterState> lst = new List<FeedFilterState>();
            lst.Add(feedFilter);
            return Save(lst);
        }

        public int Save(List<FeedFilterState> lstFeedFilter)
        {
            IDbCommand cmd;
            string strSQL;

            // Check Business Rules
            foreach (State.FeedFilterState filter in lstFeedFilter)
            {
                Validate(filter);
            }

            strSQL = "procGravaFeedFilter";

            cmd = DataLayer.CreateCommand(strSQL, mstrConnectString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(DataLayer.CreateParameter("@filterXML", DbType.String, ParseToXml(lstFeedFilter), mstrConnectString));

            return DataLayer.ExecuteSQL(cmd);
        }

        public virtual void Validate(FeedFilterState item)
        {
            string strMsg = string.Empty;

            if (item.FeedId <= 0)
            { strMsg += "Feedi Id nao pode ser menor ou igual a zero." + Environment.NewLine; }

            if (String.IsNullOrEmpty(item.ColumnName))
            { strMsg += "Column Value nao pode ser nulo." + Environment.NewLine; }

            if (String.IsNullOrEmpty(item.ColumnName))
            { strMsg += "Column Name nao pode ser nulo." + Environment.NewLine; }

            if (strMsg != string.Empty)
            { throw new TebaldiMarketDataException(strMsg); }
        }

        protected string ParseToXml(List<State.FeedFilterState> lstFeedFilter)
        {
            XmlDocument xmlDoc = new XmlDocument();
            int nodeContador = 1;

            XmlElement root = xmlDoc.CreateElement("ROOT");
            xmlDoc.AppendChild(root);

            foreach (State.FeedFilterState filter in lstFeedFilter)
            {
                XmlElement xmlObj = xmlDoc.CreateElement("Filter");
                xmlObj.SetAttribute("NodeId", nodeContador.ToString());
                xmlObj.SetAttribute("FilterId", filter.Id.ToString());
                xmlObj.SetAttribute("FeedId", filter.FeedId.ToString());

                xmlObj.SetAttribute("ColumnName", filter.ColumnName);
                xmlObj.SetAttribute("ColumnValue", filter.ColumnValue);

                root.AppendChild(xmlObj);

                nodeContador++;
            }

            return xmlDoc.OuterXml;
        }

        public int Delete(FeedFilterState filterState)
        {
            IDbCommand cmd;
            string strSQL;

            strSQL = "procDeleteFilterById";

            cmd = DataLayer.CreateCommand(strSQL, mstrConnectString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(DataLayer.CreateParameter("@FilterId", DbType.Int32, filterState.Id, mstrConnectString));

            return DataLayer.ExecuteSQL(cmd);
        }

        #endregion

        #region Feed Transformation Modification

        public int Save(State.FeedTransformationState feedTransformation)
        {
            List<State.FeedTransformationState> lstfeedTransformation = new List<FeedTransformationState>();
            lstfeedTransformation.Add(feedTransformation);

            return Save(lstfeedTransformation);
        }

        public int Save(List<FeedTransformationState> feedTransformation)
        {
            IDbCommand cmd;
            string strSQL;

            // Check Business Rules
            foreach (State.FeedTransformationState trans in feedTransformation)
            {
                Validate(trans);
            }

            strSQL = "procGravaFeedTransformation";

            cmd = DataLayer.CreateCommand(strSQL, mstrConnectString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(DataLayer.CreateParameter("@TransformationXML", DbType.String, ParseToXml(feedTransformation), mstrConnectString));

            return DataLayer.ExecuteSQL(cmd);
        }

        public virtual void Validate(FeedTransformationState item)
        {
            string strMsg = string.Empty;

            if (item.FeedId <= 0)
            { strMsg += "Feedi Id nao pode ser menor ou igual a zero." + Environment.NewLine; }

            if (String.IsNullOrEmpty(item.OriginalValue))
            { strMsg += "Original Value nao pode ser nulo." + Environment.NewLine; }

            if (String.IsNullOrEmpty(item.OriginalColumn))
            { strMsg += "Original Column nao pode ser nulo." + Environment.NewLine; }

            if (String.IsNullOrEmpty(item.NewValue))
            { strMsg += "New Value nao pode ser nulo." + Environment.NewLine; }

            if (String.IsNullOrEmpty(item.NewColumn))
            { strMsg += "New Column nao pode ser nulo." + Environment.NewLine; }

            if (strMsg != string.Empty)
            { throw new TebaldiMarketDataException(strMsg); }
        }

        protected string ParseToXml(List<State.FeedTransformationState> lstFeedTransformation)
        {
            XmlDocument xmlDoc = new XmlDocument();
            int nodeContador = 1;

            XmlElement root = xmlDoc.CreateElement("ROOT");
            xmlDoc.AppendChild(root);

            foreach (State.FeedTransformationState map in lstFeedTransformation)
            {
                XmlElement xmlObj = xmlDoc.CreateElement("Transformation");
                xmlObj.SetAttribute("NodeId", nodeContador.ToString());
                xmlObj.SetAttribute("TransformationId", map.TransformationId.ToString());
                xmlObj.SetAttribute("FeedId", map.FeedId.ToString());

                xmlObj.SetAttribute("ExecuteOrder", map.ExecuteOrder.ToString());

                xmlObj.SetAttribute("OriginalValue", map.OriginalValue);
                xmlObj.SetAttribute("OriginalColumn", map.OriginalColumn);


                xmlObj.SetAttribute("NewValue", map.NewValue);
                xmlObj.SetAttribute("NewColumn", map.NewColumn);

                root.AppendChild(xmlObj);

                nodeContador++;
            }

            return xmlDoc.OuterXml;
        }

        public int Delete(FeedTransformationState transform)
        {
            IDbCommand cmd;
            string strSql;

            strSql = "DELETE FROM " + transform.Schema.ObjectName + " WHERE " + transform.Schema.TransformationId + "=" + transform.TransformationId.ToString();

            cmd = DataLayer.CreateCommand(strSql, mstrConnectString);
            cmd.CommandType = CommandType.Text;

            return DataLayer.ExecuteSQL(cmd);
        }

        #endregion

        #region Feed Key Value Modification

        public int Delete(KeyValueState keyValueState)
        {
            IDbCommand cmd;
            string strSQL;

            strSQL = "procDeleteKeyValueById";

            cmd = DataLayer.CreateCommand(strSQL, mstrConnectString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(DataLayer.CreateParameter("@KeyValueId", DbType.Int32, keyValueState.KeyValueId, mstrConnectString));

            return DataLayer.ExecuteSQL(cmd);
        }

        public int Save(State.KeyValueState keyValueState)
        {
            List<State.KeyValueState> lst = new List<KeyValueState>();
            lst.Add(keyValueState);

            return Save(lst);
        }

        public int Save(List<KeyValueState> lstKeyValueState)
        {
            IDbCommand cmd;
            string strSQL;

            // Check Business Rules
            foreach (State.KeyValueState item in lstKeyValueState)
            {
                Validate(item);
            }

            strSQL = "procGravaKeyValue";

            cmd = DataLayer.CreateCommand(strSQL, mstrConnectString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(DataLayer.CreateParameter("@keyValueXML", DbType.String, ParseToXml(lstKeyValueState), mstrConnectString));

            return DataLayer.ExecuteSQL(cmd);
        }

        public virtual void Validate(KeyValueState item)
        {
            string strMsg = string.Empty;

            if (item.FeedId <= 0)
            { strMsg += "Feedi Id nao pode ser menor ou igual a zero." + Environment.NewLine; }

            if (String.IsNullOrEmpty(item.Key))
            { strMsg += "Campo Chave nao pode ser vazio." + Environment.NewLine; }

            if (String.IsNullOrEmpty(item.Value))
            { strMsg += "Campo Valor nao pode ser nulo." + Environment.NewLine; }

            if (strMsg != string.Empty)
            { throw new TebaldiMarketDataException(strMsg); }
        }

        protected string ParseToXml(List<State.KeyValueState> lstkeyValue)
        {
            XmlDocument xmlDoc = new XmlDocument();
            int nodeContador = 1;

            XmlElement root = xmlDoc.CreateElement("ROOT");
            xmlDoc.AppendChild(root);

            foreach (State.KeyValueState item in lstkeyValue)
            {
                XmlElement xmlObj = xmlDoc.CreateElement("KeyValue");
                xmlObj.SetAttribute("NodeId", nodeContador.ToString());
                xmlObj.SetAttribute("KeyValueId", item.KeyValueId.ToString());
                xmlObj.SetAttribute("FeedId", item.FeedId.ToString());

                xmlObj.SetAttribute("Chave", item.Key);
                xmlObj.SetAttribute("Valor", item.Value);
                xmlObj.SetAttribute("FeedSpecific", item.FeedSpecific.ToString());

                root.AppendChild(xmlObj);

                nodeContador++;
            }

            return xmlDoc.OuterXml;
        }

        #endregion

        #endregion

    }
}
