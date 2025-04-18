using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    public class Logger
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public Level LogLevel { get; set; }

        public string Method { get; set; }
        public string Message { get; set; }

        public Logger(string message, string problemPath, string logPath)
        {

            string[] mes = null;
            if (message.Contains("|"))
            {
                mes = message.Split('|');
                if (mes.Length == 5)
                {
                    try
                    {
                        string[] datetime = mes[0].Split(' ');
                        Date = string.Join("-", datetime[0], datetime[2], datetime[4]);
                        Time = datetime[5];
                        LogLevel = GetLevel(mes[2]);
                        Method = mes[3];
                        Message = mes[4];
                        SaveToLogs(logPath);
                    }
                    catch
                    {
                        SaveToProblems(message, problemPath);
                    }
                }
                else
                {
                    SaveToProblems(message, problemPath);
                }
            }
            else
            {
                mes = message.Split();
                if (mes.Length == 6)
                {
                    try
                    {
                        string[] data = mes[0].Split('.');
                        string day = data[0].Trim();
                        string month = data[1].Trim();
                        string year = data[2].Trim();

                        Date = string.Join("-", year, month, day);
                        Time = mes[1];
                        LogLevel = GetLevel(mes[2]);
                        Method = "DEFAULT";
                        Message = string.Join(" ", mes[3], mes[4], mes[5]);
                        SaveToLogs(logPath);
                    }
                    catch
                    {
                        SaveToProblems(problemPath, message);
                    }

                }
                else
                {
                    SaveToProblems(problemPath, message);
                }
            }


        }

        private void SaveToLogs(string logPath)
        {
            StringBuilder sB = new StringBuilder();
            sB.Append(Date);
            sB.Append("\t");
            sB.Append(Time);
            sB.Append("\t");
            sB.Append(LogLevel);
            sB.Append("\t");
            sB.Append(Method);
            sB.Append("\t");
            sB.Append(Message);
            sB.AppendLine();
            File.AppendAllText(logPath, sB.ToString());


        }

        private void SaveToProblems(string problemPath, string message)
        {
            if (!File.Exists(problemPath))
            {
                // Создаем файл и закрываем поток
                using (File.Create(problemPath)) { }

                // Записываем сообщение в новый файл
                File.WriteAllText(problemPath, message);
            }
            else
            {
                // Добавляем сообщение в существующий файл
                File.AppendAllText(problemPath, message);
            }
        }


        private Level GetLevel(string v)
        {
            if (v.Contains("INFO"))
            {
                LogLevel = Level.INFO;
            }
            if (v.Contains("WARN"))
            {
                LogLevel = Level.WARN;
            }
            if (v.Contains("DEBUG"))
            {
                LogLevel = Level.DEBUG;
            }
            if (v.Contains("ERROR"))
            {
                LogLevel = Level.ERROR;
            }
            return LogLevel;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string problemPath = Path.Combine(Directory.GetCurrentDirectory(), "problems.txt");
            string logPath = Path.Combine(Directory.GetCurrentDirectory(), "logPath.txt");
            string message1 = "10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'";
            string message2 = "2025 - 03 - 10 15:14:51.5882 | INFO | 11 | MobileComputer.GetDeviceId | Код устройства: '@MINDEO-M40-D-410244015546'";

            Logger logger1 = new Logger(message1, problemPath, logPath);
            Logger logger2 = new Logger(message2, problemPath, logPath);

            Process.Start("notepad.exe", logPath);


        }
    }
}
