
using System.Net.Mail;
using System.Reflection.PortableExecutable;

namespace SpaceCheckSimple;

public class EmailService
{
    public static string? EmailFrom = Environment.GetEnvironmentVariable("EmailFrom");

    public static string? EmailTo = Environment.GetEnvironmentVariable("EmailTo");

    public static string? SmtpUserName = Environment.GetEnvironmentVariable("SmtpUserName");
    public static string? SmtpPwd = Environment.GetEnvironmentVariable("SmtpPwd");
    public static string? SmtpServer = Environment.GetEnvironmentVariable("SmtpServer");

    public void SentAlert(string machine, int percent)
    {
        Console.WriteLine($"Sending email to {EmailTo} with {machine} {percent}%");

        if(string.IsNullOrEmpty(EmailFrom) || string.IsNullOrEmpty(EmailTo))
        {
            Console.WriteLine("!!! ERROR, EmailFrom or EmailTo is empty !!!!!!!");
            return;
        }

        sendEmail(EmailFrom, EmailTo, "Disk Space Checker Alert", $"{machine} is low on disk space: {percent}% used");
    }

    public void TestEmail()
    {
        Console.WriteLine($"EmailFrom: {EmailFrom}");
        Console.WriteLine($"EmailTo: {EmailTo}");
        Console.WriteLine($"SmtpUserName: {SmtpUserName}");
        Console.WriteLine($"SmtpPwd: {SmtpPwd}");
        Console.WriteLine($"SmtpServer: {SmtpServer}");

        if (string.IsNullOrEmpty(EmailFrom) || string.IsNullOrEmpty(EmailTo))
        {
            Console.WriteLine("!!! ERROR, EmailFrom or EmailTo is empty !!!!!!!");
            return;
        }

        sendEmail(EmailFrom, EmailTo, "Disk Space Checker Test", $"Test Email");
    }

    public static void sendEmail(string sentfrom, string sendTo, string subject, string htmlBody)
    {
        // Credentials:
        var credentialUserName = SmtpUserName;
        var pwd = SmtpPwd;

        // Configure the client:
        System.Net.Mail.SmtpClient client =
            new System.Net.Mail.SmtpClient(SmtpServer);

        client.Port = 587;
        client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        client.UseDefaultCredentials = false;

        System.Net.NetworkCredential credentials =
        new System.Net.NetworkCredential(credentialUserName, pwd);

        client.EnableSsl = true;
        client.Credentials = credentials;

        MailMessage mailMessage = new MailMessage(sentfrom, sendTo);
        mailMessage.Body = htmlBody;
        mailMessage.IsBodyHtml = true;
        mailMessage.Subject = subject;
        
        client.Send(mailMessage);
    }
}