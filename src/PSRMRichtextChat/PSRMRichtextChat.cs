using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Rocket.API;
using Rocket.API.Serialisation;
using Rocket.Core;
using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace PSRMRichtextChat
{
    public class PSRMRichtextChat : RocketPlugin<PSRMRichtextChatConfiguration>
    {
        public PSRMRichtextChat instance;
        public List<RichTextOptions> Options;
        
        protected override void Load()
        {
            instance = this;

            Logger.LogWarning(
                $"{Name} {Assembly.GetName().Version} loaded! Made by papershredder432, join the support Discord here: https://discord.gg/ydjYVJ2");

            ChatManager.onChatted += OnChatted;

            Options = instance.Configuration.Instance.Options;

            foreach (RichTextOptions richTextOptions in Options)
            {
                RocketPermissionsGroup g = R.Permissions.GetGroup(richTextOptions.PermissionGroup);
                if (g == null)
                {
                    Logger.LogWarning($"Permission group {richTextOptions.PermissionGroup} does not exist! All rich text from that group will not work.");
                    Options.Remove(richTextOptions);
                }
            }
        }

        protected override void Unload()
        {
            instance = null;

            Logger.LogWarning($"{Name} {Assembly.GetName().Version} unloaded.");
            
            ChatManager.onChatted -= OnChatted;
        }

        public void OnChatted(SteamPlayer player, EChatMode mode, ref Color chatted, ref bool rich, string text, ref bool visible)
        {
            bool isFound = false;
            RichTextOptions foundGroup = null;
            
            visible = false;
            var uPlayer = UnturnedPlayer.FromCSteamID(player.playerID.steamID);

            if (text.StartsWith("/")) return;

            foreach (RichTextOptions richTextOptions in Options)
            {
                RocketPermissionsGroup g = R.Permissions.GetGroup(richTextOptions.PermissionGroup);
                if (g != null && g.Members.Contains(player.playerID.steamID.ToString()))
                {
                    isFound = true;
                    foundGroup = richTextOptions;
                    break;
                }
            }

            if (isFound)
            {
                ChatManager.serverSendMessage(
                    foundGroup.RichText.Replace("%playerMsg%", text)
                        .Replace("%playerName%", player.playerID.characterName), Color.white, null, null,
                    EChatMode.SAY, uPlayer.SteamProfile.AvatarIcon.ToString(), true);
            }
            else
            {
                ChatManager.serverSendMessage(
                    instance.Configuration.Instance.DefaultRichText.Replace("%playerMsg%", text)
                        .Replace("%playerName%", player.playerID.characterName), Color.white, null, null,
                    EChatMode.SAY, uPlayer.SteamProfile.AvatarIcon.ToString(), true);
            }
        }
    }
}