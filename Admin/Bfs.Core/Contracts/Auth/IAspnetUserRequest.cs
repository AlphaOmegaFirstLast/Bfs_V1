using Bfs.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bfs.Core.Contracts.Auth
{
    public interface IAspnetUserRequest 
    {
        string AspNetUserId { get; set; } 

        string Notes { get; set; } 

        string Name { get; set; } 

        long UserRequestStatusId { get; set; } 
    }
}
