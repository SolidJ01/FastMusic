using System.Windows.Input;

namespace FastMusicMobile.Controls;

public partial class AlbumButton : ContentView
{
	public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(AlbumButton), String.Empty);
	public string Title
	{
		get => (string)GetValue(TitleProperty);
		set => SetValue(TitleProperty, value);
	}

	public static readonly BindableProperty ArtistProperty = BindableProperty.Create(nameof(Artist), typeof(string), typeof(AlbumButton), String.Empty);
	public string Artist
	{
		get => (string)GetValue(ArtistProperty);
		set => SetValue(ArtistProperty, value);
	}

	public static readonly BindableProperty ThumbnailProperty = BindableProperty.Create(nameof(Thumbnail), typeof(Byte[]), typeof(AlbumButton), null);
	public Byte[] Thumbnail
	{
		get => (Byte[])GetValue(ThumbnailProperty);
		set => SetValue(ThumbnailProperty, value);
	}

	public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(AlbumButton), null);
	public ICommand Command
	{
		get => (ICommand)GetValue(CommandProperty);
		set => SetValue(CommandProperty, value);
	}

	public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(AlbumButton), null);
	public object CommandParameter
	{
		get => GetValue(CommandParameterProperty);
		set => SetValue(CommandParameterProperty, value);
	}

	public AlbumButton()
	{
		InitializeComponent();
	}
}