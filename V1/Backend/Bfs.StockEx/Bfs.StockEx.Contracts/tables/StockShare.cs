using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class StockShare : IIdentifiable 
    {
        ///<Summary>
        /// StockShare IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// StockShare ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// StockShare Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// StockShare Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

        ///<Summary>
        /// StockShare Trading Room.
        ///</Summary>
        public long TradingRoomId {get; set;} = 0 ;
///<Summary>
        /// StockShare Currency.
        ///</Summary>
        public long CurrencyId {get; set;} = 0 ;

    }
}