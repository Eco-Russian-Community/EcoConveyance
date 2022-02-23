using System;
using System.Collections.Generic;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;

namespace Eco.Mods.EcoConveyance
{
	public class EcoConveyance : IModKitPlugin, IServerPlugin, IInitializablePlugin
	{
		public const string Version = "1.0.1-alpha";

		public string GetStatus()
		{
			return $"{this} v{Version}";
		}

		public void Initialize(TimedTask timer)
		{

		}

		public override string ToString()
		{
			return "Eco.Mods.EcoConveyance";
		}
	}
}
