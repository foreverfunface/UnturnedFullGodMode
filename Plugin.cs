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
            DamageTool.damagePlayerRequested += DamageTool_damagePlayerRequested;
        }

        private void DamageTool_damagePlayerRequested(ref DamagePlayerParameters parameters, ref bool shouldAllow)
        {
            UnturnedPlayer player = UnturnedPlayer.FromPlayer(parameters.player);

            if (player.GodMode || player.VanishMode)
            {
                parameters.virusModifier = 0;
                parameters.waterModifier = 0;
                parameters.ragdollEffect = ERagdollEffect.NONE;
                parameters.damage = 0;
                parameters.bleedingModifier = DamagePlayerParameters.Bleeding.Never;
                parameters.bonesModifier = DamagePlayerParameters.Bones.None;
                parameters.cause = EDeathCause.FOOD;
                parameters.limb = ELimb.SPINE;
            }
        }

        protected override void Unload()
        {
            DamageTool.damagePlayerRequested -= DamageTool_damagePlayerRequested;
        }
    }
}
