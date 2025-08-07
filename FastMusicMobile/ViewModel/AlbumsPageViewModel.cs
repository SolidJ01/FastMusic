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
    public class AlbumsPageViewModel : ControlBarPageViewModel
    {

        private ObservableCollection<Album> _albums;
        public ObservableCollection<Album> Albums
        {
            get { return _albums; }
            set
            {
                if (_albums == value)
                    return;
                _albums = value;
                TriggerPropertyChange(nameof(Albums));
            }
        }

        public ICommand GoToAlbumCommand { get; private set; }

        public AlbumsPageViewModel(AudioMasterService audioService) : base(audioService)
        {
            GoToAlbumCommand = new Command<Album>(GoToAlbum);
        }

        public async Task Initialize()
        {
            Albums = new ObservableCollection<Album>(_audioMasterService.Albums);
        }

        private async void GoToAlbum(Album album)
        {
            await Shell.Current.GoToAsync("album", new ShellNavigationQueryParameters { { "Album", album } });
        }
    }
}
