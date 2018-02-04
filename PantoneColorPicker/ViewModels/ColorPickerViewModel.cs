using PantoneColorPicker.Exceptions;
using PantoneColorPicker.Interfaces;
using PantoneColorPicker.Models;

namespace PantoneColorPicker.ViewModels
{
    public class ColorPickerViewModel : ViewModelBase
    {
        private readonly IColorFinder _colorFinder;

        public ColorPickerViewModel(IColorFinder colorFinder)
        {
            _colorFinder = colorFinder;
            InputText = "";
        }

        private PantoneColor color;
        /// <summary>
        /// Color selected. Either a color available in the Pantone's catalog or null
        /// </summary>
        public PantoneColor Color
        {
            get { return color; }
            set
            {
                color = value;
                NotifyPropertyChanged();
            }
        }
        
        private string inputText;
        /// <summary>
        /// Text input by the user to select the color
        /// </summary>
        public string InputText
        {
            get { return inputText; }
            set
            {
                inputText = value;
                NotifyPropertyChanged();

                // Try to find a color at every change in the input
                try
                {
                    Color = _colorFinder.Find(inputText);
                }
                catch (PantoneColorParsingFailedException ex)
                {
                    // No match found
                    Color = null;
                }
            }
        }
    }
}
