using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class BackendDataType : IIdentifiable 
    {
        ///<Summary>
        /// BackendDataType IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// BackendDataType ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// BackendDataType Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// BackendDataType Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}