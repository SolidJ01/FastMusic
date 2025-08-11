using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FastMusicMobile.Controls;

public partial class LargePlayer : ContentView
{
    public static BindableProperty IsActiveProperty = BindableProperty.Create(nameof(IsActive), typeof(bool), typeof(LargePlayer), false);

    public bool IsActive
    {
        get => (bool)GetValue(IsActiveProperty);
        set
        {
            if (value != (bool)GetValue(IsActiveProperty))
            {
                if (value is true)
                {
                    Show();
                }
                else
                {
                    Hide();
                }
            }
            
            SetValue(IsActiveProperty, value);
        }
    }
    
    public ICommand HideCommand { get; private set; }
    
    public LargePlayer()
    {
        InitializeComponent();
        Grid.TranslationY = Grid.Height;

        HideCommand = new Command(() => IsActive = false);
    }

    private void Hide()
    {
        Grid.TranslateTo(0, Grid.Height, 250U);
    }

    private void Show()
    {
        Grid.TranslateTo(0, 0, 250U);
    }
}