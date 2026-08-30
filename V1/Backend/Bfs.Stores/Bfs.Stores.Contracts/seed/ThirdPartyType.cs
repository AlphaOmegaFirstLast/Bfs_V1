using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Stores.Contracts
{
    public class ThirdPartyType : IIdentifiable 
    {
        ///<Summary>
        /// ThirdPartyType IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// ThirdPartyType ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// ThirdPartyType Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// ThirdPartyType Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}

