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
        public event EventHandler CurrentlyPlayingChanged;
        public event EventHandler PlayingStateChanged;

        private readonly IPlatformAudioService _audioService;

        private List<Song> _songs = new();
        public List<Song> Songs { get { return _songs; } }

        private List<Album> _albums = new();
        public List<Album> Albums { get { return _albums; } }

        private List<Playlist> _playlists = new();
        public List<Playlist> Playlists { get { return _playlists; } }

        //private Song? _currentlyPlaying;
        private List<Song>? _currentCollection = new();
        private List<Song>? _currentCollectionPlayed = new();
        public Song? CurrentlyPlaying
        {
            get { return _currentCollection.Count == 0 ? null : _currentCollection.First(); }
        }
        public bool IsPlaying => _audioService.IsPlaying;

        public AudioMasterService()
        {
#if ANDROID
            _audioService = new AndroidAudioService();
#endif
            
            //_audioService.Completed += (sender, args) => PlayingStateChanged?.Invoke(this, EventArgs.Empty);
            _audioService.Completed += (sender, args) => NextInCollection();
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
                    Artist = song.Artist, 
                    Thumbnail = await _audioService.GetThumbnail(song.Id)
                });
                
                song.Album = _albums.Last();
            }

            foreach (Album album in _albums)
            {
                album.Songs = _songs.Where(x => x.AlbumId.Equals(album.ID)).ToList();
                foreach (Song song in album.Songs)
                {
                    song.Thumbnail = album.Thumbnail;
                }
            }

            _albums = _albums.OrderBy(x => x.Name).ToList();
        }

        private async Task GetPlaylists()
        {

        }

        public void PlayPause()
        {
            if (_audioService.IsPlaying)
                _audioService.Pause();
            else
                _audioService.Play();
            
            PlayingStateChanged?.Invoke(this, new EventArgs());
        }

        public void PlaySong(Song song)
        {
            _audioService.PlaySong(song);
            //_currentlyPlaying = song;
            PlayingStateChanged?.Invoke(this, new EventArgs());
            CurrentlyPlayingChanged?.Invoke(this, new EventArgs());
        }

        public void PlayCollection(List<Song> songs, int index = 0)
        {
            _currentCollection = songs.GetRange(index, songs.Count - index);
            _currentCollection.AddRange(songs.GetRange(0, index));
            _currentCollectionPlayed.Clear();
            PlaySong(CurrentlyPlaying);
        }

        private void NextInCollection()
        {
            _currentCollectionPlayed.Add(CurrentlyPlaying);
            _currentCollection.Remove(CurrentlyPlaying);
            if (_currentCollection.Count > 0)
            {
                PlaySong(CurrentlyPlaying);
            }
            else
            {
                _currentCollection = _currentCollectionPlayed;
                _currentCollectionPlayed = new();
                _audioService.SetActiveSong(CurrentlyPlaying);
                PlayingStateChanged?.Invoke(this, new EventArgs());
                CurrentlyPlayingChanged?.Invoke(this, new EventArgs());
            }
        }
    }
}
