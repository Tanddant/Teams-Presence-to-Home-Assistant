using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrayIcon {
    internal class TrayAppSettings {
        public TrayAppSettings() {}
        public TrayAppSettings(string[] args) {
            if (args.Length < 5) {
                throw new Exception("Please provide the correct paramaters to run the application (ClientId, TenantId, HomeAssistantUrl, HomeAssistantToken, HomeAssistantLightEntityName)");          
            }  else {
                ClientId = new Guid(args[0]);
                TenantId = new Guid(args[1]);
                HomeAssistantUrl = new Uri(args[2]);
                HomeAssistantToken = new JwtSecurityToken(args[3]);
                HomeAssistantLightEntity = args[4];
            }
        }

        public Guid ClientId { get; set; }
        public Guid TenantId { get; set; }
        public Uri HomeAssistantUrl { get; set; }
        public JwtSecurityToken HomeAssistantToken { get; set; }
        public string HomeAssistantLightEntity { get; set; }
    }
}
