using System.Collections.Generic;
using System.IO;
using CodeContracts;

namespace Executer
{
    public class FileManager:IFileManager
    {
        public IEnumerable<string> GetFiles(string path)
        {
            Requires.NotNullOrEmpty(path, "path");
            return Directory.GetFiles(path);
        }
        public void Delete(string path)
        {
            Requires.NotNullOrEmpty(path, "path");
            Directory.Delete(path);
        }
        public void Move(string source, string destanation)
        {
            Requires.NotNullOrEmpty(source, "source");
            Requires.NotNullOrEmpty(destanation, "destanation");
            Directory.Move(source,destanation);
        }
    }
}