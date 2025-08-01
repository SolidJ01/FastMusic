using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastMusicMobile.Model
{
    public class Album
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public List<Song> Songs { get; set; }
        public byte[] Thumbnail { get; set; }
    }
}
