using Eco.Gameplay.Objects;
using Eco.Shared.Utils;
using System;
using System.Threading;

namespace Eco.Mods.EcoConveyance.Animation
{
	public class Animation : IDisposable
	{
		private static readonly int delay = 15;

		public string Name { get; private set; }
		public int Duration { get; private set; } = 0;

		public delegate void AnimationDoneAction();
		private AnimationDoneAction _action;

		private readonly Timer _timer;

		public Animation(string name)
		{
			this._timer = new Timer(new TimerCallback(this.Done));
			this.Name = name ?? throw new ArgumentNullException(nameof(name));
		}

		public Animation(string name, int duration)
		{
			this._timer = new Timer(new TimerCallback(this.Done));
			this.Name = name ?? throw new ArgumentNullException(nameof(name));
			this.Duration = duration;
		}

		public Animation(string name, int duration, AnimationDoneAction action)
		{
			this._timer = new Timer(new TimerCallback(this.Done));
			this.Name = name ?? throw new ArgumentNullException(nameof(name));
			this.Duration = duration;
			this._action = action;
		}

		public void SetDuration(int duration)
		{
			this.Duration = duration;
		}

		public void SetAction(AnimationDoneAction action)
		{
			this._action = action;
		}

		public bool Play(WorldObject worldObject)
		{
			if (this._disposed) { throw new AccessViolationException("Trying to play disposed animation"); }
			if (this.Duration == 0) { throw new ArgumentNullException(nameof(this.Duration)); }
			worldObject.TriggerAnimatedEvent(this.Name);
			return this._timer.Change(this.Duration + delay, Timeout.Infinite);
		}

		private void Done(object data)
		{
			try
			{
				this._action?.Invoke();
			}
			catch (Exception ex) { Log.WriteException(ex); }
		}

		private bool _disposed = false;
		public void Dispose()
		{
			if(this._timer != null) { this._timer.Dispose(); }
			_disposed = true;
		}
	}
}
