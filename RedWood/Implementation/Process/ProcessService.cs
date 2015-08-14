using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedWood.Interface.Process;
using System.Diagnostics;

namespace RedWood.Implementation.Process
{
    public class ProcessService : IProcessService
    {
        public int RunProcessSynchronous(IProcessConfiguration configuration, out int pid)
        {
            var process = new System.Diagnostics.Process();

            process.StartInfo.FileName = configuration.FetchFileName();

            if (!string.IsNullOrEmpty(configuration.FetchArguments()))
            {
                process.StartInfo.Arguments = configuration.FetchArguments();
            }

            process.Start();

            pid = process.Id;

            process.WaitForExit();

            return process.ExitCode;
        }

        public void RunProcessASynchronous(IProcessConfiguration configuration, out int pid)
        {
            var process = new System.Diagnostics.Process();

            process.StartInfo.FileName = configuration.FetchFileName();

            if (!string.IsNullOrEmpty(configuration.FetchArguments()))
            {
                process.StartInfo.Arguments = configuration.FetchArguments();
            }

            process.Start();

            pid = process.Id;
        }

        public void KillProcess(int pid)
        {
            System.Diagnostics.Process.GetProcessById(pid).Kill();
        }

        public void KillProcess(string name)
        {
            foreach (var process in System.Diagnostics.Process.GetProcessesByName(name))
            {
                process.Kill();
            }
        }
    }
}
