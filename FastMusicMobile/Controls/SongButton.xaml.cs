using System.Windows.Input;
using FastMusicMobile.Model;

namespace FastMusicMobile.Controls;

public partial class SongButton : ContentView
{
	/*public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(SongButton), string.Empty);
	public string Title
	{
		get => (string)GetValue(TitleProperty);
		set => SetValue(TitleProperty, value);
	}

	public static readonly BindableProperty ArtistProperty = BindableProperty.Create(nameof(Artist), typeof(string), typeof(SongButton), string.Empty);
    public string Artist
	{
		get => (string)GetValue(ArtistProperty);
		set => SetValue(ArtistProperty, value);
	}

	public static readonly BindableProperty ThumbnailProperty = BindableProperty.Create(nameof(Thumbnail), typeof(byte[]), typeof(SongButton), null);
	public byte[] Thumbnail
	{
		get => (byte[])GetValue(ThumbnailProperty);
		set => SetValue(ThumbnailProperty, value);
	}*/
	public static readonly BindableProperty SongProperty = BindableProperty.Create(nameof(Song), typeof(Song), typeof(SongButton), null, BindingMode.TwoWay);

	public Song Song
	{
		get => (Song)GetValue(SongProperty);
		set => SetValue(SongProperty, value);
	}

	public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(SongButton), null);

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