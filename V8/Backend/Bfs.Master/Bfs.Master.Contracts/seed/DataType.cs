using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class DataType : IIdentifiable 
    {
        ///<Summary>
        /// DataType IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// DataType ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// DataType Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// DataType Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}