using PantoneColorPicker.Interfaces;
using PantoneColorPicker.Services;
using PantoneColorPicker.Services.Parsers;
using PantoneColorPicker.ViewModels;
using System.Collections.Generic;
using System.Windows;

namespace PantoneColorPicker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Initialization - TODO: replace with dependency injection framework

            var catalogs = new List<IPantoneColorCatalog>
            {
                new PantoneUncoatedCatalogReader()
            };
            var catalog = new PantoneColorCatalogAggregate(catalogs);

            var matcher = new ColorMatcher(catalog);

            var parsers = new List<IPantoneColorParser>
            {
                new NameParser(catalog)
            };
            var parser = new PantoneColorParserPipeline(parsers);
            var finder = new ColorFinder(parser, matcher);
            var colorPickerVmFactory = new ColorPickerViewModelFactory(finder);
            
            var mainViewModel = new MainViewModel(colorPickerVmFactory, matcher);
            var window = new MainWindow { DataContext = mainViewModel };
            window.Show();
        }
    }
}
