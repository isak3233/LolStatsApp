using LoLStatsMaui.Views;

namespace LoLStatsMaui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LolAccountOverviewPage), typeof(LolAccountOverviewPage));
            Routing.RegisterRoute(nameof(CreateUserPage), typeof(CreateUserPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(LinkAccountPage), typeof(LinkAccountPage));
        }
    }
}
