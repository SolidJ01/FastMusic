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
    
    public ControlBarPageViewModel(AudioMasterService audioMasterService)
    {
        _audioMasterService = audioMasterService;
        _audioMasterService.CurrentlyPlayingChanged += OnCurrentlyPlayingChanged;
        _audioMasterService.PlayingStateChanged += OnPlayingStateChanged;
        
        PlaySongCommand = new Command<Song>(_audioMasterService.PlaySong);
        PlayPauseCommand = new Command(_audioMasterService.PlayPause);
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