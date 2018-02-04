using PantoneColorPicker.Exceptions;
using PantoneColorPicker.Interfaces;
using PantoneColorPicker.Models;
using System;
using System.Linq;

namespace PantoneColorPicker.Services.Parsers
{
    class RgbParser : PantoneColorParserBase, IPantoneColorParser
    {
        private readonly char _separator;

        public RgbParser(
            IPantoneColorCatalog catalog,
            char separator) : base(catalog)
        {
            _separator = separator;
        }

        public PantoneColor Parse(string userInput)
        {
            var input = userInput.Split(_separator);

            if (input.Count() < 3)
                throw new PantoneColorParsingFailedException("Parsing RGB failed");

            try
            {
                // Create RGB
                var rgb = new RGB((byte)(Int32.Parse(input[0])),
                            (byte)(Int32.Parse(input[1])),
                            (byte)(Int32.Parse(input[2])));

                return new PantoneColor
                {
                    RGB = rgb
                };
            }
            catch(Exception ex)
            {
                throw new PantoneColorParsingFailedException("Parsing RGB failed", ex);
            }
        }
    }
}
