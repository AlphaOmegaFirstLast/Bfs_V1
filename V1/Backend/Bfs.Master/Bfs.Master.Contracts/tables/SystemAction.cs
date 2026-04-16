using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class SystemAction : IIdentifiable 
    {
        ///<Summary>
        /// SystemAction IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// SystemAction ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// SystemAction Short Name.
        ///</Summary>
        public string ShortName {get; set;} = string.Empty ;
///<Summary>
        /// SystemAction Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;
///<Summary>
        /// SystemAction Writer Matching Property.
        ///</Summary>
        public string MatchProperty {get; set;} = string.Empty ;
///<Summary>
        /// SystemAction Writer Matching Values.
        ///</Summary>
        public string MatchValues {get; set;} = string.Empty ;
///<Summary>
        /// SystemAction Action Template.
        ///</Summary>
        public string ActionTemplate {get; set;} = string.Empty ;
///<Summary>
        /// SystemAction Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;

        ///<Summary>
        /// SystemAction Action Type.
        ///</Summary>
        public int ActionTypeId {get; set;} = 0 ;
///<Summary>
        /// SystemAction Writer Type.
        ///</Summary>
        public int WriterTypeId {get; set;} = 0 ;

    }
}