using PantoneColorPicker.Exceptions;
using PantoneColorPicker.Interfaces;
using PantoneColorPicker.Models;
using System;
using System.Linq;

namespace PantoneColorPicker.Services.Parsers
{
    class CmykParser : PantoneColorParserBase, IPantoneColorParser
    {
        private readonly char _separator;

        public CmykParser(
            IPantoneColorCatalog catalog,
            char separator) : base(catalog)
        {
            _separator = separator;
        }

        public PantoneColor Parse(string userInput)
        {
            var input = userInput.Split(_separator);

            if (input.Count() < 4)
                throw new PantoneColorParsingFailedException("Parsing RGB failed");

            try
            {
                var cmyk = new CMYK((byte)(Int32.Parse(input[0])),
                            (byte)(Int32.Parse(input[1])),
                            (byte)(Int32.Parse(input[2])),
                            (byte)(Int32.Parse(input[3])));

                return new PantoneColor
                {
                    CMYK = cmyk
                };
            }
            catch (Exception ex)
            {
                throw new PantoneColorParsingFailedException("Parsing CMYK failed", ex);
            }
        }
    }
}
