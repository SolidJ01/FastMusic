using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FastMusicMobile.Model;

namespace FastMusicMobile.Controls;

public partial class LargePlayer : ContentView
{
    public static BindableProperty IsActiveProperty = BindableProperty.Create(nameof(IsActive), typeof(bool), typeof(LargePlayer), false, propertyChanged:IsActiveChanged);
    public static BindableProperty CurrentlyPlayingProperty = BindableProperty.Create(nameof(CurrentlyPlaying), typeof(Song), typeof(LargePlayer), null);
    
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
        await Grid.TranslateTo(0, Grid.Height, 250U);
    }

    public async void Show()
    {
        await Grid.TranslateTo(0, 0, 250U);
    }

    private void Grid_OnSizeChanged(object? sender, EventArgs e)
    {
        if (Grid.TranslationY != 0)
            return;
        Grid.TranslationY = Grid.Height;
    }
}