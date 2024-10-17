using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastMusicMobile.Model
{
    internal class Folder
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public List<Song> Songs { get; set; }
    }
}
