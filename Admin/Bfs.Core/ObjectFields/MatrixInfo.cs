namespace Bfs.Core.ObjectFields;

public class MatrixInfo 
{  // i.e ComponentSystemAction
    public string? ParentApi{ get; set; } = string.Empty;           // Component => ComponentId
    public string? HorizontalApi { get; set; } = string.Empty;      // ActionLocation => Action Location
    public string? VerticalApi { get; set; } = string.Empty;        // SystemAction  => System Action
}