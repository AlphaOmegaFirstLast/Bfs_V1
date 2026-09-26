using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class ActionLocation : IIdentifiable 
    {
        ///<Summary>
        /// ActionLocation IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// ActionLocation ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// ActionLocation Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// ActionLocation Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}