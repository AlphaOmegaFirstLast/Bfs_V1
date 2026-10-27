using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class ProcessStatus : IIdentifiable 
    {
        ///<Summary>
        /// ProcessStatus IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// ProcessStatus ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// ProcessStatus Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// ProcessStatus Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}