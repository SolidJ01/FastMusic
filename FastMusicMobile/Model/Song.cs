using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastMusicMobile.Model
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public Album? Album { get; set; }
        public string PathInDirectory { get; set; }

    }
}
