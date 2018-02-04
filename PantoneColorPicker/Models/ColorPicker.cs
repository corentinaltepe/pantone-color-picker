using PantoneColorPicker.ViewModels;

namespace PantoneColorPicker.Models
{
    public class ColorPicker : ViewModelBase
    {
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

                // Find a color at every change in the input
                this.Color = PantoneColor.FindColor(inputText);
            }
        }


        public ColorPicker()
        {
            this.InputText = "";
        }
    }
}
