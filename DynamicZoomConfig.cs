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

		[Label("Zoom-In speed multiplier")]
		[DefaultValue(1f)]
		[Range(0.1f, 7f)]
		public float zoomInMult;
		[Label("Zoom-Out speed multiplier")]
		[DefaultValue(1f)]
		[Range(0.1f, 7f)]
		public float zoomOutMult;
		[Label("Max zoom multiplier")]
		[DefaultValue(1f)]
		[Range(0.5f, 2f)]
		public float maxZoomMult;
        [Label("Zoom delay")]
        [DefaultValue(1f)]
        [Range(0.5f, 10f)]
		[Increment(0.5f)]
        public float zoomDelay;

    }
}
