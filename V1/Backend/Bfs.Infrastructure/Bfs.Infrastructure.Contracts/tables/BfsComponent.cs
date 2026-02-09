using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Contracts
{
    public class BfsComponent : IIdentifiable
    {
        ///<Summary>
        /// BfsComponent IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// BfsComponent ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// BfsComponent Is Soft Delete.
        ///</Summary>
        public bool IsSoftDelete {get; set;} = false ;
///<Summary>
        /// BfsComponent Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// BfsComponent DisplayName.
        ///</Summary>
        public string DisplayName {get; set;} = string.Empty ;
///<Summary>
        /// BfsComponent MenuName.
        ///</Summary>
        public string MenuName {get; set;} = string.Empty ;
///<Summary>
        /// BfsComponent MenuPlaceHolder.
        ///</Summary>
        public string MenuPlaceHolder {get; set;} = string.Empty ;
///<Summary>
        /// BfsComponent QueryBaseTable.
        ///</Summary>
        public string QueryBaseTable {get; set;} = string.Empty ;
///<Summary>
        /// BfsComponent Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

        ///<Summary>
        /// BfsComponent BestFit System.
        ///</Summary>
        public long BfsSystemId {get; set;} = 0 ;
///<Summary>
        /// BfsComponent Data Type.
        ///</Summary>
        public int DataTypeId {get; set;} = 0 ;

    }
}