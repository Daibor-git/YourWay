using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BastrikovTask.Analysys.Helpers
{
    interface IWriterReader<T>
    {
        bool WriteToFile(string nameOfFile, string directoryPath, T objectToSave);

        T ReadFromFile(string path);
    }
}