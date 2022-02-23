﻿using Eco.Core.Utils;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Mods.EcoConveyance.Components;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Mods.EcoConveyance.Objects
{
	[Serialized]
	[RequireComponent(typeof(ConveyorExporterComponent))]
	[RequireComponent(typeof(SolidGroundComponent))]
	internal class ConveyorExporterObject : BaseConveyorObject, IRepresentsItem
	{
		public override LocString DisplayName => Localizer.DoStr("Conveyor Exporter");
		public virtual Type RepresentedItemType => typeof(ConveyorExporterItem);

		protected override void OnCreate()
		{
			base.OnCreate();
			this.GetComponent<ConveyorExporterComponent>().OutputDirection = new Direction[] { DirectionExtensions.FacingDir(this.Rotation.RotateVector(Vector3.Back)) };
		}

		protected override void Initialize()
		{
			base.Initialize();
			this.GetComponent<LinkComponent>().Initialize(1);
		}
	}
}
