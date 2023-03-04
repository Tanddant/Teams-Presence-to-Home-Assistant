using _Common;
using _Common.Extensions;
using Azure.Identity;
using HomeAssistant.LightController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrayIcon {
    internal class Daemon : ApplicationContext {
        PresenceWatcher presenceWatcher;
        LightController light;
        NotifyIcon trayIcon;

        public Daemon(TrayAppSettings ts) {
            SetupLights(ts.HomeAssistantUrl.ToString(), ts.HomeAssistantLightEntity, ts.HomeAssistantToken.RawData);
            SetupPresenceWatcher(ts.TenantId.ToString(), ts.ClientId.ToString());
            SetupTrayicon();

            presenceWatcher.Subscribe(async (p) => {
                var color = p.Color();
                await light.TurnOn(100, new int[] { color.Red, color.Green, color.Blue });
            });

            new Thread(() => presenceWatcher.Start()).Start();
        }

        private void SetupTrayicon() {
            trayIcon = new NotifyIcon() {
                Icon = new Icon("icon.ico"),
                ContextMenuStrip = new ContextMenuStrip(),
                Visible = true
            };

            trayIcon.ContextMenuStrip.Items.AddRange(new ToolStripItem[]{
                new ToolStripMenuItem("Exit", null, new EventHandler((_,_) => ShutDown())),
                new ToolStripMenuItem("By Dan Toft for Graph hack-together"){ Enabled = false }
            });
        }

        private void SetupPresenceWatcher(string tenantId, string clientId) {
            var options = new InteractiveBrowserCredentialOptions {
                TenantId = tenantId,
                ClientId = clientId,
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
                RedirectUri = new Uri("http://localhost"),
            };

            InteractiveBrowserCredential Credentials = new InteractiveBrowserCredential(options);
            presenceWatcher = new PresenceWatcher(Credentials);
        }

        private void SetupLights(string url, string lightEntity, string token) {
            light = new LightController(new HomeAssistant.LightController.Models.HomeAssistantConfig() {
                Token = token,
                Url = url
            }, lightEntity);
        }

        private void ShutDown() {
            presenceWatcher.Stop();
            light.TurnOff();
            trayIcon.Dispose();

            Application.Exit();
        }
    }
}
