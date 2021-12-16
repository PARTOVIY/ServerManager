using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerManager
{
    class ServersCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "Servers";

        public string Help => "Return servers list";

        public string Syntax => "/servers";

        public List<string> Aliases => new List<string> { };

        public List<string> Permissions => new List<string> { Plugin.Instance.Configuration.Instance.Permission };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;
            Server[] servers = Plugin.Instance.Configuration.Instance.Servers;
            foreach (Server i in Plugin.Instance.Configuration.Instance.Servers) {
                ChatManager.serverSendMessage(Plugin.Instance.Translate("ServersList", i.Name, i.Map, Array.IndexOf(servers, i)), Color.green, null, player.Player.channel.owner, 0, Plugin.Instance.Configuration.Instance.ImagePluginURL, true);
            }
        }
    }
}
