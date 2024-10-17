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
        private FileService _fileService;

        public MainPageViewModel(FileService fileService)
        {
            _fileService = fileService;
            _fileService.IndexMusic();
        }
    }
}
