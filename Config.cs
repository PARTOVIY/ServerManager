using Rocket.API;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerManager
{
    public class Config : IRocketPluginConfiguration
    {
        public string ImagePluginURL { get; set; }
        public bool CancelConnectOnDamage { get; set; }
        public string Permission { get; set; }
        public Server[] Servers { get; set; }

        public void LoadDefaults()
        {
            ImagePluginURL = "https://sun9-51.userapi.com/impg/FX9B3oPy6VVZSwwM0X51n1QKlych-Gv-Ld6_vA/L5bSgVrZmGE.jpg";
            CancelConnectOnDamage = false;
            Permission = "ServerManager.access";
            Servers = new Server[]
            {
                new Server()
                {
                    Name = Provider.serverName,
                    Map = Provider.map,
                    ImageURL = Provider.configData.Browser.Icon,
                    InLobby = true,
                    IP = SteamGameServer.GetPublicIP().ToString(),
                    Port = Provider.port
                }
            };
        }
    }
}
