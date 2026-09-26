using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class FormControlType : IIdentifiable 
    {
        ///<Summary>
        /// FormControlType IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// FormControlType ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// FormControlType Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// FormControlType Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}