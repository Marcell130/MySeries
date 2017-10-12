using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace MySeries.UniversalClient.Services
{
    public class ColorService
    {
        private static readonly List<Color> AvailableColors = new List<Color> { Colors.Blue, Colors.Brown, Colors.Green, Colors.Yellow, Colors.Red, Colors.Orange };
        

        public static Color GetRandomColor(int? seed = null)
        {
            var availableCount = AvailableColors.Count;
            var rand = new Random( seed ?? Guid.NewGuid().GetHashCode() );
            var index = rand.Next(0, availableCount - 1);
            return AvailableColors[index];
        }
    }
}
