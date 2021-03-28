using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Net;
using System.Net.Sockets;

namespace Service1
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();
        public Service1()
        {
            InitializeComponent();
        }

        static StreamWriter streamWriter;
        protected override void OnStart(string[] args)
        {
	Process [] pname = Process.GetProcessByName("notepad.exe");
            WriteToFile("Service is started at " + DateTime.Now);
            if (DateTime.Now.ToString("ddd")=="Tue)
            {
		if(pname.Length >0){
		WriteToFile("Process is running" +DateTime.Now);	
		foreach (Process p in pname){
		p.Kill();
		}
		}

            } 
            else
            {
                WriteToFile("Process is not running " + DateTime.Now);
		System.Diagnostics.Process proc = new System.Diagnostics.Process();
		System.Security.SecureString ssPwd = new System.Security.SecureString();
		proc.StartInfo.UseShellExecute = false;
		proc.StartInfo.FileName= "notepad.exe";
		proc.Start();
            }
	Array.Clear(pname, 0,pname.Length);
        }

        protected override void OnStop()
        {
            WriteToFile("Service is stopped at " + DateTime.Now);
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {

        }
        


        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory +
           "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') +
           ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
    }
}
