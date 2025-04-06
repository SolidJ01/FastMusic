using FastMusicMobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastMusicMobile.ViewModel
{
    public class MainPageViewModel : BindableObject
    {
        private AudioMasterService _audioService;

        public MainPageViewModel(AudioMasterService audioService)
        {
            _audioService = audioService;
        }

        public async Task Initialize()
        {
            await _audioService.Initialize();
        }
    }
}
