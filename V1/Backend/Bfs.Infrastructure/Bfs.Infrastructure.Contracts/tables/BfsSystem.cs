using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Contracts
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
        /// BfsSystem Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
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
        /// BfsSystem BestFit Client.
        ///</Summary>
        public long BfsClientId {get; set;} = 0 ;
///<Summary>
        /// BfsSystem Template.
        ///</Summary>
        public int SystemTemplateId {get; set;} = 0 ;

    }
}