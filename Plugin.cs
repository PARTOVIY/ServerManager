using Rocket.API.Collections;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace ServerManager
{
    public class Plugin : RocketPlugin<Config>
    {
        public static Plugin Instance;
        public static List<string> Queues = new List<string>();
        protected override void Load()
        {
            Instance = this;
            Logger.Log("Development by PARTOVIY#9987 & ALT:IT");
            if (Instance.Configuration.Instance.CancelConnectOnDamage == true)
            {
                UnturnedEvents.OnPlayerDamaged += onDamage;
            }
        }

        private void onDamage(UnturnedPlayer player, ref EDeathCause cause, ref ELimb limb, ref UnturnedPlayer killer, ref Vector3 direction, ref float damage, ref float times, ref bool canDamage)
        {
            CSteamID steamID = player.Player.channel.owner.playerID.steamID;
            if (Instance.Configuration.Instance.CancelConnectOnDamage == true && Queues.Contains(steamID.ToString()))
            {
                Queues.Remove(steamID.ToString());
                ChatManager.serverSendMessage(Instance.Translate("CancelConnectOnDamage"), Color.green, null, player.Player.channel.owner, 0, Instance.Configuration.Instance.ImagePluginURL, true);
            }
        }

        protected override void Unload()
        {
            Instance = null;
        }

        public override TranslationList DefaultTranslations
        {
            get {
                return new TranslationList()
                {
                    {"ServersList", "{0} [{1}]\nПодключиться к серверу - /server {2}"},
                    {"ServerJoin", "Подключение к серверу через {0} секунд.\nОтмена подключения: /server cancel"},
                    {"CancelConnect", "Подключение отменено."},
                    {"CancelConnectOnDamage", "Подключение отменено, вы получили урон."},
                    {"ErrorSyntax", "Используйте: /server [number]"},
                    {"ErrorServerNumber", "Номер сервера не найден."},
                };
            }
        }
    }
}
