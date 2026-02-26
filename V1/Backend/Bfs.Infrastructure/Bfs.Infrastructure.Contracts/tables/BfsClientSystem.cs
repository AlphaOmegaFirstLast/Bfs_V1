using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Contracts
{
    public class BfsClientSystem : IIdentifiable
    {
        ///<Summary>
        /// BfsClientSystem IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// BfsClientSystem ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;

        ///<Summary>
        /// BfsClientSystem Client Name.
        ///</Summary>
        public long BfsClientId {get; set;} = 0 ;
///<Summary>
        /// BfsClientSystem BestFit System.
        ///</Summary>
        public long BfsSystemId {get; set;} = 0 ;

    }
}
//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1

