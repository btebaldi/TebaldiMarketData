using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;



namespace Tebaldi.MarketData
{
    public class HistoricoCotacaoHandler : MarketData.Models.HistoricoCotacaoDC
    {
        #region "Constructors"
        public HistoricoCotacaoHandler(string ConnectString)
        { base.ConnectString = ConnectString; }
        #endregion

        #region "Validate Method"
        public override void Validate(Tebaldi.MarketData.Models.State.HistoricoCotacaoState item)
        {
            string strMsg = string.Empty;

            try
            {
                // Check data class business rules
                base.Validate(item);
            }
            //catch (BusinessRuleException ex)
            catch (Exception ex)
            {
                // Get Business Rule Messages
                strMsg = ex.Message;
            }

            //*******************************************************
            //* CHECK YOUR BUSINESS RULES HERE

            if (strMsg != String.Empty)
            { throw new TebaldiMarketDataException(strMsg); }
        }
        #endregion

        #region "Custom Methods"
        public static DataTable GetDataTable()
        {
            Models.State.HistoricoCotacaoState obj = new Models.State.HistoricoCotacaoState();
            DataTable table = new DataTable(obj.Schema.ObjectName);

            table.Columns.Add(obj.Schema.EXT_ID, typeof(String));
            table.Columns.Add(obj.Schema.AtivoId, typeof(Int32));
            table.Columns.Add(obj.Schema.ProcessId, typeof(String));
            table.Columns.Add(obj.Schema.DataPregao, typeof(DateTime));
            table.Columns.Add(obj.Schema.Ticker, typeof(String));
            table.Columns.Add(obj.Schema.TipoMercado, typeof(Int32));
            table.Columns.Add(obj.Schema.NomeResumido, typeof(String));
            table.Columns.Add(obj.Schema.EspecificacaoPapel, typeof(String));
            table.Columns.Add(obj.Schema.PrecoAbertura, typeof(Decimal));
            table.Columns.Add(obj.Schema.PrecoMaximo, typeof(Decimal));
            table.Columns.Add(obj.Schema.PrecoMinimo, typeof(Decimal));
            table.Columns.Add(obj.Schema.PrecoMedio, typeof(Decimal));
            table.Columns.Add(obj.Schema.PrecoFechamento, typeof(Decimal));
            table.Columns.Add(obj.Schema.TotalNegocios, typeof(Int32));
            table.Columns.Add(obj.Schema.Quantidade, typeof(Int64));
            table.Columns.Add(obj.Schema.Volume, typeof(Decimal));
            table.Columns.Add(obj.Schema.ISIN, typeof(String));
            table.Columns.Add(obj.Schema.Variacao, typeof(System.Decimal));
            return table;
        }


        public static List<String> GetColumnNames()
        {
            List<string> lst = new List<string>();
            Models.State.HistoricoCotacaoState obj = new Models.State.HistoricoCotacaoState();

            lst.Add(obj.Schema.EXT_ID);
            lst.Add(obj.Schema.AtivoId);
            lst.Add(obj.Schema.ProcessId);
            lst.Add(obj.Schema.DataPregao);
            lst.Add(obj.Schema.Ticker);
            lst.Add(obj.Schema.TipoMercado);
            lst.Add(obj.Schema.NomeResumido);
            lst.Add(obj.Schema.EspecificacaoPapel);
            lst.Add(obj.Schema.PrecoAbertura);
            lst.Add(obj.Schema.PrecoMaximo);
            lst.Add(obj.Schema.PrecoMinimo);
            lst.Add(obj.Schema.PrecoMedio);
            lst.Add(obj.Schema.PrecoFechamento);
            lst.Add(obj.Schema.TotalNegocios);
            lst.Add(obj.Schema.Quantidade);
            lst.Add(obj.Schema.Volume);
            lst.Add(obj.Schema.ISIN);
            lst.Add(obj.Schema.Variacao);
            return lst;
        }
        #endregion


    }
}
