using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KirillandDynamicZoom
{
	public class ZoomPlayer : ModPlayer
	{
		float zoom = 0;
		public override void PreUpdate()
		{
			if ((Main.netMode == NetmodeID.SinglePlayer) || (Main.myPlayer == Player.whoAmI))
			{
				var bossIsActive = false;
				foreach (NPC activeNPC in Main.ActiveNPCs)
				{
					if (activeNPC.boss) { bossIsActive = true; break; }
				}
				if ((!bossIsActive) && (Player.townNPCs > 2f) &&
				!Main.bloodMoon && !Main.eclipse && !Main.slimeRain && !Main.snowMoon && !Main.pumpkinMoon &&
				Main.invasionType == 0)
				{
					if (zoom >= 100 * ModContent.GetInstance<DynamicZoomConfig>().maxZoomMult) zoom = 100 * ModContent.GetInstance<DynamicZoomConfig>().maxZoomMult;
					else
					{
						zoom += ModContent.GetInstance<DynamicZoomConfig>().zoomInMult;
						Main.GameZoomTarget = Utils.Clamp<float>(Main.GameZoomTarget + 0.003f * ModContent.GetInstance<DynamicZoomConfig>().zoomInMult, KirillandDynamicZoom.minZoom, KirillandDynamicZoom.maxZoom);
					}
				}
				else
				{
					if (zoom <= 0) zoom = 0;
					else
					{
						zoom -= ModContent.GetInstance<DynamicZoomConfig>().zoomOutMult;
						Main.GameZoomTarget = Utils.Clamp<float>(Main.GameZoomTarget - 0.003f * ModContent.GetInstance<DynamicZoomConfig>().zoomOutMult, KirillandDynamicZoom.minZoom, KirillandDynamicZoom.maxZoom);
					}
				}
			}
		}
	}

}
