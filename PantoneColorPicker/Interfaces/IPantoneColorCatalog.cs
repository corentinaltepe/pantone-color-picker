using PantoneColorPicker.Models;
using System.Collections.Generic;

namespace PantoneColorPicker.Interfaces
{
    interface IPantoneColorCatalog
    {
        IEnumerable<PantoneColor> Colors { get; }
    }
}
