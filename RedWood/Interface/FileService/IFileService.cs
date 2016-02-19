namespace RedWood.Interface.FileService
{
    public interface IFileService
    {
        FileServiceFileType FetchType(string path);
        bool DoesFileExist(string path);
        bool DoesDirectoryExist(string path);
        string ReadFile(string path);
        void CopyFile(string apath, string bpath);
        void CopyDirectory(string apath, string bpath, bool ignoreHidden);
        bool DeleteFile(string path);
        bool DeleteDirectory(string path);
    }
}