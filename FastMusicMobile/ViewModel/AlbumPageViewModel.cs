using FastMusicMobile.Model;
using FastMusicMobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public AlbumPageViewModel(AudioMasterService audioMasterService)
        {
            _audioMasterService = audioMasterService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> queryAttributes)
        {
            Album = queryAttributes["Album"] as Album;
        }
    }
}
