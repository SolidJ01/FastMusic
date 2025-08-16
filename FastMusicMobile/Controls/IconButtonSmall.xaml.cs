using System.Windows.Input;

namespace FastMusicMobile.Controls;

public partial class IconButtonSmall : ContentView
{
	public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(IconButtonSmall), string.Empty);
	public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(float), typeof(IconButtonSmall), 24.0f);
	public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(IconButtonSmall), null);
	
	public string Icon
	{
		get => (string)GetValue(IconProperty);
		set => SetValue(IconProperty, value);
	}

	public float FontSize
	{
		get => (float)GetValue(FontSizeProperty);
		set => SetValue(FontSizeProperty, value);
	}

	public ICommand Command
	{
		get => (ICommand)GetValue(CommandProperty);
		set => SetValue(CommandProperty, value);
	}

	public IconButtonSmall()
	{
		InitializeComponent();
	}
}