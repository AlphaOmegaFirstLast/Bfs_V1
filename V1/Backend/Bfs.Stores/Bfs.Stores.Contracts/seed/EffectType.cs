using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Stores.Contracts
{
    public class EffectType : IIdentifiable 
    {
        ///<Summary>
        /// EffectType IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// EffectType ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// EffectType Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// EffectType Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}

