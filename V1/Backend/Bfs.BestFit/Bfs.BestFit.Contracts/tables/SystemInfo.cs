using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.BestFit.Contracts
{
    public class SystemInfo : IIdentifiable
    {
        ///<Summary>
        /// SystemInfo IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// SystemInfo ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// SystemInfo Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// SystemInfo Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;
///<Summary>
        /// SystemInfo Base Port Number.
        ///</Summary>
        public string BasePortNumber {get; set;} = string.Empty ;

        ///<Summary>
        /// SystemInfo Client.
        ///</Summary>
        public long ClientId {get; set;} = 0 ;
///<Summary>
        /// SystemInfo Template.
        ///</Summary>
        public int SystemTemplateId {get; set;} = 0 ;

    }
}