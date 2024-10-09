using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastMusic.Model
{
    internal class Song
    {
        public string FileName { get; set; }
        public string Path {
            get { return $"{Directory.Path}\\{FileName}"; }
        }
        public string Name { get; set; }
        //TODO add all the song metadata
        public Directory Directory { get; set; }
    }
}
