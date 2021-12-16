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
using System.Collections;
using Steamworks;

namespace ServerManager
{
    class ServerCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "server";

        public string Help => "server";

        public string Syntax => "/server [name/cancel]";

        public List<string> Aliases => new List<string> { };

        public List<string> Permissions => new List<string> { Plugin.Instance.Configuration.Instance.Permission };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;
            CSteamID steamid = player.Player.channel.owner.playerID.steamID;
            if (command.Length != 1)
            {
                ChatManager.serverSendMessage(Plugin.Instance.Translate("ErrorSyntax"), Color.green, null, player.Player.channel.owner, 0, Plugin.Instance.Configuration.Instance.ImagePluginURL, true);
                return;
            }
            else {
                if(Plugin.Queues.Contains(steamid.ToString()) && command[0].ToLower() == "cancel")
                {
                    Plugin.Queues.Remove(steamid.ToString());
                    ChatManager.serverSendMessage(Plugin.Instance.Translate("CancelConnect"), Color.green, null, player.Player.channel.owner, 0, Plugin.Instance.Configuration.Instance.ImagePluginURL, true);
                }
                Server Server = Plugin.Instance.Configuration.Instance.Servers[Convert.ToInt16(command[0])];
                if (Server == null)
                {
                    ChatManager.serverSendMessage(Plugin.Instance.Translate("ErrorServerNumber"), Color.green, null, player.Player.channel.owner, 0, Plugin.Instance.Configuration.Instance.ImagePluginURL, true);
                }
                else
                {
                    ChatManager.serverSendMessage(Plugin.Instance.Translate("ServerJoin"), Color.green, null, player.Player.channel.owner, 0, Plugin.Instance.Configuration.Instance.ImagePluginURL, true);
                    TimerConnect(player.Player, Server);
                }
            }
        }

        public IEnumerator TimerConnect(Player player, Server server)
        {
            yield return new WaitForSeconds(server.TimeConnect);

            ConnectServer(player, server);
        }
        public void ConnectServer(Player player, Server server)
        {
            if (Plugin.Queues.Contains(player.channel.owner.playerID.steamID.ToString()))
            {
                player.sendRelayToServer(Parser.getUInt32FromIP(server.IP), server.Port, "", server.InLobby);
            }
        }
    }
}
