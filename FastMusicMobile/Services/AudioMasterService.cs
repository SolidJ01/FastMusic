using FastMusicMobile.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastMusicMobile.Services
{
    public class AudioMasterService
    {
        private readonly IPlatformAudioService _audioService;

        private List<Song> _songs;

        public AudioMasterService()
        {
#if ANDROID
            _audioService = new AndroidAudioService();
#endif

        }

        public async Task Initialize()
        {
            await Task.Run(() =>
            {
                _songs = _audioService.GetSongs().Result;
                Debug.WriteLine($" ||| SONGS INDEXED: {_songs.Count}");
            });
        }
    }
}
