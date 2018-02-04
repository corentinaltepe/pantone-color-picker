using PantoneColorPicker.Interfaces;
using PantoneColorPicker.Models;
using System.Linq;

namespace PantoneColorPicker.Services
{
    /// <summary>
    /// Finds the closest color within a given catalog
    /// for a given color
    /// </summary>
    class ColorMatcher : IColorMatcher
    {
        /// <summary>
        /// Catalog of colors among which to search the best match
        /// </summary>
        private readonly IPantoneColorCatalog _catalog;

        public ColorMatcher(IPantoneColorCatalog catalog)
        {
            _catalog = catalog;
        }

        public PantoneColor FindBestMatch(PantoneColor color)
        {
            var colors = _catalog.Colors;

            if (colors.Contains(color))
                return color;

            return colors.OrderBy(c => c.Distance(color))
                         .First();
        }
    }
}
