using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class PaymentMode : IIdentifiable 
    {
        ///<Summary>
        /// PaymentMode IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// PaymentMode ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// PaymentMode Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// PaymentMode Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}