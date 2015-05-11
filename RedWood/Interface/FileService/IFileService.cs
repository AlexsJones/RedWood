namespace RedWood.Interface.FileService
{
    public interface IFileService
    {
        bool DoesFileExist(string url);

        string ReadFile(string url);
    }
}
