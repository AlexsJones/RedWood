using RedWood.Interface.FileService;

namespace RedWood.Implementation.FileService
{
    public class WindowsFileServiceConfiguration : IFileServiceConfiguration
    {
        public WindowsFileServiceConfiguration(string targetmachine)
        {
            _targetMachine = targetmachine;
        }

        private readonly string _targetMachine;

        public string FetchRemoteMachinePath()
        {
            return _targetMachine;
        }
    }
}