using Eco.Gameplay.Objects;
using System;

namespace Eco.Mods.EcoConveyance.Animation
{
	public class AnimationController
	{
		private WorldObject worldObject;

		public AnimationController(WorldObject worldObject)
		{
			this.worldObject = worldObject ?? throw new ArgumentNullException(nameof(worldObject));
		}

		public bool PlayAnimation(Animation animation)
		{
			return animation.Play(this.worldObject);
		}

		public bool PlayAnimation(string name, int duration)
		{
			Animation animation = new Animation(name, duration);
			return this.PlayAnimation(animation);
		}
		public bool PlayAnimation(string name, int duration, Animation.AnimationDoneAction action)
		{
			Animation animation = new Animation(name, duration, action);
			return this.PlayAnimation(animation);
		}

		public bool PlayAnimation(Animation animation, Animation.AnimationDoneAction action)
		{
			animation.SetAction(action);
			return this.PlayAnimation(animation);
		}

		public bool PlayAnimationSynced(Animation animation)
		{
			this.worldObject.SyncPositionAndRotation();
			return this.PlayAnimation(animation);
		}

		public bool PlayAnimationSynced(string name, int duration)
		{
			Animation animation = new Animation(name, duration);
			return this.PlayAnimationSynced(animation);
		}

		public bool PlayAnimationSynced(string name, int duration, Animation.AnimationDoneAction action)
		{
			Animation animation = new Animation(name, duration, action);
			return this.PlayAnimationSynced(animation);
		}

		public bool PlayAnimationSynced(Animation animation, Animation.AnimationDoneAction action)
		{
			animation.SetAction(action);
			return this.PlayAnimationSynced(animation);
		}
	}
}
