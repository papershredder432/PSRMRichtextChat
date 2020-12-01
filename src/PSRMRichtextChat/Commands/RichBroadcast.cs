using System.Collections.Generic;
using System.Linq;
using Rocket.API;
using SDG.Unturned;
using UnityEngine;

namespace PSRMRichtextChat.Commands
{
    public class RichBroadcast : IRocketCommand
    {
        public void Execute(IRocketPlayer caller, string[] command)
        {
            ChatManager.serverSendMessage($"{string.Join(" ", command)}", Color.white, null, null, EChatMode.GLOBAL, null, true);
        }

        public AllowedCaller AllowedCaller => AllowedCaller.Both;
        public string Name => "richbroadcast";
        public string Help => "Broadcast in rich text!";
        public string Syntax => "/richbroadcast <Text>";
        public List<string> Aliases => new List<string> { "rb", "richb", "richb", "rbc" };
        public List<string> Permissions => new List<string> { "ps.richtextchat.richbroadcast" };
    }
}