using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Stores.Contracts
{
    public class Operation : IIdentifiable 
    {
        ///<Summary>
        /// Operation IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// Operation ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// Operation Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// Operation Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

        ///<Summary>
        /// Operation Effect Type.
        ///</Summary>
        public int EffectTypeId {get; set;} = 0 ;
///<Summary>
        /// Operation Third Party Type.
        ///</Summary>
        public int ThirdPartyTypeId {get; set;} = 0 ;

    }
}

