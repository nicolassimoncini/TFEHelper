using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Plugins.PluginBase.Tools
{
    public sealed class PluginConfigurationController
    {
        private List<PluginConfigurationItem> _items { get; set; }
        private readonly string _configurationFilePath;
        private readonly ILogger _logger;

        public PluginConfigurationController(ILogger logger, string configurationFilePath = "", bool autoLoad = true)
        {
            var hardCoddedFilePath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\" + Assembly.GetCallingAssembly().GetName().Name + ".cfg";

            _configurationFilePath = (configurationFilePath != string.Empty) ? configurationFilePath : hardCoddedFilePath;
            _logger = logger;
            _items = new List<PluginConfigurationItem>();

            if (autoLoad) Load();
        }

        public void Load() 
        {
            try
            {
                _logger.LogInformation("Loading configuration from {ConfigFile}...", _configurationFilePath);
                
                string jsonString = File.ReadAllText(_configurationFilePath);
                _items = JsonSerializer.Deserialize<List<PluginConfigurationItem>>(jsonString);

                _logger.LogInformation("Configuration items ({Count}) loaded!", _items.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Configuration could not be loaded!");
            }
        }

        public void Save()
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(_items);
                File.WriteAllText(_configurationFilePath, jsonString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Configuration could not be saved!");
            }
        }

        public T Get<T>(string name)
        {
            object result = _items.Where(i => i.Name == name).FirstOrDefault()?.Value;
            return (result != null) ? (T)Convert.ChangeType(result, typeof(T)) : default;
        }
    }
}
#warning investigar por qué al debuggear, la base de datos se busca en "D:\I&D\Sandbox\UTN\TFE\TFEHelper\src\TFEHelper.Backend\TFEHelper.Backend.API\"!
