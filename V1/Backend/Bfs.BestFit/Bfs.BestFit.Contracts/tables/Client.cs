using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.BestFit.Contracts
{
    public class Client : IIdentifiable
    {
        ///<Summary>
        /// Client IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// Client ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// Client Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// Client Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;
///<Summary>
        /// Client Database Connection.
        ///</Summary>
        public string DbConnection {get; set;} = string.Empty ;

        ///<Summary>
        /// Client Custom Fields.
        ///</Summary>
        public List<CustomField> CustomFields {get; set;} = new List<CustomField>() ;

    }
}