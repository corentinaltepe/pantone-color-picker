using PantoneColorPicker.Interfaces;
using System.Collections.Generic;
using PantoneColorPicker.Models;
using System.Linq;

namespace PantoneColorPicker.Services
{
    class PantoneColorCatalogAggregate : IPantoneColorCatalog
    {
        private readonly IEnumerable<IPantoneColorCatalog> _catalogs;

        public PantoneColorCatalogAggregate(IEnumerable<IPantoneColorCatalog> catalogs)
        {
            _catalogs = catalogs;
        }

        public IEnumerable<PantoneColor> Colors
        {
            get { return _catalogs.SelectMany(c => c.Colors); }
        }
    }
}
