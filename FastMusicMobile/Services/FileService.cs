using FastMusicMobile.Model;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastMusicMobile.Services
{
    public class FileService
    {
        private List<Folder> _directories;

        public FileService()
        {
            _directories = new List<Folder>();
            if (_directories.Count == 0)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                //MediaStore
                //path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryMusic).AbsolutePath;
                _directories.Add(
                    new Folder
                    {
                        Path = path
                    });
            }
        }

        public void IndexMusic()
        {
            foreach (Folder folder in _directories)
            {
                List<string> dirs = Directory.EnumerateDirectories(folder.Path).ToList();
                List<string> files = Directory.GetFiles(folder.Path).ToList();
                Debug.Write("\n\n\n\n\n");
                Debug.Write($"Folder: {folder.Path}");
                foreach (string dir in dirs)
                {
                    Debug.Write(dir);
                }
                foreach (string file in files)
                {
                    Debug.Write(file);
                }
                Debug.Write("\n\n\n\n\n");
            }
        }

        public void GetSongFile(Song song) //TODO: different return type
        {

        }
    }
}
