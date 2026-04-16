using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class BfsSystem : IIdentifiable 
    {
        ///<Summary>
        /// BfsSystem IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// BfsSystem ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// BfsSystem Is BestFit Master System.
        ///</Summary>
        public bool IsMaster {get; set;} = false ;
///<Summary>
        /// BfsSystem Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;
///<Summary>
        /// BfsSystem Base Port Number.
        ///</Summary>
        public string BasePortNumber {get; set;} = string.Empty ;
///<Summary>
        /// BfsSystem DB Prefix.
        ///</Summary>
        public string DbPrefix {get; set;} = string.Empty ;
///<Summary>
        /// BfsSystem Logo.
        ///</Summary>
        public string Logo {get; set;} = string.Empty ;
///<Summary>
        /// BfsSystem Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;

        ///<Summary>
        /// BfsSystem Template.
        ///</Summary>
        public int SystemTemplateId {get; set;} = 0 ;

    }
}