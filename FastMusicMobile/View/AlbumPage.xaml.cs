using FastMusicMobile.ViewModel;

namespace FastMusicMobile.View;

public partial class AlbumPage : ContentPage
{
	private AlbumPageViewModel _viewmodel;

	public AlbumPage(AlbumPageViewModel viewModel)
    {
        _viewmodel = viewModel;
        BindingContext = _viewmodel;
        InitializeComponent();
	}
}