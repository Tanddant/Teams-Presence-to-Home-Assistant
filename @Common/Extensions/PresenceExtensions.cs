using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _Common.Models;
using Microsoft.Graph.Models;

namespace _Common.Extensions {
    public static class PresenceExtensions {
        internal static class TeamsColors {
            public static RGB Green = new RGB(0, 255, 0);
            public static RGB Yellow = new RGB(255, 169, 0);
            public static RGB Red = new RGB(255, 0, 00);
            public static RGB Grey = new RGB(146, 146, 146);
        }


        public static RGB Color(this Presence presence) {
            switch (presence.Availability) {
                case Availability.Available:
                case Availability.AvailableIdle:
                    return TeamsColors.Green;
                case Availability.Away:
                case Availability.BeRightBack:
                    return TeamsColors.Yellow;
                case Availability.Busy:
                case Availability.BusyIdle:
                case Availability.DoNotDisturb:
                    return TeamsColors.Red;
                case Availability.Offline:
                case Availability.PresenceUnknown:
                default:
                    return TeamsColors.Grey;
            }
        }

    }
}
