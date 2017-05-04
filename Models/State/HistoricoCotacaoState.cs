using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tebaldi.MarketData.Models.State
{
    public class HistoricoCotacaoState
    {
        public string EXT_ID { get; set; }
        public int AtivoId { get; set; }
        public string FeedId { get; set; }
        public DateTime DataPregao { get; set; }
        public string Ticker { get; set; }
        public int? TipoMercado { get; set; }
        public string NomeResumido { get; set; }
        public string EspecificacaoPapel { get; set; }
        public decimal PrecoAbertura { get; set; }
        public decimal PrecoMaximo { get; set; }
        public decimal PrecoMinimo { get; set; }
        public decimal PrecoMedio { get; set; }
        public decimal PrecoFechamento { get; set; }
        public decimal? TotalNegocios { get; set; }
        public Int64? Quantidade { get; set; }
        public decimal? Volume { get; set; }
        public string ISIN { get; set; }
        public decimal Var { get; set; }

        public SchemaStruct Schema;

        #region "Constructors"
        public HistoricoCotacaoState()
        {
            EXT_ID = "";
            FeedId = "";
            Ticker = "";
            NomeResumido = "";
            EspecificacaoPapel = "";
            ISIN = "";

            Schema = new SchemaStruct();
            Schema.ObjectName = "TB_IMP_HISTORICO_COTACAO";
            Schema.EXT_ID = "EXT_ID";
            Schema.AtivoId = "AtivoId";
            Schema.FeedId = "FeedId";
            Schema.DataPregao = "DataPregao";
            Schema.Ticker = "Ticker";
            Schema.TipoMercado = "TipoMercado";
            Schema.NomeResumido = "NomeResumido";
            Schema.EspecificacaoPapel = "EspecPapel";
            Schema.PrecoAbertura = "PrecoAbertura";
            Schema.PrecoMaximo = "PrecoMaximo";
            Schema.PrecoMinimo = "PrecoMinimo";
            Schema.PrecoMedio = "PrecoMedio";
            Schema.PrecoFechamento = "PrecoFechamento";
            Schema.TotalNegocios = "TotalNegocios";
            Schema.Quantidade = "Quantidade";
            Schema.Volume = "Volume";
            Schema.ISIN = "ISIN";
            Schema.Variacao = "Variacao";
        }
        #endregion

        #region "Schema Structure to return Object and Column Names"
        [Serializable]
        public struct SchemaStruct
        {
            // Nome da tabela
            public string ObjectName;

            // Nome das colunas
            public string EXT_ID;
            public string AtivoId;
            public string FeedId;
            public string DataPregao;
            public string Ticker;
            public string TipoMercado;
            public string NomeResumido;
            public string EspecificacaoPapel;
            public string PrecoAbertura;
            public string PrecoMaximo;
            public string PrecoMinimo;
            public string PrecoMedio;
            public string PrecoFechamento;
            public string TotalNegocios;
            public string Quantidade;
            public string Volume;
            public string ISIN;
            public string Variacao;
        }
        #endregion

    }
}
