using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class ChartElement : IIdentifiable 
    {
        ///<Summary>
        /// ChartElement IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// ChartElement ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// ChartElement Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// ChartElement Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}