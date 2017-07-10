using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tebaldi.MarketData.Models.State
{
    class FeedTypeState
    {
        public FeedTypeStateEnum Type { get; set; }
        public int ID { get { return (int)Type; } }
        public string EnumCode { get { return ID.ToString(); } }
        public string Descricao { get; set; }
        public List<FeedTypeDefaultKeyState> DefaultKeys = new List<FeedTypeDefaultKeyState>();

        readonly SchemaStruct _schema;
        public SchemaStruct Schema { get { return _schema; } }

        #region "Constructors"
        /// <summary>
        /// Constructor
        /// </summary>
        public FeedTypeState()
        {
            _schema = new SchemaStruct();
            _schema.ObjectName = "TB_FeedType";
            _schema.Id = "FeedTypeId";
            _schema.EnumCode = "EnumCode";
            _schema.Descricao = "Descricao";
        }
        #endregion

        #region "Schema Structure to return Object and Column Names"
        [Serializable]
        public struct SchemaStruct
        {
            public string ObjectName;
            public string Id;
            public string EnumCode;
            public string Descricao;
        }
        #endregion
    }

    public enum FeedTypeStateEnum
    {
        BDI = 1,
        //Csv = 2,
        //CsvNoHeader
    }

}
