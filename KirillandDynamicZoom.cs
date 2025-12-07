using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

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
            if (ModLoader.TryGetMod("AbsoluteZinema", out Mod val))
            {
                ModConfig config = val.GetConfig("AbsoluteZinemaConfig");
                PropertyInfo property = ((object)config).GetType().GetProperty("MinZoom", BindingFlags.Instance | BindingFlags.Public);
                if (property != null && property.GetValue(config) is int num)
                {
                    minZoom = (float)num / 100f;
                }
            }
            if (ModLoader.TryGetMod("BetterZoom", out Mod betterZoom))
			{
				var config = betterZoom.GetConfig("Config");
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
