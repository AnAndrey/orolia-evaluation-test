using System.Collections.Generic;


namespace Orolia.DataParcerInterfaces
{ 
    public interface IDataParcer
    {
        IEnumerable<SSDData> ParceFile(string filePath);

    }
}