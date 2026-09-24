using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class BfsManualCode : IIdentifiable 
    {
        ///<Summary>
        /// BfsManualCode IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// BfsManualCode ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// BfsManualCode Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// BfsManualCode FileName.
        ///</Summary>
        public string FileName {get; set;} = string.Empty ;
///<Summary>
        /// BfsManualCode Start Template.
        ///</Summary>
        public string StartTemplate {get; set;} = string.Empty ;
///<Summary>
        /// BfsManualCode End Template.
        ///</Summary>
        public string EndTemplate {get; set;} = string.Empty ;
///<Summary>
        /// BfsManualCode Code.
        ///</Summary>
        public string Code {get; set;} = string.Empty ;
///<Summary>
        /// BfsManualCode Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

        ///<Summary>
        /// BfsManualCode BestFit System.
        ///</Summary>
        public long BfsSystemId {get; set;} = 0 ;
///<Summary>
        /// BfsManualCode Component.
        ///</Summary>
        public long BfsComponentId {get; set;} = 0 ;

    }
}