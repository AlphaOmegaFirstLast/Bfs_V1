using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Contracts
{
    public class BfsClient : IIdentifiable
    {
        ///<Summary>
        /// BfsClient Database Connection.
        ///</Summary>
        public string DbConnection {get; set;} = string.Empty ;
///<Summary>
        /// BfsClient IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// BfsClient ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// BfsClient Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// BfsClient Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

        ///<Summary>
        /// BfsClient Custom Fields.
        ///</Summary>
        public List<CustomField> CustomFields {get; set;} = new List<CustomField>() ;

    }
}