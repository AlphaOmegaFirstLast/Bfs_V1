using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class CalculationMethod : IIdentifiable 
    {
        ///<Summary>
        /// CalculationMethod IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// CalculationMethod ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// CalculationMethod Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// CalculationMethod Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}

