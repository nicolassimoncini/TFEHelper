using CsvHelper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TFEHelper.Backend.Core.Plugin.Interfaces;
using TFEHelper.Backend.Plugins.PluginBase.Interfaces;
using TFEHelper.Backend.Tools.Assembly;

namespace TFEHelper.Backend.Core.Plugin.Implementations
{
    public class PluginManager : IPluginManager
    {
        private readonly ILogger<PluginManager> _logger;
        private IEnumerable<IBasePlugin> _plugins;

        public PluginManager(ILogger<PluginManager> logger)
        {
            _logger = logger;
            _plugins = new List<IBasePlugin>();
        }

        public void Scan()
        {
            _logger.LogInformation("Scanning for plugins...");
            var files = Directory.GetFiles(Path.GetDirectoryName(GetType().Assembly.Location)!, "*.dll").ToList();

            _plugins = files.SelectMany(pluginPath =>
            {
                return AssemblyHelper.GetAllImplementersOf<IBasePlugin>(LoadAssembly(pluginPath));
            }).ToList();

            if (_plugins.Any())
            {
                _logger.LogInformation("{0} plugins detected:", _plugins.Count());
                _plugins.ToList().ForEach(p => _logger.LogInformation("--> {0} - v{1}", p.Name, p.Version.ToString()));
            }
            else _logger.LogInformation("No plugins detected.");
        }

        private Assembly LoadAssembly(string assemblyPath)
        {
            var loadContext = new PluginLoadContext(assemblyPath);
            return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(assemblyPath)));
        }

        public IEnumerable<T> GetPlugins<T>() where T : IBasePlugin
        {
            return _plugins.OfType<T>();
        }
    }
}