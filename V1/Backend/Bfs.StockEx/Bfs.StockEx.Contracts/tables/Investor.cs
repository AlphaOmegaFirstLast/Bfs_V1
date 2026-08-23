using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class Investor : IIdentifiable 
    {
        ///<Summary>
        /// Investor IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// Investor ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// Investor Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// Investor Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;
///<Summary>
        /// Investor Code.
        ///</Summary>
        public string Code {get; set;} = string.Empty ;
///<Summary>
        /// Investor Email.
        ///</Summary>
        public string Email {get; set;} = string.Empty ;

    }
}