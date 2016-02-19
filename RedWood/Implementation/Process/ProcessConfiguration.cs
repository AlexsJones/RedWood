using RedWood.Interface.Process;

namespace RedWood.Implementation.Process
{
    public class ProcessConfiguration : IProcessConfiguration
    {
        private readonly string _arguments;
        private readonly string _path;

        public ProcessConfiguration(string path, string arguments)
        {
            _path = path;

            _arguments = arguments;
        }

        public ProcessConfiguration(string path)
        {
            _path = path;

            _arguments = string.Empty;
        }

        public string FetchFileName()
        {
            return _path;
        }

        public string FetchArguments()
        {
            return _arguments;
        }
    }
}