using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class SystemTemplate : IIdentifiable 
    {
        ///<Summary>
        /// SystemTemplate IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// SystemTemplate ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// SystemTemplate Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// SystemTemplate Notes.
        ///</Summary>
        public string Notes {get; set;} = string.Empty ;
///<Summary>
        /// SystemTemplate Project Type.
        ///</Summary>
        public string ProjectType {get; set;} = string.Empty ;
///<Summary>
        /// SystemTemplate Output Directory.
        ///</Summary>
        public string OutputDirectory {get; set;} = string.Empty ;
///<Summary>
        /// SystemTemplate Solution Directory.
        ///</Summary>
        public string SolutionDirectory {get; set;} = string.Empty ;
///<Summary>
        /// SystemTemplate Template.
        ///</Summary>
        public string Template {get; set;} = string.Empty ;

    }
}