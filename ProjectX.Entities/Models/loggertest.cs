
using System;
using System.IO;
namespace ProjectX.Entities.Models
{
    public class loggertest
    {
        private readonly string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "log.txt");

        public loggertest()
        {
            this.logFilePath = logFilePath;
        }

        public void Log(string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now} - {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging message: {ex.Message}");
            }
        }
    }
}




