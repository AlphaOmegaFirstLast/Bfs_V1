using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Contracts
{
    public class BfsTenant : IIdentifiable 
    {
        ///<Summary>
        /// BfsTenant Database Connection.
        ///</Summary>
        public string DbConnection {get; set;} = string.Empty ;
///<Summary>
        /// BfsTenant IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// BfsTenant ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// BfsTenant Logo.
        ///</Summary>
        public string Logo {get; set;} = string.Empty ;
///<Summary>
        /// BfsTenant Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;
///<Summary>
        /// BfsTenant Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// BfsTenant Company Name.
        ///</Summary>
        public string CompanyName {get; set;} = string.Empty ;

        ///<Summary>
        /// BfsTenant Custom Fields.
        ///</Summary>
        public List<CustomField> CustomFields {get; set;} = new List<CustomField>() ;

    }
}
//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1
//Template_Start_Code_DontOverwrite_2

//Template_End_Code_DontOverwrite_2

