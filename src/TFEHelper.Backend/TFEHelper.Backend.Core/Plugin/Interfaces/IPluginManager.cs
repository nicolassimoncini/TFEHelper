using TFEHelper.Backend.Domain.Classes.Models;
using TFEHelper.Backend.Plugins.PluginBase.Interfaces;

namespace TFEHelper.Backend.Core.Plugin.Interfaces
{
    public interface IPluginManager
    {
        Task ScanAsync();
        T? GetPlugin<T>(int id) where T : IBasePlugin;
        IEnumerable<T> GetPlugins<T>() where T : IBasePlugin;
        PluginContainer? GetPluginContainer(int id);
        IEnumerable<PluginContainer> GetPluginContainers<T>() where T : IBasePlugin;
        IEnumerable<PluginContainer> GetAllPluginContainers();
    }
}
