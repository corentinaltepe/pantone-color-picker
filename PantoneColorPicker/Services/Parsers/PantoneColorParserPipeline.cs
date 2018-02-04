using PantoneColorPicker.Exceptions;
using PantoneColorPicker.Interfaces;
using PantoneColorPicker.Models;
using System.Collections.Generic;

namespace PantoneColorPicker.Services.Parsers
{
    /// <summary>
    /// Pipeline of user input parsers: tries and parses the user input
    /// with each of the parsers until one parser succeeds. Throws PantoneColorParsingFailedException
    /// if none succeeds.
    /// </summary>
    class PantoneColorParserPipeline : IPantoneColorParser
    {
        /// <summary>
        /// Ordered list of the parsers in the pipeline
        /// </summary>
        private readonly IList<IPantoneColorParser> _parsers;

        public PantoneColorParserPipeline(IList<IPantoneColorParser> parsers)
        {
            _parsers = parsers;
        }

        public PantoneColor Parse(string userInput)
        {
            // Go through each of the parsers
            foreach(var parser in _parsers)
            {
                try
                {
                    return parser.Parse(userInput);
                }
                catch (PantoneColorParsingFailedException ex)
                {
                    // Nothing to do here
                }
            }

            // If this line is reached, no parser has succeeded
            throw new PantoneColorParsingFailedException("Parsing user input impossible");
        }
    }
}
