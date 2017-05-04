using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tebaldi.MarketData
{
    [Serializable]
    public class TebaldiMarketDataException : ApplicationException
    {
        public TebaldiMarketDataException()
            : base()
        { }

        public TebaldiMarketDataException(string msg)
            : base(msg)
        { }

        public string MessageForWebDisplay
        {
            get { return base.Message.Replace(Environment.NewLine, "<br>"); }
        }
    }
}