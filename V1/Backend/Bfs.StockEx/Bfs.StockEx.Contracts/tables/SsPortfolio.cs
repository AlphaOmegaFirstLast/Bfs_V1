using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class SsPortfolio : IIdentifiable 
    {
        ///<Summary>
        /// SsPortfolio IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// SsPortfolio ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// SsPortfolio Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// SsPortfolio Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;
///<Summary>
        /// SsPortfolio Interest.
        ///</Summary>
        public decimal Interest {get; set;} = 0 ;

        ///<Summary>
        /// SsPortfolio Broker.
        ///</Summary>
        public long BrokerId {get; set;} = 0 ;
///<Summary>
        /// SsPortfolio Investor.
        ///</Summary>
        public long InvestorId {get; set;} = 0 ;

    }
}

