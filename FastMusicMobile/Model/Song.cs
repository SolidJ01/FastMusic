using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastMusicMobile.Model
{
    public class Song
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public long AlbumId { get; set; }
        public string AlbumName { get; set; }
        public Album? Album { get; set; }
        public string PathInDirectory { get; set; }
        public string URI { get; set; }
    }
}
