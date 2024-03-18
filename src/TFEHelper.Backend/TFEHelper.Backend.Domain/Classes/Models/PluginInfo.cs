using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Domain.Classes.DTO;
using TFEHelper.Backend.Domain.Enums;
using TFEHelper.Backend.Plugins.PluginBase.Interfaces;

namespace TFEHelper.Backend.Domain.Classes.Models
{
    public class PluginInfo
    {
        public PluginType Type { get; set; }
        public string Name { get; set; }
        public Version Version { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Name} - v{Version}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is PluginInfo)
            {
                var nico = obj as PluginInfo;
                return nico.Type == this.Type && nico.Name == this.Name && nico.Version == this.Version && nico.Description == this.Description;
            }
            if (obj is PluginInfoDTO)
            {
                var nico = obj as PluginInfoDTO;
                return nico.Type == this.Type && nico.Name == this.Name && nico.Version == this.Version.ToString() && nico.Description == this.Description;
            }
            else return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
