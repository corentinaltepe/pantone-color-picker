using PantoneColorPicker.Models;

namespace PantoneColorPicker.Interfaces
{    
    /// <summary>
     /// Parses text entered in text fields into a PantoneColor
     /// </summary>
    interface IPantoneColorParser
    {
        /// <summary>
        /// Parse the user input into a Pantone Color. If parsing fails,
        /// throws a PantoneColorParsingFailedException.
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns></returns>
        PantoneColor Parse(string userInput);
    }
}
