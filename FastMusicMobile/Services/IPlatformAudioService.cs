using FastMusicMobile.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastMusicMobile.Services
{
    public interface IPlatformAudioService
    {
        public Task<List<Song>> GetSongs();
    }
}
