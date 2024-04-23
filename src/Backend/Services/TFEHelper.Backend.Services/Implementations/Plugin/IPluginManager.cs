using TFEHelper.Backend.Plugins.PluginBase.Common.Classes;
using TFEHelper.Backend.Plugins.PluginBase.Interfaces;

namespace TFEHelper.Backend.Services.Implementations.Plugin
{
    public interface IPluginManager
    {
        Task ScanAsync(CancellationToken cancellationToken = default);
        T? GetPlugin<T>(int id) where T : IBasePlugin;
        IEnumerable<T> GetPlugins<T>() where T : IBasePlugin;
        PluginContainer? GetPluginContainer(int id);
        IEnumerable<PluginContainer> GetPluginContainers<T>() where T : IBasePlugin;
        IEnumerable<PluginContainer> GetAllPluginContainers();
    }
}
