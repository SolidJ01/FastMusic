using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastMusicMobile.Services.CustomPermissions
{
    public class ReadMediaAudio : Permissions.BasePlatformPermission
    {
#if ANDROID

        public override (string androidPermission, bool isRuntime)[] RequiredPermissions => [
                ("android.permission.READ_MEDIA_AUDIO", true)
            ];

#endif
    }
}
