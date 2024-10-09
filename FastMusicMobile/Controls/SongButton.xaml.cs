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

	public SongButton()
	{
		InitializeComponent();
	}
}