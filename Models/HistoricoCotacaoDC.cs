﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using Tebaldi.MarketData.Models.State;
using System.Data;
using DataCommon;

namespace Tebaldi.MarketData.Models
{
    public class HistoricoCotacaoDC
    {
        private string mstrConnectString;
        public string ConnectString
        {
            get { return mstrConnectString; }
            set { mstrConnectString = value; }
        }

        #region "Data Retrieval Methods"
        public List<ExtIdMapState> GetIdMapping()
        {
            return GetIdMappings(null);
        }

        public ExtIdMapState GetIdMapping(int id)
        {
            ExtIdMapState obj = GetIdMappings(id).Find(f => f.Id == id);
            return obj;
        }

        private List<ExtIdMapState> GetIdMappings(int? id)
        {
            IDbCommand cmd;
            string strSQL;
            Tebaldi.MarketData.Models.State.ExtIdMapState obj = new ExtIdMapState();


            strSQL = "procGetExtIdMap";

            cmd = DataLayer.CreateCommand(strSQL, mstrConnectString);
            cmd.CommandType = CommandType.StoredProcedure;
            if (id != null)
            {
                cmd.Parameters.Add(DataLayer.CreateParameter("@Id", DbType.Int32, id, mstrConnectString));
            }

            DataSet ds = DataLayer.GetDataSet(cmd, mstrConnectString);


            List<ExtIdMapState> lst = (from dr in ds.Tables[0].AsEnumerable()
                                       select new State.ExtIdMapState()
                                       {
                                           Id = Convert.ToInt32(dr[obj.Schema.Id]),
                                           ExtId = Convert.ToString(dr[obj.Schema.ExtId]),
                                           TebBizAtivoId = Convert.ToInt32(dr[obj.Schema.TebBizAtivoId]),
                                           Ticker = Convert.ToString(dr[obj.Schema.Ticker])
                                       }).ToList();
            return lst;
        }

        public List<HistoricoCotacaoState> GetHistoricoCotacao()
        {
            throw new NotImplementedException();

            //List<HistoricoCotacaoState> lstCotacao = new List<HistoricoCotacaoState>();

            //string strSql = "";
            //{
            //    EmpresaState myState = new EmpresaState();
            //    strSql = "SELECT * FROM " + myState.Schema.ObjectName;
            //}

            //IDataReader dr = DataLayer.GetDataReader(strSql, mstrConnectString);

            //while (dr.Read())
            //{

            //    EmpresaState empresa = new EmpresaState();
            //    FillFromDataReader(dr, empresa);

            //    lstEmpresas.Add(empresa);
            //}

            //dr.Close();

            //return lstEmpresas;
        }

        public bool Load(HistoricoCotacaoState cotacao)
        {
            throw new NotImplementedException();

            //bool boolRet = false;
            //string strSql = "";
            //strSql = "SELECT * FROM " + empresa.Schema.ObjectName + " WHERE " + empresa.Schema.IdEmpresa + " = " + empresa.IdEmpresa;

            //IDataReader dr = DataLayer.GetDataReader(strSql, mstrConnectString);

            //while (dr.Read())
            //{
            //    if (boolRet)
            //    { throw new TebaldiBizException("Foir encontrado mais de um registro com o mesmo identificador."); }

            //    FillFromDataReader(dr, empresa);
            //    boolRet = true;
            //}

            //dr.Close();

            //return boolRet;
        }

        #endregion

        #region "Historico Cotacao Data Modification Methods"

        public virtual void Validate(HistoricoCotacaoState item)
        {
            string strMsg = string.Empty;

            if (String.IsNullOrEmpty(item.EXT_ID))
            { strMsg += "Identificador do ativo invalido." + Environment.NewLine; }

            //if (String.IsNullOrEmpty(item.CodEmpresa))
            //{ strMsg += "O Codigo da empresa deve ser preenchido." + Environment.NewLine; }

            if (String.IsNullOrEmpty(item.Ticker))
            { strMsg += "O ticker deve ser preenchido." + Environment.NewLine; }

            //if (String.IsNullOrEmpty(item.DenominacaoSocial))
            //{ strMsg += "A denominacao social da empresa deve ser preenchido." + Environment.NewLine; }

            if (strMsg != string.Empty)
            { throw new TebaldiMarketDataException(strMsg); }
        }

