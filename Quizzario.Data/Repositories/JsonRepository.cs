using Quizzario.Data.Abstracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzario.Data.Repositories
{
    public class JSONRepository : IJSONRepository
    {
        private string StorageDirectoryPath;

        public JSONRepository()
        {
            this.StorageDirectoryPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            this.StorageDirectoryPath += "\\jsonStorage";
            System.IO.Directory.CreateDirectory(this.StorageDirectoryPath);
        }
        public string Load(string filename)
        {
            string filepath = this.StorageDirectoryPath + filename;
            if (File.Exists(filepath))
                return File.ReadAllText(filepath);
            else
                return null;
        }

        public void Save(string filename, string json)
        {
            string filepath = this.StorageDirectoryPath + filename;
            File.WriteAllText(filepath, json);
        }
    }
}
