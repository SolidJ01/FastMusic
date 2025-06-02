using FastMusicMobile.Model;
using FastMusicMobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

        private ObservableCollection<Album> _albums;
        public ObservableCollection<Album> Albums
        {
            get { return _albums; }
            set
            {
                _albums = value;
                TriggerPropertyChange(nameof(Albums));
            }
        }

        public ICommand NavigateCommand { private set; get; }

        public MainPageViewModel(AudioMasterService audioService)
        {
            _audioService = audioService;
            Songs = new ObservableCollection<Song>();

            NavigateCommand = new Command<string>(
                execute: (string arg) => Shell.Current.GoToAsync(arg)
            );
        }

        public async Task Initialize()
        {
            if (_audioService.Songs.Count > 0)
                return;

            _audioService.MusicRetrieved += OnMusicRetrieved;
            await _audioService.RetrieveMusic();
        }

        private void OnMusicRetrieved(object? sender, EventArgs e)
        {
            Songs.Clear();
            Songs = new ObservableCollection<Song>(_audioService.Songs.GetRange(0, 10));
            Albums = new ObservableCollection<Album>(_audioService.Albums.GetRange(0, 10));
        }
    }
}
