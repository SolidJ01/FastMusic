using FastMusicMobile.Model;
using FastMusicMobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastMusicMobile.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        private AudioMasterService _audioService;

        private ObservableCollection<Song> _songs;
        public ObservableCollection<Song> Songs
        {
            get { return _songs; }
            set
            {
                _songs = value;
                TriggerPropertyChange(nameof(Songs));
            }
        }

        public MainPageViewModel(AudioMasterService audioService)
        {
            _audioService = audioService;
            Songs = new ObservableCollection<Song>();
        }

        public async Task Initialize()
        {
            _audioService.MusicRetrieved += OnMusicRetrieved;
            await _audioService.RetrieveMusic();
        }

        private void OnMusicRetrieved(object? sender, EventArgs e)
        {
            Songs.Clear();
            Songs = new ObservableCollection<Song>(_audioService.Songs.GetRange(0, 10));
        }
    }
}
