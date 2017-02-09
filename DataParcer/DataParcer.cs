using System;
using System.Collections.Generic;
using System.IO;
using CodeContracts;
using Orolia.DataParcerInterfaces;
using static Orolia.DataParcer.Resource;
namespace Orolia.DataParcer
{
    public class DataParcer: IDataParcer
    {
        public IEnumerable<SSDData> ParceFile(string filePath)
        {
            Requires.NotNullOrEmpty(filePath, "filePath");

            const int countOfParameters = 2;
            var datas = new List<SSDData>();

            using (var reader = File.OpenText(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null && !String.IsNullOrEmpty(line))
                {
                    if (line.IsHeader())
                        continue;

                    var items = line.Split(' ');
                    if (items.Length != countOfParameters)
                        throw new InvalidDataException(Err_InvalidPair);

                    datas.Add(new SSDData() { Mark = items[0], Value = items[1] });
                }
            }

            return datas;
        }
    }
}