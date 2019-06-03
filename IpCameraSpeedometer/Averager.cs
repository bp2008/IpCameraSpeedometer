using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpCameraSpeedometer
{
	public class Averager
	{
		private Queue<Sample> queue = new Queue<Sample>();
		private int overTimeMs;
		private decimal sum = 0;
		public Averager(int overTimeMs = 1000)
		{
			this.overTimeMs = overTimeMs;
		}
		public decimal AddSample(decimal sample)
		{
			queue.Enqueue(new Sample() { Value = sample });
			sum += sample;
			return GetAverage();
		}

		public decimal GetAverage()
		{
			while (queue.Count > 0 && queue.Peek().Expired)
				sum -= queue.Dequeue().Value;
			if (queue.Count > 0)
				return sum / queue.Count;
			return 0;
		}

		class Sample
		{
			public DateTime Timestamp = DateTime.Now;
			public bool Expired
			{
				get
				{
					return DateTime.Now - Timestamp >= TimeSpan.FromMilliseconds(1000);
				}
			}
			public decimal Value;
		}
	}
}
