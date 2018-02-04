using PantoneColorPicker.Interfaces;
using System.Linq;
using PantoneColorPicker.Models;
using PantoneColorPicker.Exceptions;

namespace PantoneColorPicker.Services.Parsers
{
    /// <summary>
    /// Parses the user input and tries to match it with the color name
    /// </summary>
    class NameParser : PantoneColorParserBase, IPantoneColorParser
    {
        public NameParser(IPantoneColorCatalog catalog) : base(catalog) { }

        public PantoneColor Parse(string userInput)
        {
            userInput = userInput.Replace(" ", "").ToLower();

            // Slow calls here. Can we optimize ?
            var color = _catalog.Colors.Where(c => c.Name.Replace(" ", "").ToLower().Equals(userInput))
                                       .FirstOrDefault();

            if (color == null)
                throw new PantoneColorParsingFailedException("Impossible to match name with: " + userInput);

            return color;
        }
    }
}
