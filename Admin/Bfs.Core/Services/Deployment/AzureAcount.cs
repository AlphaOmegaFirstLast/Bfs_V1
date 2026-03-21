using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bfs.Core.Services.Deployment;

public class AzureAcount
{
    //data from azure account using servicePrinciple for login

    public string azureAccountName = "accName";
    public string azureAccountPassword = "dummyPassword";
    public string tenantId = "tenantId";
    public string subscriptionId = "subscriptionId";
}