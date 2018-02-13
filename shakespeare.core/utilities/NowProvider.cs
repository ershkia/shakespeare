using System;

namespace shakespeare.core.utilities {
	public class NowProvider : INowProvider {
		DateTime INowProvider.Now
		{
			get
			{
				return DateTime.UtcNow;
			}
		}
	}
}