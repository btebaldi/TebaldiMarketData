﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tebaldi.MarketData.Models;

namespace Tebaldi.MarketData
{
    public class Log : LogDC
    {

        #region "Constructors"
        public Log(string ConnectString)
        { base.ConnectString = ConnectString; }
        #endregion

        #region "Validate Method"
        //public override void Validate(Models.State.AtivoCotacaoState item)
        //{
        //    string strMsg = string.Empty;

        //    try
        //    {
        //        // Check data class business rules
        //        base.Validate(item);
        //    }
        //    //catch (BusinessRuleException ex)
        //    catch (Exception ex)
        //    {
        //        // Get Business Rule Messages
        //        strMsg = ex.Message;
        //    }

        //    //*******************************************************
        //    //* CHECK YOUR BUSINESS RULES HERE

        //    if (strMsg != String.Empty)
        //    { throw new TebaldiMarketDataException(strMsg); }
        //}
        #endregion

        #region "Custom Methods"

        #endregion
    }
}
