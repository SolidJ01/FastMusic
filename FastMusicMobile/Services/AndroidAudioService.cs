#if ANDROID

using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Net;
using Android.OS;
using Android.Provider;
using AndroidX.Core.Content;
using FastMusicMobile.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            PermissionStatus permission = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

            if (permission != PermissionStatus.Granted)
            {
                await Permissions.RequestAsync<Permissions.StorageRead>();
            }

            List<Song> songs = new List<Song>();

            Android.App.Activity activity;
            if (Platform.CurrentActivity is null)
                return songs;
            activity = Platform.CurrentActivity;

            string[] projection = new string[]
            {
                MediaStore.Audio.Media.InterfaceConsts.Id,
                MediaStore.Audio.Media.InterfaceConsts.DisplayName,
                MediaStore.Audio.Media.InterfaceConsts.AlbumId, 
                MediaStore.Audio.Media.InterfaceConsts.IsMusic
            };
            string selection = MediaStore.Audio.Media.InterfaceConsts.IsMusic + " == ?";
            string[] selectionArgs = new string[] {
                true.ToString()
            };
            string sortOrder = MediaStore.Audio.Media.InterfaceConsts.DisplayName + " ASC";

            //MediaStore.Files.GetContentUri(MediaStore.VolumeExternal);
            var cursor = activity.ApplicationContext.ContentResolver.Query( //Android.App.Application.Context.ContentResolver
                //Build.VERSION.SdkInt >= BuildVersionCodes.Q 
                //    ? MediaStore.Audio.Media.GetContentUri(MediaStore.VolumeExternal)
                //    : MediaStore.Audio.Media.ExternalContentUri, 
                MediaStore.Audio.Media.ExternalContentUri,
                projection, 
                null, 
                null, 
                sortOrder
            );

            

            int idColumn = cursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.Id);
            int nameColumn = cursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.DisplayName);
            int albumColumn = cursor.GetColumnIndex(MediaStore.Audio.Media.InterfaceConsts.AlbumId);

            
            while (cursor.MoveToNext())
            {
                long id = cursor.GetLong(idColumn);
                string name = cursor.GetString(nameColumn);
                long album = cursor.GetLong(albumColumn);
                string uri = ContentUris.WithAppendedId(MediaStore.Audio.Media.ExternalContentUri, id).ToString();

                songs.Add(new Song
                {
                    Id = id,
                    Name = name,
                    URI = uri,
                    AlbumId = album
                });

                System.Diagnostics.Debug.WriteLine($"Indexed song {name}");
            }
            cursor.Close();

            return songs;
        }

    }
}

#endif
