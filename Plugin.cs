#region System
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion
#region Rocket
using Rocket.API.Collections;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
#endregion
#region Unturned
using SDG.Unturned;
using Steamworks;
#endregion

namespace FullGodMode
{
    public class FullGodMode : RocketPlugin<PluginConfiguration>
    {
        public static FullGodMode Instance { get; private set; }
        public UnityEngine.Color MessageColor { get; private set; }

        protected override void Load()
        {
            Instance = this;
            MessageColor = UnturnedChat.GetColorFromName(Configuration.Instance.MessageColor, UnityEngine.Color.black);
            DamageTool.playerDamaged += OnPlayerDamaged;
        }

        protected override void Unload()
        {
            DamageTool.playerDamaged -= OnPlayerDamaged;
        }

        void OnPlayerDamaged(Player player, ref EDeathCause cause, ref ELimb limb, ref CSteamID killer, ref UnityEngine.Vector3 direction, ref float damage, ref float times, ref bool canDamage)
        {
            UnturnedPlayer untKiller = UnturnedPlayer.FromCSteamID(killer);
            UnturnedPlayer untVictim = UnturnedPlayer.FromPlayer(player);

            // Remove any of those pesky errors
            if (untKiller?.Player != null)
            {
                if (untKiller.GodMode || untKiller.VanishMode)
                {
                    Logger.Log($"{untKiller.Player.name} *>> {untVictim.Player.name}");
                    canDamage = false;
                }
            }
            else
            {
                if (untVictim.GodMode || untVictim.VanishMode)
                {
                    Logger.Log($"{untVictim.Player.name} damaged");
                    canDamage = false;
                }
            }
        }
    }
}
