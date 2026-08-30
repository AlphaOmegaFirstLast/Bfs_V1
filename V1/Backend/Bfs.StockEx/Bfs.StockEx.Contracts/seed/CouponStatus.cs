using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class CouponStatus : IIdentifiable 
    {
        ///<Summary>
        /// CouponStatus IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// CouponStatus ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// CouponStatus Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// CouponStatus Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}

