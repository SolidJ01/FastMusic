using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;
using AndroidX.Core.Content;

namespace FastMusicMobile
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        //protected override void OnCreate(Bundle? savedInstanceState)
        //{
        //    base.OnCreate(savedInstanceState);

        //    if (ContextCompat.CheckSelfPermission(Platform.CurrentActivity, Manifest.Permission.ReadMediaAudio) != Permission.Granted)
        //    {
        //        ActivityCompat.RequestPermissions(Platform.CurrentActivity, new string[] { Manifest.Permission.ReadMediaAudio }, 0);
        //        //AndroidX.Activity.Result.Contract.ActivityResultContracts.RequestPermission() // (NOT THIS ONE)
        //    }
        //}
    }
}
