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
                    AnimateHide();
                }
            }
            
            SetValue(IsActiveProperty, value);
        }
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

    private async void Show()
    {
        await Grid.TranslateTo(0, 0, 250U);
    }
}