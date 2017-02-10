using System.Collections.Generic;

namespace Orolia.FileManagerInterfaces
{

    public interface IFileManager
    {
        IEnumerable<string> GetFiles(string path);
        void Delete(string path);
        void Move(string source, string destanation);

    }
}