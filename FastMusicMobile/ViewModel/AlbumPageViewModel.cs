using FastMusicMobile.Model;
using FastMusicMobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FastMusicMobile.ViewModel
{
    public class AlbumPageViewModel : BaseViewModel, IQueryAttributable
    {
        private AudioMasterService _audioMasterService;

        private Album _album;
        public Album Album
        {
            get { return _album; }
            private set
            {
                _album = value;
                TriggerPropertyChange(nameof(Album));
            }
        }

        public string Runtime
        {
            get
            {
                return "0:0";
            }
        }

        public Song CurrentlyPlaying => _audioMasterService.CurrentlyPlaying;
        public bool IsPlaying => _audioMasterService.IsPlaying;
        
        public ICommand PlaySongCommand { get; private set; }
        public ICommand PlayPauseCommand { get; private set; }

        public AlbumPageViewModel(AudioMasterService audioMasterService)
        {
            _audioMasterService = audioMasterService;
            _audioMasterService.CurrentlyPlayingChanged += (sender, args) =>
            {
                TriggerPropertyChange(nameof(CurrentlyPlaying));
            };
            _audioMasterService.PlayingStateChanged += (sender, args) =>
            {
                TriggerPropertyChange(nameof(IsPlaying));
            };
            
            PlaySongCommand = new Command<Song>(PlaySong);
            PlayPauseCommand = new Command(_audioMasterService.PlayPause);
        }

        public void ApplyQueryAttributes(IDictionary<string, object> queryAttributes)
        {
            Album = queryAttributes["Album"] as Album;
        }

        private void PlaySong(Song song)
        {
            _audioMasterService.PlaySong(song);
        }
    }
}
