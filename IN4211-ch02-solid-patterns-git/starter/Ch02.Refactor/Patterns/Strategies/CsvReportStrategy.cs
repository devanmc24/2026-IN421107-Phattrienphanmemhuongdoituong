using System.Text;

namespace EduHub.Ch02.Patterns.Strategies;

public class CsvReportStrategy : IReportStrategy
{
    public string Name => "CSV";

    public string Render(IReadOnlyList<ClubStat> stats)
    {
        var builder = new StringBuilder("Club,Members,Events,Registered,CheckedIn\n");
        foreach (var stat in stats)
            builder.Append($"\"{stat.ClubName}\",{stat.Members},{stat.Events},{stat.Registered},{stat.CheckedIn}\n");
        return builder.ToString();
    }
}
