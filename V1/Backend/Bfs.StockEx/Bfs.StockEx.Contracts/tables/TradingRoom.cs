using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class TradingRoom : IIdentifiable 
    {
        ///<Summary>
        /// TradingRoom IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// TradingRoom ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// TradingRoom Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// TradingRoom Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}

