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
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        public string Move(string source, string destanation)
        {
            Requires.NotNullOrEmpty(source, "source");
            Requires.NotNullOrEmpty(destanation, "destanation");


            var fileNameOnly = Path.GetFileNameWithoutExtension(destanation);
            var extension = Path.GetExtension(destanation);
            var path = Path.GetDirectoryName(destanation);
            var newFullPath = destanation;
            var newFileName = $"{fileNameOnly}{extension}"; ;

            if (string.IsNullOrEmpty(path)) return newFileName;

            var count = 1;
            while (File.Exists(newFullPath))
            {
                newFileName = $"{fileNameOnly}({count++}){extension}";
                newFullPath = Path.Combine(path, newFileName);
            }

            File.Move(source, newFullPath);
            return newFileName;
        }
    }
}