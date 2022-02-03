using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace Tournament_Life
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // save user settings on upgrade
            if (Tournament_Life.Properties.Settings.Default.UpgradeRequired)
            {
                Tournament_Life.Properties.Settings.Default.Upgrade();
                Tournament_Life.Properties.Settings.Default.UpgradeRequired = false;
                Tournament_Life.Properties.Settings.Default.Save();
            }

            // register syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDI4ODQ0QDMxMzkyZTMxMmUzMGkyUEI1SmVoMkY1cHZmUm5JdngySkNjQXZmdlA2ckVJYVJ6K1RqbkZLT3M9");

            // disable hardware acceleration
            RenderOptions.ProcessRenderMode = RenderMode.SoftwareOnly;
        }
    }
}
