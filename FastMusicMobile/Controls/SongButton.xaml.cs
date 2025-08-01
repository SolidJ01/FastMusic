namespace FastMusicMobile.Controls;

public partial class SongButton : ContentView
{
	public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(SongButton), string.Empty);
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
	}

	public SongButton()
	{
		InitializeComponent();
	}
}