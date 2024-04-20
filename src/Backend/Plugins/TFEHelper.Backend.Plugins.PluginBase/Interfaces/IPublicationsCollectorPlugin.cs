using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Plugins.PluginBase.Specifications.PublicationsCollector.Classes;

namespace TFEHelper.Backend.Plugins.PluginBase.Interfaces
{
    public interface IPublicationsCollectorPlugin : IBasePlugin
    {
        bool IsOnline();
        Task<IEnumerable<PublicationPLG>> GetPublicationsAsync(PublicationsCollectorParametersPLG searchParameters, CancellationToken cancellationToken = default);
    }
}

#warning implementar Task<IEnumerable<SubjectTypePLG>> GetSubjects(CancellationToken cancellationToken = default); sigue abajo:
// Cada implementador creará una colección conteniendo los subjects disponibles para cada caso contando con los campos "Code" y "Description".
// Dicha colección debería poder ser tomada por la capa de servicios para construir PluginInfo e inyectársela para que front pueda renderizarla
// y así usarla luego como parámetro (el campo "Code" de GetPublicationsAsync (dentro de PublicationsCollectorParametersPLG.Subject).
// Ojo, podría ser mejor PluginRunParametersPLG GetParameters(); Cuidado...  Implementar Strategy para que PluginRunParametersPLG sea diferente
// dependiendo de quien la use...
// Mucho mejor: meter una property IConfigurationParameters dentro de IBasePlugin y listo.