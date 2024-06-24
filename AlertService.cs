using System.Collections.Concurrent;

namespace SpaceCheckSimple;

public class AlertService(EmailService EmailService)
{
    /// <summary>
    /// last alerts sent - machine name and time
    /// </summary>
    private ConcurrentDictionary<string, DateTime> lastAlerts = new();

    public void Alert(string machine, int percent)
    {
        /// already sent email for this machine, wait at least 24h
        if (lastAlerts.TryGetValue(machine, out DateTime lastAlert) && lastAlert >= DateTime.Now.AddDays(-1))
        {
            Console.WriteLine($"Skipping alert for {machine} {percent}%, because already sent at {lastAlert.ToString("yyyy-MM-dd HH:mm")} (wait 24h)");
            return;
        }

        /// remmber we sending
        lastAlerts.AddOrUpdate(machine, DateTime.Now, (_, _) => DateTime.Now);

        /// sent email
        EmailService.SentAlert(machine, percent);
    }
}
