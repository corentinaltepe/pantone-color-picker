using PantoneColorPicker.Interfaces;
using PantoneColorPicker.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace PantoneColorPicker.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private readonly IColorPickerViewModelFactory _colorPickerViewModelFactory;
        private readonly IColorMatcher _matcher;

        public MainViewModel(
            IColorPickerViewModelFactory colorPickerViewModelFactory,
            IColorMatcher matcher)
        {
            _colorPickerViewModelFactory = colorPickerViewModelFactory;
            _matcher = matcher;

            Initialize();
        }

        private ColorPickerViewModel colorPicker;
        /// <summary>
        /// Used on the right tab to pick any color based on RGB 
        /// or reference number
        /// </summary>
        public ColorPickerViewModel ColorPicker
        {
            get { return colorPicker; }
            set
            {
                colorPicker = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<ColorPickerViewModel> averageColorPicks;
        /// <summary>
        /// List of color picks to be averaged to match the best color
        /// </summary>
        public ObservableCollection<ColorPickerViewModel> AverageColorPicks
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
        /// TODO: move to a business logic service
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
                var color = new PantoneColor
                {
                    RGB = new RGB((byte)r, (byte)g, (byte)b)
                };

                // Build the closest color based on the RGB
                return _matcher.FindBestMatch(color);
            }
        }

        private void Initialize()
        {
            this.ColorPicker = _colorPickerViewModelFactory.Create();
            this.AverageColorPicks = new ObservableCollection<ColorPickerViewModel>();

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
                            this.AverageColorPicks.Add(_colorPickerViewModelFactory.Create());

                        // Force recalculation of the statistics
                        NotifyPropertyChanged("AveragedColor");
                    };

            // Add only 1 item, the others will automatically add up
            AverageColorPicks.Add(_colorPickerViewModelFactory.Create());
        }

    }
}
