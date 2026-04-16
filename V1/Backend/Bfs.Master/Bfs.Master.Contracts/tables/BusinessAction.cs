using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class BusinessAction : IIdentifiable 
    {
        ///<Summary>
        /// BusinessAction IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// BusinessAction ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// BusinessAction Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// BusinessAction Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;
///<Summary>
        /// BusinessAction Short Name.
        ///</Summary>
        public string ShortName {get; set;} = string.Empty ;

        ///<Summary>
        /// BusinessAction Action Type.
        ///</Summary>
        public int ActionTypeId {get; set;} = 0 ;

    }
}