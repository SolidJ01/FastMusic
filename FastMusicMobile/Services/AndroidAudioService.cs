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
using static Java.Util.Jar.Attributes;
using Size = Android.Util.Size;

namespace FastMusicMobile.Services
{
    public class AndroidAudioService : IPlatformAudioService
    {

        //private readonly MediaStore _mediaStore;

        public AndroidAudioService()
        {
            //_mediaStore = mediaStore;
        }

        public async Task<List<Song>> GetSongs()
        {
            PermissionStatus permission = await Permissions.CheckStatusAsync<ReadMediaAudio>();

            if (permission != PermissionStatus.Granted)
            {
                permission = await Permissions.RequestAsync<ReadMediaAudio>();
            }

            //  THIS IS THE ONE THAT WORKS
            //if (ContextCompat.CheckSelfPermission(Platform.CurrentActivity, Manifest.Permission.ReadMediaAudio) != Permission.Granted)
            //{
            //    ActivityCompat.RequestPermissions(Platform.CurrentActivity, new string[] { Manifest.Permission.ReadMediaAudio }, 0);
            //    //AndroidX.Activity.Result.Contract.ActivityResultContracts.RequestPermission() // (NOT THIS ONE)
            //}

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
                MediaStore.Audio.Media.InterfaceConsts.IsMusic
            };
            string selection = MediaStore.Audio.Media.InterfaceConsts.IsMusic + " == ?";
            string[] selectionArgs = new string[] {
                true.ToString()
            };
            string sortOrder = MediaStore.Audio.Media.InterfaceConsts.Title + " ASC";

            //MediaStore.Files.GetContentUri(MediaStore.VolumeExternal);
            var cursor = activity.ApplicationContext.ContentResolver.Query( //Android.App.Application.Context.ContentResolver
                Build.VERSION.SdkInt >= BuildVersionCodes.Q 
                    ? MediaStore.Audio.Media.GetContentUri(MediaStore.VolumeExternal)
                    : MediaStore.Audio.Media.ExternalContentUri, 
                //MediaStore.Audio.Media.ExternalContentUri,
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

            
            while (cursor.MoveToNext())
            {
                long id = cursor.GetLong(idColumn);
                string name = cursor.GetString(nameColumn);
                string artist = cursor.GetString(artistColumn);
                string album = cursor.GetString(albumColumn);
                long albumId = cursor.GetLong(albumIdColumn);
                Android.Net.Uri uri = ContentUris.WithAppendedId(Build.VERSION.SdkInt >= BuildVersionCodes.Q ? MediaStore.Audio.Media.GetContentUri(MediaStore.VolumeExternal) : MediaStore.Audio.Media.ExternalContentUri, id);
                Android.Net.Uri albumUri = ContentUris.WithAppendedId(Build.VERSION.SdkInt >= BuildVersionCodes.Q ? MediaStore.Audio.Media.GetContentUri(MediaStore.VolumeExternal) : MediaStore.Audio.Media.ExternalContentUri, albumId);
                /*MemoryStream thumbnail = null;

                try
                {
                    var bitmap = activity.ApplicationContext.ContentResolver.LoadThumbnail(uri, new Android.Util.Size(300, 300), null);
                    thumbnail = new MemoryStream();
                    bitmap.Compress(Bitmap.CompressFormat.Png, 100, thumbnail);
                    bitmap.Recycle();
                }
                catch (Exception e)
                {

                }*/

                songs.Add(new Song
                {
                    Id = id,
                    Name = name,
                    Artist = artist,
                    URI = uri.ToString(),
                    AlbumName = album,
                    AlbumId = albumId,
                    //Thumbnail = thumbnail?.ToArray()
                });
                //thumbnail?.Dispose();

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

    }
}

#endif
