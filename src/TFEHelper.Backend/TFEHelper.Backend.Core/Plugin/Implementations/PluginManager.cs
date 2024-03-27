using Microsoft.Extensions.Logging;
using System.Reflection;
using TFEHelper.Backend.Core.Plugin.Interfaces;
using TFEHelper.Backend.Domain.Classes.Models;
using TFEHelper.Backend.Domain.Classes.Plugin;
using TFEHelper.Backend.Plugins.PluginBase.Interfaces;
using TFEHelper.Backend.Tools.Assembly;

namespace TFEHelper.Backend.Core.Plugin.Implementations
{
    public class PluginManager : IPluginManager
    {
        private readonly ILogger<PluginManager> _logger;
        private IList<PluginContainer> _plugins;

        public PluginManager(ILogger<PluginManager> logger)
        {
            _logger = logger;
            _plugins = new List<PluginContainer>();
        }

        public async Task ScanAsync()
        {
            await Task.Run(() => 
            {
                _logger.LogInformation("Scanning for plugins...");
                var files = Directory.GetFiles(Path.GetDirectoryName(GetType().Assembly.Location)!, "*.dll").ToList();

                List<IBasePlugin> plugins = new List<IBasePlugin>();
                foreach (var file in files)
                {
                    plugins.AddRange(AssemblyHelper.GetAllImplementersOf<IBasePlugin>(LoadAssembly(file)));
                }

                var i = 1;
                foreach (var plugin in plugins)
                {
                    _plugins.Add(new PluginContainer()
                    {
                        Info = new PluginInfo()
                        {
                            Id = i,
                            Type = plugin.Type,
                            Name = plugin.Name,
                            Version = plugin.Version,
                            Description = plugin.Description
                        },
                        Plugin = plugin
                    });
                    i++;
                }

                if (_plugins.Any())
                {
                    _logger.LogInformation($"{_plugins.Count()} plugin(s) detected:");
                    _plugins.ToList().ForEach(p => _logger.LogInformation($"--> {p.Info.Name} - v{p.Info.Version}"));
                }
                else _logger.LogInformation("No plugins detected.");
            });
        }

        private Assembly LoadAssembly(string assemblyPath)
        {
            var loadContext = new PluginLoadContext(assemblyPath);
            return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(assemblyPath)));
        }

        public T? GetPlugin<T>(int id) where T : IBasePlugin
        {
            return _plugins
                .Where(p => p.Info.Id == id)
                .Select(p => p.Plugin)
                .OfType<T>()
                .FirstOrDefault();
        }

        public IEnumerable<T> GetPlugins<T>() where T : IBasePlugin
        {
            return _plugins
                .Select(p => p.Plugin)
                .OfType<T>()
                .ToList();
        }

        public PluginContainer? GetPluginContainer(int id)
        {
            return _plugins
                .Where(p => p.Info.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<PluginContainer> GetPluginContainers<T>() where T : IBasePlugin
        {
            return _plugins
                .Where(p => p.Plugin.GetType() is T)
                .ToList();
        }

        public IEnumerable<PluginContainer> GetAllPluginContainers()
        {
            return _plugins.ToList();
        }
    }
}