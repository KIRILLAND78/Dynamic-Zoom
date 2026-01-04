using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace KirillandDynamicZoom
{
    internal class DynamicZoomPlayer : ModPlayer
    {
        public override void OnHurt(Player.HurtInfo info)
        {
            if ((Player.whoAmI == Main.myPlayer) && (ModContent.GetInstance<DynamicZoomConfig>().zoomOutHurt))
            {
                ModContent.GetInstance<ZoomSystem>().plHurt = (int)(ModContent.GetInstance<DynamicZoomConfig>().zoomOutHurtTime * 60);
            }
            base.OnHurt(info);
        }
    }
}
