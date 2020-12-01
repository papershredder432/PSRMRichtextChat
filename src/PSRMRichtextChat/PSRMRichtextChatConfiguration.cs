using System.Collections.Generic;
using System.Xml.Serialization;
using Rocket.API;

namespace PSRMRichtextChat
{
    public class PSRMRichtextChatConfiguration : IRocketPluginConfiguration
    {
        public List<RichTextOptions> Options;
        public string DefaultRichText;
        
        public void LoadDefaults()
        {
            Options = new List<RichTextOptions>()
            {
                new RichTextOptions { PermissionGroup = "VIP", RichText = "<color=#ff00ff>%playerName%</color>: <color=#ffff00>%playerMsg%</color>"}
            };
            DefaultRichText = "<color=#ffffff>%playerName%</color>: <color=#ffffff>%playerMsg%</color>";
        }
    }

    public class RichTextOptions
    {
        [XmlAttribute] public string PermissionGroup;
        [XmlAttribute] public string RichText;
    }
}