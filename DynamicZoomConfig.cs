using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace KirillandDynamicZoom
{
	public class DynamicZoomConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue(false)]
        public bool zoomOutHurt;

        [DefaultValue(1f)]
        [Range(1f, 15f)]
        [Increment(0.5f)]
        public float zoomOutHurtTime;

        [DefaultValue(false)]
        public bool zoomOutSwim;

        [DefaultValue(false)]
        public bool zoomOutRunFast;

        [DefaultValue(26f)]
        [Range(18f, 60f)]
        [Increment(2f)]
        public float zoomRunFastThreshold;

        [DefaultValue(false)]
        public bool extraZoomOutWhenBoss;

        [DefaultValue(1f)]
        [Range(0.5f, 2f)]
        public float extraZoomOutWhenBossMult;

        [DefaultValue(1f)]
		[Range(0.1f, 7f)]
		public float zoomInMult;

		[DefaultValue(1f)]
		[Range(0.1f, 7f)]
		public float zoomOutMult;

		[DefaultValue(1f)]
		[Range(0.5f, 2f)]
		public float maxZoomMult;


        [DefaultValue(1f)]
        [Range(0.5f, 10f)]
		[Increment(0.5f)]
        public float zoomDelay;

    }
}
