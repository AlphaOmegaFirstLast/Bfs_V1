using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Auth.Contracts
{
    public interface IAuthUser : IIdentifiable 
    {
        string AspNetUserId {get; set;}
        string Name { get; set; }
        string Notes { get; set; }
    }
}

