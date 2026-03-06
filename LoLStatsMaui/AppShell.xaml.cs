using LoLStatsMaui.Views;

namespace LoLStatsMaui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LolAccountOverviewPage), typeof(LolAccountOverviewPage));
        }
    }
}
