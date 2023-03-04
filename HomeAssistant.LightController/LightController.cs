using HomeAssistant.LightController.Models;
using System.Net.Http.Headers;

namespace HomeAssistant.LightController {
    public class LightController {

        private HttpClient _httpClient;
        private string entity_id;

        public LightController(HomeAssistantConfig cfg, string LightName) {
            entity_id = LightName;

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cfg.Token);
            _httpClient.BaseAddress = new Uri(cfg.Url);
        }


        public async Task TurnOff() {
            await _httpClient.PostAsync("/api/services/light/turn_off", new LightPostBody(entity_id).ToContent());
        }


        public async Task TurnOn(int brightness = 100, int[]? RGB = null) {
            await _httpClient.PostAsync("/api/services/light/turn_on", new LightPostBody(entity_id) { brightness_pct = brightness, rgb_color = RGB, transition = 1 }.ToContent());
        }
    }
}