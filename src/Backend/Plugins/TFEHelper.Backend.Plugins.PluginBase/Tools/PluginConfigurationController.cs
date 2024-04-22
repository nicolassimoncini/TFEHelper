using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Text.Json;
using TFEHelper.Backend.Plugins.PluginBase.Common.Classes;

namespace TFEHelper.Backend.Plugins.PluginBase.Tools
{
    public sealed class PluginConfigurationController
    {
        private List<NamedKeyValuePair<string>> _items { get; set; }
        private readonly string _configurationFilePath;
        private readonly ILogger _logger;

        public PluginConfigurationController(ILogger logger, string configurationFilePath = "", bool autoLoad = true)
        {
            var hardCoddedFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location), Assembly.GetCallingAssembly().GetName().Name) + ".cfg";
            
            _configurationFilePath = (configurationFilePath != string.Empty) ? configurationFilePath : hardCoddedFilePath;
            _logger = logger;
            _items = new List<NamedKeyValuePair<string>>();

            if (autoLoad) Load();
        }

        public void Load() 
        {
            try
            {
                _logger.LogInformation("Loading configuration from {ConfigFile}...", _configurationFilePath);
                
                string jsonString = File.ReadAllText(_configurationFilePath);
                _items = JsonSerializer.Deserialize<List<NamedKeyValuePair<string>>>(jsonString);

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