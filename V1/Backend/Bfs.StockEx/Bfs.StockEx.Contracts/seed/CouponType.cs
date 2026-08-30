using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class CouponType : IIdentifiable 
    {
        ///<Summary>
        /// CouponType IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// CouponType ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// CouponType Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// CouponType Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}

