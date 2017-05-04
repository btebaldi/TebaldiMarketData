using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public List<HistoricoCotacaoState> GetHistoricoCotacao()
        {
            throw new NotImplementedException();

            List<HistoricoCotacaoState> lstCotacao = new List<HistoricoCotacaoState>();

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

        //private void FillFromDataReader(IDataReader dr, EmpresaState empresa)
        //{
        //    throw new NotImplementedException();

        //    empresa.IdEmpresa = Convert.ToInt32(dr[empresa.Schema.IdEmpresa]);
        //    empresa.CodEmpresa = dr[empresa.Schema.CodEmpresa].ToString();
        //    empresa.DenominacaoSocial = dr[empresa.Schema.DenominacaoSocial].ToString();
        //    empresa.NomeResumido = dr[empresa.Schema.NomeResumido].ToString();
        //    empresa.DataRegistro = Convert.ToDateTime(dr[empresa.Schema.DataRegistro]);
        //}

        #endregion

        #region "Data Modification Methods"
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
    }
}

