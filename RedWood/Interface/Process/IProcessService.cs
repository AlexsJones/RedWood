namespace RedWood.Interface.Process
{
    public interface IProcessService
    {
        int RunProcessSynchronous(IProcessConfiguration configuration, out int pid);
        void RunProcessASynchronous(IProcessConfiguration configuration, out int pid);
        void KillProcess(int pid);
        void KillProcess(string name);
    }
}