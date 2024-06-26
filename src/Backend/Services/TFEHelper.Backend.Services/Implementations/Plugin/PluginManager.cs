﻿using Microsoft.Extensions.Logging;
using System.Reflection;
using TFEHelper.Backend.Plugins.PluginBase.Common.Classes;
using TFEHelper.Backend.Plugins.PluginBase.Interfaces;
using TFEHelper.Backend.Tools.Assembly;
using TFEHelper.Backend.Tools.Object;

namespace TFEHelper.Backend.Services.Implementations.Plugin
{
    public class PluginManager : IPluginManager
    {
        private readonly ILogger<PluginManager> _logger;
        private readonly ILoggerFactory _loggerFactory;
        private IList<PluginContainer> _plugins;

        public PluginManager(ILogger<PluginManager> logger, ILoggerFactory loggerFactory)
        {
            _logger = logger;
            _plugins = new List<PluginContainer>();
            _loggerFactory = loggerFactory;
        }

        public async Task ScanAsync(CancellationToken cancellationToken) 
        {
            await Task.Run(async () =>
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
                    _logger.LogInformation("Plugin {PluginName} ({PluginVersion}) detected.", plugin.Name, plugin.Version);
                    _logger.LogInformation("Invoking {PluginName} configuration...", plugin.Name);
                    plugin.StartUp(_loggerFactory.CreateLogger(plugin.GetType()));

                    _plugins.Add(new PluginContainer(
                        new PluginInfo()
                        {
                            Id = i,
                            Type = plugin.Type,
                            Name = plugin.Name,
                            Version = plugin.Version,
                            Description = plugin.Description,
                            Parameters = plugin.Implements(typeof(IParametersTypesExposser)) ? await ((IParametersTypesExposser)plugin).GetParametersTypesAsync(cancellationToken) : null
                        }
                        , plugin));
                    i++;
                }

                if (!_plugins.Any()) _logger.LogInformation("No plugins detected.");
            }, cancellationToken);
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