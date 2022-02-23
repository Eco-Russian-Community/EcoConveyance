using Eco.Shared.Localization;
using Eco.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Mods.EcoConveyance.Utils
{
	internal static class DebuggingUtils
	{
		[Conditional("DEBUG")] public static void LogInfoLine(string msg) => Log.WriteLine(Localizer.DoStr(msg));
		[Conditional("DEBUG")] public static void LogWarningLine(string msg) => Log.WriteWarningLineLocStr(msg);
		[Conditional("DEBUG")] public static void LogErrorLine(string msg) => Log.WriteErrorLineLocStr(msg);
		[Conditional("DEBUG")] public static void LogException(Exception ex) => Log.WriteException(ex);
		[Conditional("DEBUG")] public static void Debug(string msg) => Log.Debug(msg);
	}
}
