using _Common;
using _Common.Extensions;
using Azure.Identity;
using HomeAssistant.LightController;

namespace TrayIcon {
    internal static class Program {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
            TrayAppSettings ts;

            try {
                ts = new TrayAppSettings(args);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return;
            }
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Daemon(ts));
        }
    }
}
