namespace Bfs.Core.Data;

public class IdGenerator
{
    public static long GetId()
    {
        return DateTime.UtcNow.Ticks - new DateTime(day: 01, month: 01, year: 2024).Ticks;
    }
}