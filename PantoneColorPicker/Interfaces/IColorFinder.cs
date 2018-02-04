using PantoneColorPicker.Models;

namespace PantoneColorPicker.Interfaces
{
    /// <summary>
    /// Parses a user input into a PantoneColor and finds the best match
    /// within the calatogs. This encapsulates a PantoneColorParser and 
    /// a PantoneColorMatcher.
    /// </summary>
    public interface IColorFinder
    {
        PantoneColor Find(string userInput);
    }
}
