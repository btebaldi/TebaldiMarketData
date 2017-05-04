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
        public List<FeedState> GetFeeds()
        {
            IDbCommand cmd;
            State.FeedState obj = new State.FeedState();

            string strSQL;
            strSQL = "procGetFeed";

            cmd = DataLayer.CreateCommand(strSQL, mstrConnectString);
            cmd.CommandType = CommandType.StoredProcedure;

            DataSet ds = DataLayer.GetDataSet(cmd, mstrConnectString);

            List<FeedState> lstFeeds = (from dr in ds.Tables[0].AsEnumerable()
                                        select new State.FeedState()
                                        {
                                            ID = Convert.ToInt32(dr[obj.Schema.FeedId]),
                                            Name = dr[obj.Schema.Name].ToString(),
                                            Type = (FeedTypeEnum)Enum.Parse(typeof(FeedTypeEnum), dr[obj.Schema.FeedType].ToString()),
                                            Active = Convert.ToBoolean(dr[obj.Schema.Active])
                                        }).ToList();

            return lstFeeds;
        }

        public FeedState GetFeed(int Id)
        {
            return GetFeeds().Find(f => f.ID == Id);
        }

        // ToDo: Apagar
        /*
        private void LoadMapping(List<FeedState> feedList)
        {
            if (feedList.Count > 0)
            {
                string strSql = "";
                State.FeedMappingState obj = new State.FeedMappingState();
                strSql = "SELECT * FROM " + obj.Schema.ObjectName + " WHERE " + obj.Schema.FeedId + " in (-1";
                foreach (FeedState feed in feedList)
                {
                    strSql += ", " + feed.ID.ToString();
                }
                strSql += ")";

                DataSet ds = DataLayer.GetDataSet(strSql, mstrConnectString);


                foreach (FeedState feed in feedList)
                {
                    feed.ColumnMapping = (from dr in ds.Tables[0].AsEnumerable()
                                          select new State.FeedMappingState()
                                          {
                                              MappingId = Convert.ToInt32(dr[obj.Schema.MappingId]),
                                              FeedId = Convert.ToInt32(dr[obj.Schema.FeedId]),
                                              ColumnIndex = dr[obj.Schema.ColumnIndex] == DBNull.Value ? 0 : Convert.ToInt32(dr[obj.Schema.ColumnIndex]),
                                              ColumnName = dr[obj.Schema.ColumnName].ToString(),
                                              StaticValue = dr[obj.Schema.StaticValue].ToString(),
                                              Type = Type.GetType(dr[obj.Schema.Type].ToString()),
                                              DateTimeParseMask = dr[obj.Schema.DateTimeParseMask].ToString(),
                                              Culture = new System.Globalization.CultureInfo(dr[obj.Schema.Culture].ToString()),
                                              Destination = dr[obj.Schema.Destination].ToString()
                                          }).Where(c => c.FeedId == feed.ID).ToList();
                }
            }

        }


        private void LoadTransformations(List<FeedState> feedList)
        {
            if (feedList.Count > 0)
            {
                string strSql = "";
                State.FeedTransformationState obj = new State.FeedTransformationState();
                strSql = "SELECT * FROM " + obj.Schema.ObjectName + " WHERE " + obj.Schema.FeedId + " in (-1";
                foreach (FeedState feed in feedList)
                {
                    strSql += ", " + feed.ID.ToString();
                }
                strSql += ")";

                DataSet ds = DataLayer.GetDataSet(strSql, mstrConnectString);

                foreach (FeedState feed in feedList)
                {
                    feed.Transformations = (from dr in ds.Tables[0].AsEnumerable()
                                            select new State.FeedTransformationState()
                                            {
                                                TransformationId = Convert.ToInt32(dr[obj.Schema.TransformationId]),
                                                FeedId = Convert.ToInt32(dr[obj.Schema.FeedId]),

                                                ExecuteOrder = Convert.ToInt32(dr[obj.Schema.ExecuteOrder]),

                                                OriginalColumn = Convert.ToString(dr[obj.Schema.OriginalColumn]),
                                                OriginalValue = Convert.ToString(dr[obj.Schema.OriginalValue]),


                                                NewColumn = Convert.ToString(dr[obj.Schema.NewColumn]),
                                                NewValue = Convert.ToString(dr[obj.Schema.NewValue]),

                                            }).Where(c => c.FeedId == feed.ID).ToList();
                }
            }

        }
        */

        public List<FeedFilterState> GetFilterByFeedId(int feedId)
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
                                                   FeedId = Convert.ToInt32(dr[obj.Schema.FeedId]),
                                                   ColumnName = Convert.ToString(dr[obj.Schema.ColumnName]),
                                                   ColumnValue = Convert.ToString(dr[obj.Schema.ColumnValue])
                                               }).ToList();
            return lstFilter;
        }

        public List<FeedTransformationState> GetTransformationByFeedId(int feedId)
        {
            IDbCommand cmd;
            Tebaldi.MarketData.Models.State.FeedTransformationState obj = new State.FeedTransformationState();

            string strSQL;
            strSQL = "procGetTransformations";

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

        public List<KeyValueState> GetKeyValuesById(int feedId)
        {
            IDbCommand cmd;
            Tebaldi.MarketData.Models.State.KeyValueState obj = new State.KeyValueState();

            string strSQL;
            strSQL = "procGetKeyValue";

            cmd = DataLayer.CreateCommand(strSQL, mstrConnectString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(DataLayer.CreateParameter("@FeedId", DbType.Int32, feedId, mstrConnectString));

            DataSet ds = DataLayer.GetDataSet(cmd, mstrConnectString);

            List<KeyValueState> lstKeyValue = (from dr in ds.Tables[0].AsEnumerable()
                                               select new State.KeyValueState()
                                               {
                                                   KeyValueId = Convert.ToInt32(dr[obj.Schema.Id]),
                                                   FeedId = Convert.ToInt32(dr[obj.Schema.FeedId]),
                                                   Key = Convert.ToString(dr[obj.Schema.Key]),
                                                   Value = Convert.ToString(dr[obj.Schema.Value]),
                                                   FeedSprecific = Convert.ToBoolean(dr[obj.Schema.FeedSpecific])
                                               }).ToList();

            return lstKeyValue;
        }

        #endregion

        #region "Data Modification Methods"

        #region Feed Mapping Modification

        //TODO apagar

        //public int SaveFeedMapping(State.FeedMappingState feedMapping)
        //{
        //    List<State.FeedMappingState> lstfeedMapping = new List<FeedMappingState>();
        //    lstfeedMapping.Add(feedMapping);

        //    return SaveFeedMapping(lstfeedMapping);
        //}

        //TODO Apagar
        //public int SaveFeedMapping(List<State.FeedMappingState> lstFeedMapping)
        //{
        //    IDbCommand cmd;
        //    string strSQL;

        //    // Check Business Rules
        //    foreach (State.FeedMappingState map in lstFeedMapping)
        //    {
        //        Validate(map);
        //    }

        //    strSQL = "procGravaFeedMapping";

        //    cmd = DataLayer.CreateCommand(strSQL, mstrConnectString);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add(DataLayer.CreateParameter("@mappingXML", DbType.String, ParseToXml(lstFeedMapping), mstrConnectString));

        //    return DataLayer.ExecuteSQL(cmd);
        //}

        // ToDo: Apagar
        //public int DeleteFeedMapping(FeedMappingState feedMapping)
        //{
        //    IDbCommand cmd;
        //    string strSql;

        //    strSql = "DELETE FROM " + feedMapping.Schema.ObjectName + " WHERE " + feedMapping.Schema.MappingId + "=" + feedMapping.MappingId.ToString();

        //    cmd = DataLayer.CreateCommand(strSql, mstrConnectString);
        //    cmd.CommandType = CommandType.Text;

        //    return DataLayer.ExecuteSQL(cmd);
        //}


        //TODO apagar

        //protected string ParseToXml(List<State.FeedMappingState> lstFeedMapping)
        //{
        //    XmlDocument xmlDoc = new XmlDocument();
        //    int nodeContador = 1;

        //    XmlElement root = xmlDoc.CreateElement("ROOT");
        //    xmlDoc.AppendChild(root);

        //    foreach (State.FeedMappingState map in lstFeedMapping)
        //    {
        //        XmlElement xmlObj = xmlDoc.CreateElement("Mapping");
        //        xmlObj.SetAttribute("NodeId", nodeContador.ToString());
        //        xmlObj.SetAttribute("MappingId", map.MappingId.ToString());
        //        xmlObj.SetAttribute("FeedId", map.FeedId.ToString());

        //        if (map.ColumnIndex > 0)
        //        { xmlObj.SetAttribute("ColumnIndex", map.ColumnIndex.ToString()); }

        //        if (!String.IsNullOrEmpty(map.ColumnName))
        //        { xmlObj.SetAttribute("ColumnName", map.ColumnName); }

        //        if (!String.IsNullOrEmpty(map.StaticValue))
        //        { xmlObj.SetAttribute("StaticValue", map.StaticValue); }

        //        xmlObj.SetAttribute("Type", map.Type.FullName);

        //        if (!String.IsNullOrEmpty(map.StaticValue))
        //        { xmlObj.SetAttribute("DateTimeParseMask", map.DateTimeParseMask); }

        //        xmlObj.SetAttribute("Culture", map.Culture.Name);
        //        xmlObj.SetAttribute("Destination", map.Destination);

        //        root.AppendChild(xmlObj);

        //        nodeContador++;
        //    }

        //    return xmlDoc.OuterXml;
        //}


            //TODO apagar
        //public virtual void Validate(FeedMappingState item)
        //{
        //    string strMsg = string.Empty;

        //    if (item.FeedId <= 0)
        //    { strMsg += "Feedi Id nao pode ser menor ou igual a zero." + Environment.NewLine; }

        //    if (item.ColumnIndex < 0)
        //    { strMsg += "Column Index nao pode ser menor que zero." + Environment.NewLine; }

        //    if (item.Type == null)
        //    { strMsg += "O Tipo (Type) do campo nao pode ser nulo." + Environment.NewLine; }

        //    if (item.Culture == null)
        //    { strMsg += "A cultura do campo nao pode ser nula" + Environment.NewLine; }

        //    if (strMsg != string.Empty)
        //    { throw new TebaldiMarketDataException(strMsg); }
        //}


        #endregion

        #region Feed Transformation Modification

        public int SaveFeedTransformation(State.FeedTransformationState feedTransformation)
        {
            List<State.FeedTransformationState> lstfeedTransformation = new List<FeedTransformationState>();
            lstfeedTransformation.Add(feedTransformation);

            return SaveFeedTransformation(lstfeedTransformation);
        }

        public int SaveFeedTransformation(List<FeedTransformationState> feedTransformation)
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

        public int DeleteFeedTransformation(FeedTransformationState transform)
        {
            IDbCommand cmd;
            string strSql;

            strSql = "DELETE FROM " + transform.Schema.ObjectName + " WHERE " + transform.Schema.TransformationId + "=" + transform.TransformationId.ToString();

            cmd = DataLayer.CreateCommand(strSql, mstrConnectString);
            cmd.CommandType = CommandType.Text;

            return DataLayer.ExecuteSQL(cmd);
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

        #endregion

        #region Feed State Modification

        protected int SaveFeedInfo(FeedState feed)
        {
            List<FeedState> lstfeed = new List<FeedState>();
            lstfeed.Add(feed);

            return SaveFeedInfo(lstfeed);
        }

        public int SaveFeedInfo(List<FeedState> lstFfeed)
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

            //if (item.Type == null)
            //{ strMsg += "O tipo de feed deve ser definido." + Environment.NewLine; }

            // ToDo: Apagar
            //if (item.Uri == null)
            //{ strMsg += "O feed deve ter uma origem." + Environment.NewLine; }

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

                xmlObj.SetAttribute("FeedType", feed.Type.ToString());

                // ToDo: Apagar
                //xmlObj.SetAttribute("Origem", feed.Uri.AbsoluteUri);
                //xmlObj.SetAttribute("FileMask", feed.FileMask);

                xmlObj.SetAttribute("Active", feed.Active.ToString());

                root.AppendChild(xmlObj);

                nodeContador++;
            }

            return xmlDoc.OuterXml;
        }

        public int DeleteFeedState(List<FeedState> FeedList)
        {
            int retorno = 0;

            if (FeedList.Count > 0)
            {

                string strSql = "";

                strSql = "DELETE FROM " + FeedList[0].Schema.ObjectName + " WHERE " + FeedList[0].Schema.FeedId + " in (-1";

                foreach (FeedState feed in FeedList)
                { strSql += ", " + feed.Schema.FeedId; }

                strSql += ") ";

                IDbCommand cmd;
                cmd = DataLayer.CreateCommand(strSql, mstrConnectString);
                cmd.CommandType = CommandType.Text;

                retorno = DataLayer.ExecuteSQL(cmd);
            }

            return retorno;
        }

        public int DeleteFeedState(FeedState feed)
        {
            string strSql = "";

            strSql = "DELETE FROM " + feed.Schema.ObjectName + " WHERE " + feed.Schema.FeedId + "=" + feed.ID.ToString();

            IDbCommand cmd;
            cmd = DataLayer.CreateCommand(strSql, mstrConnectString);
            cmd.CommandType = CommandType.Text;

            return DataLayer.ExecuteSQL(cmd);
        }

        #endregion

        #endregion

    }
}
