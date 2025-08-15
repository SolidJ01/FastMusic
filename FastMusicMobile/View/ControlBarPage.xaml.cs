using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FastMusicMobile.Model;

namespace FastMusicMobile.View;

public partial class ControlBarPage : ContentPage
{
    public static BindableProperty CurrentlyPlayingProperty = BindableProperty.Create(nameof(CurrentlyPlaying), typeof(Song), typeof(ControlBarPage), null);
    public static BindableProperty PlayPauseCommandProperty = BindableProperty.Create(nameof(PlayPauseCommand), typeof(ICommand), typeof(ControlBarPage), null);
    public static BindableProperty NextCommandProperty = BindableProperty.Create(nameof(NextCommand), typeof(ICommand), typeof(ControlBarPage));
    public static BindableProperty PreviousCommandProperty = BindableProperty.Create(nameof(PreviousCommand), typeof(ICommand), typeof(ControlBarPage));
    public static BindableProperty IsPlayingProperty = BindableProperty.Create(nameof(IsPlaying), typeof(bool), typeof(ControlBarPage), false);
    public static BindableProperty LargePlayerActiveProperty = BindableProperty.Create(nameof(LargePlayerActive), typeof(bool), typeof(ControlBarPage), false);
    public static BindableProperty ShowLargePlayerCommandProperty = BindableProperty.Create(nameof(ShowLargePlayerCommand), typeof(ICommand), typeof(ControlBarPage), null);
    
    public Song? CurrentlyPlaying
    {
        get => (Song)GetValue(CurrentlyPlayingProperty);
        set => SetValue(CurrentlyPlayingProperty, value);
    }
    
    public ICommand PlayPauseCommand
    {
        get => (ICommand)GetValue(PlayPauseCommandProperty);
        set => SetValue(PlayPauseCommandProperty, value);
    }

    public ICommand NextCommand
    {
        get => (ICommand)GetValue(NextCommandProperty);
        set => SetValue(NextCommandProperty, value);
    }
    
    
    public ICommand PreviousCommand
    {
        get => (ICommand)GetValue(PreviousCommandProperty);
        set => SetValue(PreviousCommandProperty, value);
    }

    public bool IsPlaying
    {
        get => (bool)GetValue(IsPlayingProperty);
        set => SetValue(IsPlayingProperty, value);
    }

    public bool LargePlayerActive
    {
        get => (bool)GetValue(LargePlayerActiveProperty);
        set => SetValue(LargePlayerActiveProperty, value);
    }

    public ICommand ShowLargePlayerCommand
    {
        get => (ICommand)GetValue(ShowLargePlayerCommandProperty);
        set => SetValue(ShowLargePlayerCommandProperty, value);
    }
    
    public ControlBarPage()
    {
        InitializeComponent();
        ShowLargePlayerCommand = new Command(ShowLargePlayer);
    }

    private void ShowLargePlayer()
    {
        LargePlayerActive = true;
    }
}