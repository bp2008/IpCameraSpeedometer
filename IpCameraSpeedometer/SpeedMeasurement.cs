using System;

namespace IpCameraSpeedometer
{
	public class SpeedMeasurement
	{
		private static readonly long ticksPerHour = TimeSpan.FromHours(1).Ticks;
		/// <summary>
		/// The direction of the motion.
		/// </summary>
		public readonly MovementDirection Direction;
		/// <summary>
		/// The distance the object moved.
		/// </summary>
		public decimal DistancePx { get; protected set; }
		/// <summary>
		/// The time it took for the object to move this distnace.
		/// </summary>
		public TimeSpan Time { get; protected set; }
		/// <summary>
		/// Gets the speed of the object in pixels per hour.
		/// </summary>
		public decimal SpeedPPH
		{
			get
			{
				long ticks = Time.Ticks;
				if (ticks <= 0)
					return decimal.MaxValue;
				decimal modifier = ticksPerHour / ticks;
				return DistancePx * modifier;
			}
		}
		public SpeedMeasurement(MovementDirection direction, decimal distancePx, TimeSpan time)
		{
			Direction = direction;
			DistancePx = distancePx;
			Time = time;
		}
	}
	public enum MovementDirection
	{
		Left,
		Right
	}
}