using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastMusicMobile.Model
{
    public abstract class MusicCollection
    {
        private string _name;
        public string Name { get =>  _name; }

        private List<Song> _songs;
        public List<Song> Songs { get => _songs; }

        public MusicCollection(string name)
        {
            _name = name;
            _songs = new List<Song>();
        }

        public void AddSong(Song song)
        {
            _songs.Add(song);
        }

        public void RemoveSong(Song song)
        {
            _songs.Remove(song);
        }
    }
}
