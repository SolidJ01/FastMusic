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
    public class AlbumsPageViewModel : BaseViewModel
    {
        private AudioMasterService _audioService;

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

        public AlbumsPageViewModel(AudioMasterService audioService)
        {
            _audioService = audioService;
        }

        public async Task Initialize()
        {
            Albums = new ObservableCollection<Album>(_audioService.Albums);
        }
    }
}
