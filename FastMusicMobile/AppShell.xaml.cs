using FastMusicMobile.View;

namespace FastMusicMobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            Routing.RegisterRoute("albums", typeof(AlbumsPage));

            InitializeComponent();
        }
    }
}
