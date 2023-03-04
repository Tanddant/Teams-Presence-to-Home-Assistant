using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Common.Models {
    public class RGB {
        private int[] colors;
        public RGB(int Red, int Green, int Blue) {
            colors = new int[] { Red, Green, Blue };
            if (colors.Any(color => color < 0 || color > 255)) throw new ArgumentException($"{nameof(RGB)} - All color values must fall within 0 and 255");
        }

        public int Red { get { return colors[0]; } }
        public int Green { get { return colors[1]; } }
        public int Blue { get { return colors[2]; } }

    }
}