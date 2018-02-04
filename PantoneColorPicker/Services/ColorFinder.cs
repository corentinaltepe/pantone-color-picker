using PantoneColorPicker.Interfaces;
using PantoneColorPicker.Models;

namespace PantoneColorPicker.Services
{
    class ColorFinder : IColorFinder
    {
        private readonly IPantoneColorParser _parser;
        private readonly IColorMatcher _matcher;

        public ColorFinder(IPantoneColorParser parser,
             IColorMatcher matcher)
        {
            _parser = parser;
            _matcher = matcher;
        }

        public PantoneColor Find(string userInput)
        {
            var color = _parser.Parse(userInput);
            return _matcher.FindBestMatch(color);
        }
    }
}
