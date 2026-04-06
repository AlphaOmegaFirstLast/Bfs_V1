using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class App : IIdentifiable 
    {
        ///<Summary>
        /// App IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// App ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// App Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// App Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;
///<Summary>
        /// App Logo.
        ///</Summary>
        public string Logo {get; set;} = string.Empty ;

        ///<Summary>
        /// App BestFit System.
        ///</Summary>
        public long BfsSystemId {get; set;} = 0 ;

    }
}

