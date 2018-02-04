using PantoneColorPicker.Interfaces;

namespace PantoneColorPicker.ViewModels
{
    class ColorPickerViewModelFactory : IColorPickerViewModelFactory
    {
        private readonly IColorFinder _finder;

        public ColorPickerViewModelFactory(IColorFinder finder)
        {
            _finder = finder;
        }

        public ColorPickerViewModel Create()
        {
            return new ColorPickerViewModel(_finder);
        }
    }
}
