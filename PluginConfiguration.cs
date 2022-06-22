using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullGodMode
{
    public class PluginConfiguration : IRocketPluginConfiguration
    {
        public string MessageColor { get; set; }
        public string LoadMessage { get; set; }
        public void LoadDefaults()
        {
            MessageColor = "green";
            LoadMessage = "FullGM Mod is active!\nGet more plugins on https://unturned.londev.ru/";
        }
    }
}
