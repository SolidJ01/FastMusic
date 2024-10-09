using System.Windows.Input;

namespace FastMusicMobile.Controls;

public partial class MusicCollectionButton : ContentView
{
	public static readonly BindableProperty NameProperty = BindableProperty.Create(nameof(Name), typeof(string), typeof(MusicCollectionButton), string.Empty);
	public string Name
	{
		get => (string)GetValue(NameProperty);
		set => SetValue(NameProperty, value);
	}
	
	public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(MusicCollectionButton), string.Empty);
	public string Icon
	{
		get => (string)(GetValue(IconProperty));
		set => SetValue(IconProperty, value);
	}

	public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MusicCollectionButton), null);
	public ICommand Command
	{
		get => (ICommand)GetValue(CommandProperty);
		set => SetValue(CommandProperty, value);
	}

	public MusicCollectionButton()
	{
		InitializeComponent();
	}
}