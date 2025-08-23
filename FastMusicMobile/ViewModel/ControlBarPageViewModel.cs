using System.Windows.Input;
using FastMusicMobile.Model;
using FastMusicMobile.Services;

namespace FastMusicMobile.ViewModel;

public class ControlBarPageViewModel : BaseViewModel
{
    protected AudioMasterService _audioMasterService;

    public Song? CurrentlyPlaying => _audioMasterService.CurrentlyPlaying;
    public bool IsPlaying => _audioMasterService.IsPlaying;

    public ICommand PlaySongCommand { get; private set; }
    public ICommand PlayPauseCommand { get; private set; }
    public ICommand NextCommand { get; private set; }
    public ICommand PreviousCommand { get; private set; }
    public ICommand ShuffleCommand { get; private set; }
    
    public ControlBarPageViewModel(AudioMasterService audioMasterService)
    {
        _audioMasterService = audioMasterService;
        _audioMasterService.CurrentlyPlayingChanged += OnCurrentlyPlayingChanged;
        _audioMasterService.PlayingStateChanged += OnPlayingStateChanged;
        
        PlaySongCommand = new Command<Song>(PlaySong);
        PlayPauseCommand = new Command(_audioMasterService.PlayPause);
        NextCommand = new Command(_audioMasterService.NextInCollection);
        PreviousCommand = new Command(_audioMasterService.PreviousInCollection);
        ShuffleCommand = new Command(_audioMasterService.ShuffleCollection);
    }

    private void PlaySong(Song song)
    {
        _audioMasterService.PlayCollection(new List<Song> { song });
    }

    private void OnCurrentlyPlayingChanged(object? sender, EventArgs e)
    {
        TriggerPropertyChange(nameof(CurrentlyPlaying));
    }

    private void OnPlayingStateChanged(object? sender, EventArgs e)
    {
        TriggerPropertyChange(nameof(IsPlaying));
    }
}