using Bfs.Core.Contracts;

namespace Bfs.BestFit.Contracts
{
    public class DataType1Item
    {
        public int DataTypeId { get; set; }
public string DataTypeName { get; set; }
public string DataTypeNotes { get; set; }

        //manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
    }
}