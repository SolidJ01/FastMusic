using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FastMusicMobile.Model;

namespace FastMusicMobile.Controls;

public partial class LargePlayer : ContentView
{
    public static BindableProperty IsActiveProperty = BindableProperty.Create(nameof(IsActive), typeof(bool), typeof(LargePlayer), false, propertyChanged: IsActiveChanged);
    public static BindableProperty CurrentlyPlayingProperty = BindableProperty.Create(nameof(CurrentlyPlaying), typeof(Song), typeof(LargePlayer), null);
    public static BindableProperty IsPlayingProperty = BindableProperty.Create(nameof(IsPlaying), typeof(bool), typeof(LargePlayer), false);
    public static BindableProperty PlayPauseCommandProperty = BindableProperty.Create(nameof(PlayPauseCommand), typeof(ICommand), typeof(LargePlayer), null);
    public static BindableProperty NextCommandProperty = BindableProperty.Create(nameof(NextCommand), typeof(ICommand), typeof(LargePlayer), null);
    public static BindableProperty PreviousCommandProperty = BindableProperty.Create(nameof(PreviousCommand), typeof(ICommand), typeof(LargePlayer), null);
    public static BindableProperty ShuffleCommandProperty = BindableProperty.Create(nameof(ShuffleCommand), typeof(ICommand), typeof(LargePlayer));

    private static void IsActiveChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if ((bool)newvalue)
        {
            LargePlayer player = (LargePlayer)bindable;
            player.Show();
        }
    }

    public bool IsActive
    {
        get => (bool)GetValue(IsActiveProperty);
        set
        {
            if (!value)
                AnimateHide();

            SetValue(IsActiveProperty, value);
        }
    }

    public Song CurrentlyPlaying
    {
        get => (Song)GetValue(CurrentlyPlayingProperty);
        set => SetValue(CurrentlyPlayingProperty, value);
    }

    public bool IsPlaying
    {
        get => (bool)GetValue(IsPlayingProperty);
        set => SetValue(IsPlayingProperty, value);
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

    public ICommand ShuffleCommand
    {
        get => (Command)GetValue(ShuffleCommandProperty);
        set => SetValue(ShuffleCommandProperty, value);
    }

    private double _panX = 0;

    public ICommand HideCommand { get; private set; }

    public LargePlayer()
    {
        HideCommand = new Command(Hide);
        InitializeComponent();
    }

    private void Hide()
    {
        IsActive = false;
    }

    private async void AnimateHide()
    {
        await Grid.TranslateTo(0, Grid.Height, 250U, Easing.CubicInOut);
    }

    public async void Show()
    {
        await Grid.TranslateTo(0, 0, 250U, Easing.CubicInOut);
    }

    private void Grid_OnSizeChanged(object? sender, EventArgs e)
    {
        
        if (Grid.TranslationY != 0)
            return;
        Grid.TranslationY = Grid.Height;
    }

    private void PanGestureRecognizer_OnPanUpdated(object? sender, PanUpdatedEventArgs e)
    {
        switch (e.StatusType)
        {
            case GestureStatus.Running:
                Carousel.TranslationX += e.TotalX;
                Carousel.TranslationX = double.Clamp(Carousel.TranslationX, -Carousel.Width, 0);
                break;
            case GestureStatus.Completed:
                
                break;
        }
    }
}