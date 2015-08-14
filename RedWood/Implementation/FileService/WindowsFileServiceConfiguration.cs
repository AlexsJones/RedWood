using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedWood.Interface.FileService;

namespace RedWood.Implementation.FileService
{
    public class WindowsFileServiceConfiguration : IFileServiceConfiguration
    {
        private string TargetMachine { get; set; }

        public WindowsFileServiceConfiguration(string targetmachine)
        {
            TargetMachine = targetmachine;
        }
        public string FetchRemoteMachinePath()
        {
            return TargetMachine;
        }
    }
}