        public void Insert(System.Data.DataTable tblHistoricoCotacao)
        {
            if (tblHistoricoCotacao.Rows.Count > 0)
            { DataCommon.DataLayerBulkInsert.SqlServerBulkInsert(mstrConnectString, tblHistoricoCotacao); }
            //IDbCommand cmd;
            //string strSQL;

            //// Check Business Rules
            //Validate(evento);

            //strSQL = "INSERT INTO " + evento.Schema.ObjectName + "( ";
            //strSQL += evento.Schema.CodEvento + ", " + evento.Schema.Descricao + ", " + evento.Schema.IsAtivo + ", " + evento.Schema.IsDayTrade + ", " + evento.Schema.IsProvento + ", ";
            //strSQL += evento.Schema.IdTipoMercado + ") ";
            //strSQL += "VALUES (";
            //strSQL += "@" + evento.Schema.CodEvento + ", @" + evento.Schema.Descricao + ", @" + evento.Schema.IsAtivo + ", @" + evento.Schema.IsDayTrade + ", @" + evento.Schema.IsProvento + ", ";
            //strSQL += "@" + evento.Schema.IdTipoMercado + ") ";

            //cmd = DataLayer.CreateCommand(strSQL, mstrConnectString);

            //FillInParameters(evento, cmd);

            //return DataLayer.ExecuteSQL(cmd, true);
        }

        public int Update(List<HistoricoCotacaoState> empresa)
        {

            throw new NotImplementedException();

            //IDbCommand cmd;
            //string strSQL;

            //// Check Business Rules
            //Validate(mov);

            //strSQL = "UPDATE " + mov.Schema.ObjectName + " ";
            //strSQL += " SET ";

            ////strSQL += " " + mov.Schema.IdCliente + " = @" + mov.Schema.IdCliente + ",";
            ////strSQL += " " + mov.Schema.Data + " = @" + mov.Schema.Data + ",";
            ////strSQL += " " + mov.Schema.Evento + " = @" + mov.Schema.Evento + ",";
            ////strSQL += " " + mov.Schema.Ticker + " = @" + mov.Schema.Ticker + ",";

            ////strSQL += " " + mov.Schema.Quantidade + " = @" + mov.Schema.Quantidade + ",";
            ////strSQL += " " + mov.Schema.Valor + " = @" + mov.Schema.Valor + ",";
            ////strSQL += " " + mov.Schema.Tipo + " = @" + mov.Schema.Tipo + ",";
            ////strSQL += " " + mov.Schema.FluxoDeAtivo + " = @" + mov.Schema.FluxoDeAtivo + ",";
            //strSQL += " " + mov.Schema.ValorContabil + " = " + mov.ValorContabil.ToString(System.Globalization.CultureInfo.InvariantCulture) + ", ";
            ////strSQL += " " + mov.Schema.Lucro + " = @" + mov.Schema.Lucro + ",";

            //strSQL += " " + mov.Schema.DataRegistro + " = #" + DateTime.Now.ToString() + "#";
            //strSQL += " WHERE " + mov.Schema.Id + " = " + mov.Id + " ";

            //cmd = DataLayer.CreateCommand(strSQL, mstrConnectString);

            ////FillInParameters(mov, cmd);

            //return DataLayer.ExecuteSQL(cmd, true);
        }

        public int ClearImpTable()
        {
            IDbCommand cmd;
            string strSQL;

            strSQL = "procClearImpTable";

            cmd = DataLayer.CreateCommand(strSQL, mstrConnectString);
            return DataLayer.ExecuteSQL(cmd);
        }

        public int ImportImpTable()
        {
            IDbCommand cmd;
            string strSQL;

            strSQL = "procImportIMP";

            cmd = DataLayer.CreateCommand(strSQL, mstrConnectString);
            return DataLayer.ExecuteSQL(cmd);
        }

        //protected void FillInParameters(EmpresaState empresa, IDbCommand cmd)
        //{
        //    throw new NotImplementedException();

