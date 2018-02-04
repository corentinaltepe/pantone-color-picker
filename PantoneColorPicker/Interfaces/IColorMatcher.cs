using PantoneColorPicker.Models;

namespace PantoneColorPicker.Interfaces
{
    public interface IColorMatcher
    {
        PantoneColor FindBestMatch(PantoneColor color);
    }
}
