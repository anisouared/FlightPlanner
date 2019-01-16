using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;


namespace Core
{
    public class LogError : ILogError
    {
        private IConfiguration _configuration;
        
        public LogError(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Log(Exception ex)
        {
            string message = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            string indentation = "";
            message += Environment.NewLine;
            message += ex.ToString() + Environment.NewLine;
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                indentation += "    ";
                message += indentation + ex.ToString() + Environment.NewLine;
            }

            using (StreamWriter writer = File.AppendText(_configuration["LogFile"]))
            {
                writer.WriteLine(message);
            }
        }

        public void Log(string msgLog)
        {
            string message = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            message += Environment.NewLine;
            message += msgLog + Environment.NewLine;

            using (StreamWriter writer = File.AppendText(_configuration["LogFile"]))
            {
                writer.WriteLine(message);
            }
        }       
    }
}