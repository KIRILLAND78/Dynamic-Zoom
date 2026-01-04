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
        public bool plUnderwater = false;
        public int plHurt = 0;
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
            if (((!bossIsActive) && (Main.LocalPlayer.townNPCs > 2f) &&
            !Main.bloodMoon && !Main.eclipse && !Main.slimeRain && !Main.snowMoon && !Main.pumpkinMoon &&
            Main.invasionType == 0) &&
            (plHurt == 0) &&
            ((!(Main.LocalPlayer.breathMax > Main.LocalPlayer.breath)) || !ModContent.GetInstance<DynamicZoomConfig>().zoomOutSwim) &&
            ((!(ModContent.GetInstance<DynamicZoomConfig>().zoomRunFastThreshold < Main.LocalPlayer.velocity.Length()*5.1f)) || !ModContent.GetInstance<DynamicZoomConfig>().zoomOutRunFast))
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
                if (bossIsActive) delay = 0;
                delay -= 1;
                if (delay >= 0)
                {
                    base.PreUpdatePlayers();
                    return;
                }
                delay = 0;
                if (!(bossIsActive && ModContent.GetInstance<DynamicZoomConfig>().extraZoomOutWhenBoss))
                {
                    if (zoom < -0.01)
                    {
                        Main.NewText(zoom);
                        zoom += ModContent.GetInstance<DynamicZoomConfig>().zoomOutMult;
                        Main.GameZoomTarget = Utils.Clamp<float>(Main.GameZoomTarget + 0.003f * ModContent.GetInstance<DynamicZoomConfig>().zoomOutMult, KirillandDynamicZoom.minZoom, KirillandDynamicZoom.maxZoom);
                    }
                    if (zoom > 0.01)
                    {
                        zoom -= ModContent.GetInstance<DynamicZoomConfig>().zoomOutMult;
                        Main.GameZoomTarget = Utils.Clamp<float>(Main.GameZoomTarget - 0.003f * ModContent.GetInstance<DynamicZoomConfig>().zoomOutMult, KirillandDynamicZoom.minZoom, KirillandDynamicZoom.maxZoom);
                    }
                }
                else
                {
                    if (zoom <= -ModContent.GetInstance<DynamicZoomConfig>().extraZoomOutWhenBossMult*100) zoom = -ModContent.GetInstance<DynamicZoomConfig>().extraZoomOutWhenBossMult * 100;
                    else
                    {
                        zoom -= ModContent.GetInstance<DynamicZoomConfig>().zoomOutMult;
                        Main.GameZoomTarget = Utils.Clamp<float>(Main.GameZoomTarget - 0.003f * ModContent.GetInstance<DynamicZoomConfig>().zoomOutMult, KirillandDynamicZoom.minZoom, KirillandDynamicZoom.maxZoom);
                    }
                }

            }
            if (plHurt>0)
            plHurt--;
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
