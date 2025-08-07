#if ANDROID

using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Net;
using Android.OS;
using Android.Provider;
using AndroidX.Activity.Result;
using AndroidX.Activity.Result.Contract;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using FastMusicMobile.Model;
using FastMusicMobile.Services.CustomPermissions;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Media;
using static Java.Util.Jar.Attributes;
using Size = Android.Util.Size;

namespace FastMusicMobile.Services
{
    public class AndroidAudioService : IPlatformAudioService
    {
        public event EventHandler Completed;
        
        private MediaPlayer _mediaPlayer;
        
        public bool IsPlaying => _mediaPlayer.IsPlaying;

        public AndroidAudioService()
        {
            _mediaPlayer = new MediaPlayer();
            _mediaPlayer.SetAudioAttributes(new AudioAttributes.Builder().SetContentType(AudioContentType.Music).SetUsage(AudioUsageKind.Media).Build());
            
            _mediaPlayer.Completion += (sender, args) => {Completed?.Invoke(this, EventArgs.Empty); }; 
        }
        
        public async Task<List<Song>> GetSongs()
        {
            PermissionStatus permission = await Permissions.CheckStatusAsync<ReadMediaAudio>();

            if (permission != PermissionStatus.Granted)
            {
                permission = await Permissions.RequestAsync<ReadMediaAudio>();
            }

            List<Song> songs = new List<Song>();

            Android.App.Activity activity;
            if (Platform.CurrentActivity is null)
                return songs;
            activity = Platform.CurrentActivity;

            string[] projection = new string[]
            {
                MediaStore.Audio.Media.InterfaceConsts.Id,
                MediaStore.Audio.Media.InterfaceConsts.Title,
                MediaStore.Audio.Media.InterfaceConsts.Artist,
                MediaStore.Audio.Media.InterfaceConsts.Album,
                MediaStore.Audio.Media.InterfaceConsts.AlbumId, 
                MediaStore.Audio.Media.InterfaceConsts.IsMusic, 
                MediaStore.Audio.Media.InterfaceConsts.Data
            };
            string selection = MediaStore.Audio.Media.InterfaceConsts.IsMusic + " == ?";
            string[] selectionArgs = new string[] {
                true.ToString()
            };
            string sortOrder = MediaStore.Audio.Media.InterfaceConsts.Title + " ASC";

            var cursor = activity.ApplicationContext.ContentResolver.Query( 
                Build.VERSION.SdkInt >= BuildVersionCodes.Q 
                    ? MediaStore.Audio.Media.GetContentUri(MediaStore.VolumeExternal)
                    : MediaStore.Audio.Media.ExternalContentUri, 
                projection, 
                null, 
                null, 
                sortOrder
            );

            int idColumn = cursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.Id);
            int nameColumn = cursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.Title);
            int artistColumn = cursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.Artist);
            int albumColumn = cursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.Album);
            int albumIdColumn = cursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.AlbumId);
            int dataColumn = cursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.Data);
            
            while (cursor.MoveToNext())
            {
                long id = cursor.GetLong(idColumn);
                string name = cursor.GetString(nameColumn);
                string artist = cursor.GetString(artistColumn);
                string album = cursor.GetString(albumColumn);
                long albumId = cursor.GetLong(albumIdColumn);
                string path = cursor.GetString(dataColumn);
                Android.Net.Uri uri = ContentUris.WithAppendedId(Build.VERSION.SdkInt >= BuildVersionCodes.Q ? MediaStore.Audio.Media.GetContentUri(MediaStore.VolumeExternal) : MediaStore.Audio.Media.ExternalContentUri, id);
                Android.Net.Uri albumUri = ContentUris.WithAppendedId(Build.VERSION.SdkInt >= BuildVersionCodes.Q ? MediaStore.Audio.Media.GetContentUri(MediaStore.VolumeExternal) : MediaStore.Audio.Media.ExternalContentUri, albumId);

                songs.Add(new Song
                {
                    Id = id,
                    Name = name,
                    Artist = artist,
                    URI = uri.ToString(),
                    AlbumName = album,
                    AlbumId = albumId, 
                    Path = path
                });

                System.Diagnostics.Debug.WriteLine($"Indexed song {name}");
            }
            cursor.Close();

            return songs;
        }

        public async Task<byte[]> GetThumbnail(long id)
        {
            Android.Net.Uri uri = ContentUris.WithAppendedId(Build.VERSION.SdkInt >= BuildVersionCodes.Q ? MediaStore.Audio.Media.GetContentUri(MediaStore.VolumeExternal) : MediaStore.Audio.Media.ExternalContentUri, id);

            try
            {
                var bitmap = Platform.CurrentActivity.ApplicationContext.ContentResolver.LoadThumbnail(uri, new Size(300, 300), null);
                MemoryStream ms = new MemoryStream();
                bitmap.Compress(Bitmap.CompressFormat.Png, 100, ms);
                
                return ms.ToArray();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public void Play()
        {
            try
            {
                _mediaPlayer.Start();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public void Pause()
        {
            if (!_mediaPlayer.IsPlaying)
                return;
            
            _mediaPlayer.Pause();
        }

        public void PlaySong(Song song)
        {
            _mediaPlayer.Reset();
            try
            {
                _mediaPlayer.SetDataSource(
                    Platform.CurrentActivity.ApplicationContext,
                    ContentUris.WithAppendedId(
                        Build.VERSION.SdkInt >= BuildVersionCodes.Q
                            ? MediaStore.Audio.Media.GetContentUri(MediaStore.VolumeExternal)
                            : MediaStore.Audio.Media.ExternalContentUri, song.Id));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            _mediaPlayer.Prepare();
            _mediaPlayer.Start();
        }
    }
}

#endif
