using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public enum CommandType
    {
        CMD,
        POWERSHELL
    }
    public class CommandRes
    {
        public string Out { get; }
        public string Err { get; }
        public int ExitCode { get; }
        public CommandRes(string Out, string Err, int ExitCode)
        {
            this.Out = Out;
            this.Err = Err;
            this.ExitCode = ExitCode;
        }
    }

    public class Command : Process
    {
        public Command(CommandType type)
        {
            StartInfo = new ProcessStartInfo()
            {
                FileName = GetCommandType(type),
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow= true,
                StandardOutputEncoding = Encoding.GetEncoding("GBK"),
                StandardErrorEncoding = Encoding.GetEncoding("GBK"),
           
            };
        }
        string GetCommandType(CommandType type)
        {
            if (type == CommandType.CMD)
            {
                return "cmd.exe";
            }
            else if (type == CommandType.POWERSHELL) {
                return "powershell.exe";
            }
            else
            {
                return null;
            }
        }
        public CommandRes Send(string command)
        {
            try
            {
                StartInfo.Arguments = $"/c {command}";
                Start();
                string Out = StandardOutput.ReadToEnd();
                string Err = StandardError.ReadToEnd();
                WaitForExit();
                if (ExitCode == 0)
                {
                    return new CommandRes(Out,Err,ExitCode);
                }
                return new CommandRes("", Err, 1);
            }
            catch (Exception e)
            {
                return new CommandRes("", e.Message, 1);
            }
        }
    }
}
