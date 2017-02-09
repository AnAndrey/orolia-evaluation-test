using System.Collections.Generic;

namespace Executer
{

    public interface IFileManager
    {
        IEnumerable<string> GetFiles(string path);
        void Delete(string path);
        void Move(string source, string destanation);

    }
}