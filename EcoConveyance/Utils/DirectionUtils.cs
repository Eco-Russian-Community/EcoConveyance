using Eco.Shared.Math;
using System.Runtime.CompilerServices;

namespace Eco.Mods.EcoConveyance.Utils
{
	internal static class DirectionUtils
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Direction GetDeltaDirection(Vector3i toPos, Vector3i fromPos) => WorldPosition3i.GetDelta(toPos, fromPos).Clamp(Vector3i.NegOne, Vector3i.One).ToDir();
	}
}
