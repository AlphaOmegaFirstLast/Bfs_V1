using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Contracts
{
    public class CustomReports : IIdentifiable
    {
        ///<Summary>
        /// CustomReports ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// CustomReports Name.
        ///</Summary>
        public string Name {get; set;} = string.Empty ;
///<Summary>
        /// CustomReports Request.
        ///</Summary>
        public string Request {get; set;} = string.Empty ;
///<Summary>
        /// CustomReports Base Report.
        ///</Summary>
        public string BaseReport {get; set;} = string.Empty ;
///<Summary>
        /// CustomReports Private.
        ///</Summary>
        public bool IsPrivate {get; set;} = false ;
///<Summary>
        /// CustomReports IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// CustomReports Created By.
        ///</Summary>
        public string CreatedBy {get; set;} = string.Empty ;
///<Summary>
        /// CustomReports Base Report Url.
        ///</Summary>
        public string Url {get; set;} = string.Empty ;

    }
}