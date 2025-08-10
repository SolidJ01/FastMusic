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
    public class AlbumPageViewModel : ControlBarPageViewModel, IQueryAttributable
    {
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
        
        public ICommand PlayFromSongCommand { private set; get; }
        public ICommand PlayFromBeginningCommand { private set; get; }

        public AlbumPageViewModel(AudioMasterService audioMasterService) :  base(audioMasterService)
        {
            PlayFromSongCommand = new Command<Song>(PlayFromSong);
            PlayFromBeginningCommand = new Command(PlayFromBeginning);
        }

        public void ApplyQueryAttributes(IDictionary<string, object> queryAttributes)
        {
            Album = queryAttributes["Album"] as Album;
        }

        private void PlayFromSong(Song song)
        {
            _audioMasterService.PlayCollection(_album.Songs, _album.Songs.IndexOf(song));
        }

        private void PlayFromBeginning()
        {
            _audioMasterService.PlayCollection(_album.Songs);
        }
    }
}
