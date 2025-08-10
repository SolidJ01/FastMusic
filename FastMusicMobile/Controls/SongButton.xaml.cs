using System.Windows.Input;
using FastMusicMobile.Model;

namespace FastMusicMobile.Controls;

public partial class SongButton : ContentView
{
	public static readonly BindableProperty SongProperty = BindableProperty.Create(nameof(Song), typeof(Song), typeof(SongButton), null, BindingMode.TwoWay);
	public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(SongButton), null);

	public Song Song
	{
		get => (Song)GetValue(SongProperty);
		set => SetValue(SongProperty, value);
	}

	public ICommand Command
	{
		get => (ICommand)GetValue(CommandProperty);
		set => SetValue(CommandProperty, value);
	}

	public SongButton()
	{
		InitializeComponent();
	}
}