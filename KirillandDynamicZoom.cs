using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace KirillandDynamicZoom
{
	public class KirillandDynamicZoom : Mod
	{

		public static float minZoom = 1f;
		public static float maxZoom = 2f;

		public override void PostSetupContent()
		{
			minZoom = 1f;
			maxZoom = 2f;
			if (ModLoader.TryGetMod("BetterZoom", out Mod exampleMod))
			{
				var config = exampleMod.GetConfig("Config");
				Type configType = config.GetType();
				{
					FieldInfo field = configType.GetField("minZoom", BindingFlags.Public | BindingFlags.Instance);
					if (field != null)
					{
						object value = field.GetValue(config);
						if (value is float floatVal)
						{
							minZoom = floatVal;
						}
					}
				}
				{
					FieldInfo field = configType.GetField("maxZoom", BindingFlags.Public | BindingFlags.Instance);
					if (field != null)
					{
						object value = field.GetValue(config);
						if (value is float floatVal)
						{
							maxZoom = floatVal;
						}
					}
				}
			}
			base.PostSetupContent();
		}
	}
}
