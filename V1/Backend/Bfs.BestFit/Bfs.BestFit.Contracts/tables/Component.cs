using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.BestFit.Contracts
{
    public class Component : IIdentifiable
    {
        ///<Summary>
        /// Component IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// Component ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// Component Is Soft Delete.
        ///</Summary>
        public bool IsSoftDelete {get; set;} = false ;
///<Summary>
        /// Component Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// Component DisplayName.
        ///</Summary>
        public string DisplayName {get; set;} = string.Empty ;
///<Summary>
        /// Component MenuName.
        ///</Summary>
        public string MenuName {get; set;} = string.Empty ;
///<Summary>
        /// Component MenuPlaceHolder.
        ///</Summary>
        public string MenuPlaceHolder {get; set;} = string.Empty ;
///<Summary>
        /// Component Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;
///<Summary>
        /// Component QueryBaseTable.
        ///</Summary>
        public string QueryBaseTable {get; set;} = string.Empty ;

        ///<Summary>
        /// Component System Info.
        ///</Summary>
        public long SystemInfoId {get; set;} = 0 ;
///<Summary>
        /// Component Data Type.
        ///</Summary>
        public int DataTypeId {get; set;} = 0 ;

    }
}