using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Stores.Contracts
{
    public class StrStore : IIdentifiable 
    {
        ///<Summary>
        /// StrStore IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// StrStore ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// StrStore Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// StrStore Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;

    }
}