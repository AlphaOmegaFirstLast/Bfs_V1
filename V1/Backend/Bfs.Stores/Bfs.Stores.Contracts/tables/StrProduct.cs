using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Stores.Contracts
{
    public class StrProduct : IIdentifiable 
    {
        ///<Summary>
        /// StrProduct IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// StrProduct ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// StrProduct Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// StrProduct Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}