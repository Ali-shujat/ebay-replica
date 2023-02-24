namespace eCommerce.Services;
public static class LoggingService
{
    private static readonly string logFilePath = "log.txt";

    public static void Log(string message)
    {
        string logMessage = $"{DateTime.Now.ToString()} - {message}";

        using (StreamWriter sw = File.AppendText(logFilePath))
        {
            sw.WriteLine(logMessage);
        }
    }
}