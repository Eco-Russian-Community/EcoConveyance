using Eco.Gameplay.Objects;
using Eco.Shared;
using Eco.Shared.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Mods.EcoConveyance.Utils
{
	internal static class Vector3iUtils
	{
		public static Vector3i Clamp(this Vector3i vector, Vector3i min, Vector3i max)
		{
			int x = Mathf.Clamp(vector.x, min.x, max.x);
			int y = Mathf.Clamp(vector.y, min.y, max.y);
			int z = Mathf.Clamp(vector.z, min.z, max.z);
			return new Vector3i(x, y, z);
		}

		public static List<Vector3i> OccupancyWorldPosition(WorldObject worldObject) => OccupancyHelper.ToWorldPositions(worldObject.Position3i, worldObject.Occupancy, worldObject.Rotation);
	}
}