        //    //cmd.Parameters.Add(DataLayer.CreateParameter("@" + mov.Schema.IdCliente, DbType.Int32, mov.IdCliente));
        //    //cmd.Parameters.Add(DataLayer.CreateParameter("@" + mov.Schema.Data, DbType.DateTime, mov.Data));
        //    //cmd.Parameters.Add(DataLayer.CreateParameter("@" + mov.Schema.Evento, DbType.String, mov.Evento));
        //    //cmd.Parameters.Add(DataLayer.CreateParameter("@" + mov.Schema.Ticker, DbType.String, mov.Ticker));
        //    //cmd.Parameters.Add(DataLayer.CreateParameter("@" + mov.Schema.Quantidade, DbType.Int32, mov.Quantidade));
        //    //cmd.Parameters.Add(DataLayer.CreateParameter("@" + mov.Schema.Valor, DbType.Decimal, mov.Valor));
        //    //cmd.Parameters.Add(DataLayer.CreateParameter("@" + mov.Schema.Tipo, DbType.Int32, mov.Tipo));
        //    //cmd.Parameters.Add(DataLayer.CreateParameter("@" + mov.Schema.FluxoDeAtivo, DbType.Int32, mov.FluxoDeAtivo));
        //    //cmd.Parameters.Add(DataLayer.CreateParameter("@" + mov.Schema.ValorContabil, DbType.Decimal, mov.ValorContabil));
        //    //cmd.Parameters.Add(DataLayer.CreateParameter("@" + mov.Schema.Lucro, DbType.Decimal, mov.Lucro));
        //}
        #endregion

        #region "Ext Id Map Data Modification Methods"

        public virtual void Validate(ExtIdMapState item)
        {
            string strMsg = string.Empty;

            if (String.IsNullOrEmpty(item.ExtId))
            { strMsg += "Identificador externo do ativo invalido." + Environment.NewLine; }

            if (String.IsNullOrEmpty(item.Ticker))
            { strMsg += "O Ticker deve ser preenchido." + Environment.NewLine; }

            if (item.Id < 0)
            { strMsg += "O identificador deve ser positivo." + Environment.NewLine; }

            if (item.TebBizAtivoId < 0)
            { strMsg += "O identificador (TebaldiBiz.Id) deve ser positivo." + Environment.NewLine; }

            if (strMsg != string.Empty)
            { throw new TebaldiMarketDataException(strMsg); }
        }

        public int Save(State.ExtIdMapState extIdMap)
        {
            List<ExtIdMapState> lst = new List<ExtIdMapState>();
            lst.Add(extIdMap);

            return Save(lst);
        }

        public int Save(List<ExtIdMapState> lst)
        {
            IDbCommand cmd;
            string strSQL;

            // Check Business Rules
            foreach (State.ExtIdMapState item in lst)
            {
                Validate(item);
            }

            strSQL = "procGravaExtIdMap";

            cmd = DataLayer.CreateCommand(strSQL, mstrConnectString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(DataLayer.CreateParameter("@extIdMapList", DbType.String, ParseToXml(lst), mstrConnectString));

            return DataLayer.ExecuteSQL(cmd);
        }

        protected string ParseToXml(List<State.ExtIdMapState> lst)
        {
            XmlDocument xmlDoc = new XmlDocument();
            int nodeContador = 1;

            XmlElement root = xmlDoc.CreateElement("ROOT");
            xmlDoc.AppendChild(root);

            foreach (State.ExtIdMapState item in lst)
            {
                XmlElement xmlObj = xmlDoc.CreateElement("ExtIdMap");
                xmlObj.SetAttribute("NodeId", nodeContador.ToString());
                xmlObj.SetAttribute("Id", item.Id.ToString());
                xmlObj.SetAttribute("EXT_ID", item.ExtId);

                xmlObj.SetAttribute("TebaldiBiz_AtivoId", item.TebBizAtivoId.ToString());

                xmlObj.SetAttribute("Ticker", item.Ticker);

                root.AppendChild(xmlObj);

                nodeContador++;
            }

            return xmlDoc.OuterXml;
        }

        #endregion
    }
}

