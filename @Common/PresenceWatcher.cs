using _Common.Models;
using Azure.Core;
using Azure.Identity;
using HomeAssistant.LightController;
using Microsoft.Graph;
using Microsoft.Graph.Me.Presence;
using Microsoft.Graph.Models;

namespace _Common {
    public class PresenceWatcher {
        private GraphServiceClient GraphClient;
        public PresenceWatcher(GraphServiceClient Client) {
            GraphClient = Client;
        }
        public PresenceWatcher(TokenCredential tokenCredential) {
            GraphClient = new GraphServiceClient(tokenCredential);
        }

        private List<Action<Presence>> Observer = new List<Action<Presence>>();
        private string _PrevAvailability = "";
        private string _PrevActivity = "";
        private bool KeepRunning = false;


        public async Task Start(int PullingRateInSeconds = 5) {
            if (PullingRateInSeconds <= 0) throw new Exception($"{nameof(PresenceWatcher)} - Pulling rate must be greater than 0!");
            Start(TimeSpan.FromSeconds(PullingRateInSeconds));
        }


        public async Task Start(TimeSpan PullingRate) {
            if (Observer.Count == 0) throw new Exception($"{nameof(PresenceWatcher)} - You cannot start the watcher without providing a observer first!");
            if (PullingRate <= TimeSpan.Zero) throw new Exception($"{nameof(PresenceWatcher)} - You cannot start the watcher multiple times!");
            if (KeepRunning) throw new Exception($"{nameof(PresenceWatcher)} - You cannot start the watcher multiple times!");

            KeepRunning = true;
            while (KeepRunning) {
                Presence status = GraphClient.Me.Presence.GetAsync().Result;
                if (_PrevActivity != status.Activity || _PrevAvailability != status.Activity) {
                    NotifyObservers(status);
                    _PrevAvailability = status.Availability;
                    _PrevActivity = status.Activity;
                }

                Thread.Sleep(PullingRate);
            }
        }


        private void NotifyObservers(Presence update) {
            foreach (var observer in Observer) {
                observer(update);
            }
        }

        public void Subscribe(Action<Presence> action) {
            Observer.Add(action);
        }

        public void Stop() {
            if (!KeepRunning) throw new Exception($"{nameof(PresenceWatcher)} - Error cannot stop a non running watcher!");
            KeepRunning = false;
        }
    }
}