using PantoneColorPicker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantoneColorPicker.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private ColorPicker colorPicker;
        /// <summary>
        /// Used on the right tab to pick any color based on RGB 
        /// or reference number
        /// </summary>
        public ColorPicker ColorPicker
        {
            get { return colorPicker; }
            set
            {
                colorPicker = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<ColorPicker> averageColorPicks;
        /// <summary>
        /// List of color picks to be average to match the best color
        /// </summary>
        public ObservableCollection<ColorPicker> AverageColorPicks
        {
            get { return averageColorPicks; }
            set
            {
                averageColorPicks = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Represents the best averaged color based on the AverageColorPicks list
        /// </summary>
        public PantoneColor AveragedColor {
            get
            {
                // Select only the valid picks
                var picks = AverageColorPicks.Where(u => u.Color != null).ToList();
                if (picks == null || picks.Count == 0) return null;

                // Compute statistics to get the RGB
                double r = picks.Average(u => u.Color.RGB.R);
                double g = picks.Average(u => u.Color.RGB.G);
                double b = picks.Average(u => u.Color.RGB.B);

                // Build the closest color based on the RGB
                return PantoneColor.FindColor(r, g, b);
            }
        }

        public MainViewModel()
        {
            this.ColorPicker = new ColorPicker();
            this.AverageColorPicks = new ObservableCollection<ColorPicker>();

            // The collection can only ADD elements, not remove them. 
            // At every item added, register an event listener to it.
            // Whenever the LAST item of the list is valid (color not null),
            // add an item (which will automatically register a listener again)
            this.AverageColorPicks.CollectionChanged +=
                (e, sender) => this.AverageColorPicks.Last().PropertyChanged +=
                    (ee, sendere) =>
                    {
                        // Add a new color if the last color is valid
                        if (this.AverageColorPicks.Last().Color != null)
                            this.AverageColorPicks.Add(new ColorPicker());

                        // Force recalculation of the statistics
                        NotifyPropertyChanged("AveragedColor");
                    };

            // Add only 1 item, the others will automatically add up
            AverageColorPicks.Add(new ColorPicker());
        }

    }
}
