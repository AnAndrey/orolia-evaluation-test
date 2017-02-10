using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodeContracts;
using Orolia.FileManagerInterfaces;

namespace Orolia.FileManager
{
    public class FileManager:IFileManager
    {
        public IEnumerable<string> GetFiles(string path)
        {
            Requires.NotNullOrEmpty(path, "path");
            var fullFileNames = Directory.GetFiles(path).ToList();

            return fullFileNames.Select(Path.GetFileName);
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