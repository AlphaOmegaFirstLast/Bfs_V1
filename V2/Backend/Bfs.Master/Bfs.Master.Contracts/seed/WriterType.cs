using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class WriterType : IIdentifiable 
    {
        ///<Summary>
        /// WriterType IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// WriterType ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// WriterType Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// WriterType Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}