using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Contracts
{
    public class SystemActionListFilter
    {

        public string? ShortName { get; set; }
public string? MatchProperty { get; set; }
public string? MatchValues { get; set; }
public string? Name { get; set; }

        public int? ActionTypeId { get; set; }
public int? WriterTypeId { get; set; }

    }
}
//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1

