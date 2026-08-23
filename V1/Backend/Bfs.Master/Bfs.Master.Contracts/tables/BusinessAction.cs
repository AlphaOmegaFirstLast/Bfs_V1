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
        /// BusinessAction Short Name.
        ///</Summary>
        public string ShortName {get; set;} = string.Empty ;
///<Summary>
        /// BusinessAction Writer Matching Property.
        ///</Summary>
        public string MatchProperty {get; set;} = string.Empty ;
///<Summary>
        /// BusinessAction Writer Matching Values.
        ///</Summary>
        public string MatchValues {get; set;} = string.Empty ;
///<Summary>
        /// BusinessAction Action Template.
        ///</Summary>
        public string ActionTemplate {get; set;} = string.Empty ;
///<Summary>
        /// BusinessAction Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// BusinessAction Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

        ///<Summary>
        /// BusinessAction Action Type.
        ///</Summary>
        public int ActionTypeId {get; set;} = 0 ;
///<Summary>
        /// BusinessAction Writer Type.
        ///</Summary>
        public int WriterTypeId {get; set;} = 0 ;

    }
}

