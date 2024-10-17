#if ANDROID

using Android.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastMusicMobile.Services
{
    public class AndroidFileService : IFileService
    {

        private readonly MediaStore _mediaStore;

        public AndroidFileService(MediaStore mediaStore)
        {
            _mediaStore = mediaStore;
        }

        public async Task<IEnumerable<string>> GetSongsAsync()
        {
            MediaStore.Files.GetContentUri(MediaStore.VolumeExternal);
            var cursor = new Cursor
        }

    }
}

#endif
