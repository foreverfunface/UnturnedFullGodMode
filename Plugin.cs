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
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
#endregion
#region Unturned
using SDG.Unturned;
using Steamworks;
#endregion

namespace FullGodMode
{
    public class FullGodMode : RocketPlugin
    {
        public static FullGodMode Instance { get; private set; }

        protected override void Load()
        {
            Instance = this;
            DamageTool.damagePlayerRequested += DamageTool_damagePlayerRequested;
            UnturnedPlayerEvents.OnPlayerUpdateBroken += UnturnedPlayerEvents_OnPlayerUpdateBroken;
            UnturnedPlayerEvents.OnPlayerUpdateBleeding += UnturnedPlayerEvents_OnPlayerUpdateBleeding;
        }

        private void UnturnedPlayerEvents_OnPlayerUpdateBleeding(UnturnedPlayer player, bool bleeding)
        {
            if (player.GodMode || player.VanishMode) player.Bleeding = false;
        }

        private void UnturnedPlayerEvents_OnPlayerUpdateBroken(UnturnedPlayer player, bool broken)
        {
            if (player.GodMode || player.VanishMode) player.Broken = false;
        }

        private void DamageTool_damagePlayerRequested(ref DamagePlayerParameters parameters, ref bool shouldAllow)
        {
            UnturnedPlayer player = UnturnedPlayer.FromPlayer(parameters.player);

            if (player.GodMode || player.VanishMode)
            {
                shouldAllow = false;
                parameters.virusModifier = 0;
                parameters.waterModifier = 0;
                parameters.ragdollEffect = ERagdollEffect.NONE;
                parameters.damage = 0;
                parameters.bleedingModifier = DamagePlayerParameters.Bleeding.Heal;
                parameters.bonesModifier = DamagePlayerParameters.Bones.Heal;
                parameters.cause = EDeathCause.FOOD;
                parameters.limb = ELimb.SPINE;
                parameters.trackKill = false;
                parameters.times = 0;
                parameters.hallucinationModifier = 0;
            }
        }

        protected override void Unload()
        {
            DamageTool.damagePlayerRequested -= DamageTool_damagePlayerRequested;
            UnturnedPlayerEvents.OnPlayerUpdateBroken -= UnturnedPlayerEvents_OnPlayerUpdateBroken;
            UnturnedPlayerEvents.OnPlayerUpdateBleeding -= UnturnedPlayerEvents_OnPlayerUpdateBleeding;
        }
    }
}
