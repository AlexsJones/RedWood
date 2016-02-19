using RedWood.Interface.FileService;

namespace RedWood.Implementation.FileService
{
    public class WindowsFileServiceConfiguration : IFileServiceConfiguration
    {
        public WindowsFileServiceConfiguration(string targetmachine)
        {
            TargetMachine = targetmachine;
        }

        private string TargetMachine { get; }

        public string FetchRemoteMachinePath()
        {
            return TargetMachine;
        }
    }
}