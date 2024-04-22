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
// Cada tipo de plug in necesitará informar bajo qué condiciones estos se deben ejecutar y cuales serán sus parámetros de ejecución.
// Para el caso de IPublicationsCollectorPlugin, éste deberá informar cual es la lista de subjects (colección de llave/valor).
// El problema que veo es que esto puede ser infinito si se piensa de forma genérica.  Tip: ver el patrón Visitor...
//
// (1) Cada implementador creará una colección conteniendo los subjects disponibles para cada caso contando con los campos "Code" y "Description".
// Dicha colección debería poder ser tomada por la capa de servicios para construir PluginInfo e inyectársela para que front pueda renderizarla
// y así usarla luego como parámetro (el campo "Code" de GetPublicationsAsync (dentro de PublicationsCollectorParametersPLG.Subject).
//
// (2) Ojo, podría ser mejor PluginRunParametersPLG GetParameters(); Cuidado...  Implementar Strategy para que PluginRunParametersPLG sea diferente
// dependiendo de quien la use...
//
// (3) Mucho mejor: meter una property IConfigurationParameters dentro de IBasePlugin u otra intefaz (IFeedbackConfigurablePlugin?) y listo (eso lo meto en
// PluginInfo y chau problema).  La estructura debería ser super genérica tipo:
//
// public class ConfigurationElement : IFeedbackConfigurationElement
// {
//     public string Name { get; set; }
//     public object Value { get; set; }
// }
//
// ConfigurationElement.Value podría ser:
// public class ConfigurationItem
// {
//     public string Name { get; set; }
//     public dynamic Value  { get; set; }
// }
// o: Dictionary<string, object> ConfigurationItems
// Hay que ver si esto se puede serializar bien al pasarlo por la API como respuesta a un request...
// No lo veo del todo bueno porque al no estar tipado, puede traer muchos problemas.