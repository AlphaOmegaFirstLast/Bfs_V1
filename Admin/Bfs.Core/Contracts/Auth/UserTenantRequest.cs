using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bfs.Core.Contracts.Auth
{
    public record UserTenantRequest(string UserId, string TenantId);

}
