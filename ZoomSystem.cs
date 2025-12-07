using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KirillandDynamicZoom
{
    internal class ZoomSystem : ModSystem
    {
        int delay = (int)ModContent.GetInstance<DynamicZoomConfig>().zoomDelay * 60;
        float zoom = 0;
        public override bool IsLoadingEnabled(Mod mod)
        {
            if (Main.dedServ) return false;
            return base.IsLoadingEnabled(mod);
        }
        public override void PreUpdatePlayers()
        {
            var bossIsActive = false;
            foreach (NPC activeNPC in Main.ActiveNPCs)
            {
                if (activeNPC.boss) { bossIsActive = true; break; }
            }
            if ((!bossIsActive) && (Main.player[Main.myPlayer].townNPCs > 2f) &&
            !Main.bloodMoon && !Main.eclipse && !Main.slimeRain && !Main.snowMoon && !Main.pumpkinMoon &&
            Main.invasionType == 0)
            {
                delay += 1;
                if (delay <= ModContent.GetInstance<DynamicZoomConfig>().zoomDelay*60)
                {
                    base.PreUpdatePlayers();
                    return;
                }
                delay = (int)(ModContent.GetInstance<DynamicZoomConfig>().zoomDelay * 60);
                if (zoom >= 100 * ModContent.GetInstance<DynamicZoomConfig>().maxZoomMult) zoom = 100 * ModContent.GetInstance<DynamicZoomConfig>().maxZoomMult;
                else
                {
                    zoom += ModContent.GetInstance<DynamicZoomConfig>().zoomInMult;
                    Main.GameZoomTarget = Utils.Clamp<float>(Main.GameZoomTarget + 0.003f * ModContent.GetInstance<DynamicZoomConfig>().zoomInMult, KirillandDynamicZoom.minZoom, KirillandDynamicZoom.maxZoom);
                }
            }
            else
            {
                delay -= 1;
                if (delay >= 0)
                {
                    base.PreUpdatePlayers();
                    return;
                }
                delay = 0;
                if (zoom <= 0) zoom = 0;
                else
                {
                    zoom -= ModContent.GetInstance<DynamicZoomConfig>().zoomOutMult;
                    Main.GameZoomTarget = Utils.Clamp<float>(Main.GameZoomTarget - 0.003f * ModContent.GetInstance<DynamicZoomConfig>().zoomOutMult, KirillandDynamicZoom.minZoom, KirillandDynamicZoom.maxZoom);
                }
            }
            base.PreUpdatePlayers();
        }
        public override void OnWorldUnload()
        {
            Main.GameZoomTarget = Utils.Clamp<float>(Main.GameZoomTarget - 0.003f * zoom * ModContent.GetInstance<DynamicZoomConfig>().zoomOutMult, KirillandDynamicZoom.minZoom, KirillandDynamicZoom.maxZoom);
            zoom = 0;
            base.OnWorldUnload();
        }
    }
}
