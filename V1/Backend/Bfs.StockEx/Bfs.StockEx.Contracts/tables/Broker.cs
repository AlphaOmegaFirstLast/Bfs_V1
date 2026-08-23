using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class Broker : IIdentifiable 
    {
        ///<Summary>
        /// Broker IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// Broker ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// Broker Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// Broker Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;
///<Summary>
        /// Broker Code.
        ///</Summary>
        public string Code {get; set;} = string.Empty ;
///<Summary>
        /// Broker Email.
        ///</Summary>
        public string Email {get; set;} = string.Empty ;

        ///<Summary>
        /// Broker Trading Room.
        ///</Summary>
        public long TradingRoomId {get; set;} = 0 ;

    }
}