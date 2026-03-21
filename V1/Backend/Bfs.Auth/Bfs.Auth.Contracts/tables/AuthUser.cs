using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public class AuthUser : IIdentifiable 
    {
        ///<Summary>
        /// AuthUser IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// AuthUser ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// AuthUser AspNetUserId.
        ///</Summary>
        public string AspNetUserId {get; set;} = string.Empty ;
///<Summary>
        /// AuthUser Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;
///<Summary>
        /// AuthUser Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;

    }
}
//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1

