namespace SpaceCheckSimple;

public class DataService(AlertService AlertService)
{
    private List<MachineInfo> data = new();

    public void Add(string NodeName, int PercentFull, DateTime At)
    {
        // remove older than 24h
        data.RemoveAll(x => x.At <= DateTime.Now.AddDays(-1));

        // add incoming
        data.Add(new MachineInfo(NodeName, PercentFull, At));

        // sent alert if disk full
        if(PercentFull >= 90)
            AlertService.Alert(NodeName, PercentFull);
    }

    public IEnumerable<MachineInfo> GetLast()
    {
        return data
            .GroupBy(x => x.NodeName)
            .Select(g => g.OrderByDescending(x => x.At).First());
    }
}

public record MachineInfo(string NodeName, int PercentFull, DateTime At);
