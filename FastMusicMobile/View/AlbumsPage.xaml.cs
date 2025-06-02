using FastMusicMobile.ViewModel;

namespace FastMusicMobile.View;

public partial class AlbumsPage : ContentPage
{
	private AlbumsPageViewModel _viewModel;

	public AlbumsPage(AlbumsPageViewModel viewModel)
	{
		_viewModel = viewModel;
		this.BindingContext = _viewModel;
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await _viewModel.Initialize();
    }
}