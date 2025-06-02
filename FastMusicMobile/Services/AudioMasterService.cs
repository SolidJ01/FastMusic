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
        public event EventHandler MusicRetrieved;

        private readonly IPlatformAudioService _audioService;

        private List<Song> _songs = new();
        public List<Song> Songs { get { return _songs; } }

        private List<Album> _albums = new();
        public List<Album> Albums { get { return _albums; } }

        private List<Playlist> _playlists = new();
        public List<Playlist> Playlists { get { return _playlists; } }

        public AudioMasterService()
        {
#if ANDROID
            _audioService = new AndroidAudioService();
#endif
        }

        public async Task RetrieveMusic()
        {
            await GetSongs();
            await IndexAlbums();
            await GetPlaylists();

            MusicRetrieved?.Invoke(this, new EventArgs());
        }

        public async Task GetSongs()
        {
            _songs = await _audioService.GetSongs();
        }

        private async Task IndexAlbums()
        {
            //_albums = await _audioService.GetAlbums();

            foreach (Song song in _songs)
            {
                if (_albums.Any(x => x.ID.Equals(song.AlbumId)))
                    continue;

                _albums.Add(new Album
                {
                    ID = song.AlbumId,
                    Name = song.AlbumName,
                    Artist = song.Artist
                });
                
                song.Album = _albums.Last();
            }

            foreach (Album album in _albums)
            {
                album.Songs = _songs.Where(x => x.AlbumId.Equals(album.ID)).ToList();
            }
        }

        private async Task GetPlaylists()
        {

        }
    }
}
