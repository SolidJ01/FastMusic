﻿using FastMusicMobile.ViewModel;

namespace FastMusicMobile
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModel _viewModel;

        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            await _viewModel.Initialize();
        }
    }

}
