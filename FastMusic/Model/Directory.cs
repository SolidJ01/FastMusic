using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastMusic.Model
{
    internal class Directory
    {
        public string Path { get; set; }
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(Path))
                    return string.Empty;

                return Path.Split('\\').Last();
            }
        }
    }
}
