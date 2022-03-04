using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Mods.EcoConveyance.Components;
using Eco.Mods.EcoConveyance.Utils;
using Eco.Shared.Utils;

namespace Eco.Mods.EcoConveyance
{
	public class EcoConveyance : IModKitPlugin, IInitializablePlugin, IShutdownablePlugin
	{
		public const string Version = "1.0.1-alpha";
		public static bool IsShutdown { get; private set; }

		public Task ShutdownAsync()
		{
			DebuggingUtils.LogWarningLine("EcoConveyance: Prepare to shutdown");
			IsShutdown = true;
			return Task.Delay(TimeSpan.FromSeconds(5));
		}

        public string GetStatus() => $"v{Version}";
		public override string ToString() => "EcoConveyance";

		public void Initialize(TimedTask timer)
		{
			BaseConveyorComponent.IsShutdown = false;
			//throw new NotImplementedException();
		}
	}
}
