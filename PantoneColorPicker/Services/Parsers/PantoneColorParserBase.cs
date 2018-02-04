using PantoneColorPicker.Interfaces;

namespace PantoneColorPicker.Services.Parsers
{
    class PantoneColorParserBase
    {
        protected readonly IPantoneColorCatalog _catalog;

        protected PantoneColorParserBase(IPantoneColorCatalog catalog)
        {
            _catalog = catalog;
        }
    }
}
