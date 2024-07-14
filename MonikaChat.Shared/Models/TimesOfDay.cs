namespace MonikaChat.Shared.Models
{
	public static class TimesOfDay
	{
		public static TimeOnly Morning = new TimeOnly(6, 0);
		public static TimeOnly Day = new TimeOnly(10, 0);
		public static TimeOnly Evening = new TimeOnly(18, 0);
		public static TimeOnly Night = new TimeOnly(22, 0);

		public static TimeOfDay GetCurrentTimeOfDay(TimeOnly currentTime)
		{
			if (currentTime >= Night || currentTime < Morning)
			{
				return TimeOfDay.Night;
			}

			if (currentTime >= Morning && currentTime < Day)
			{
				return TimeOfDay.Morning;
			}

			if (currentTime >= Day && currentTime < Evening)
			{
				return TimeOfDay.Day;
			}

			if (currentTime >= Evening && currentTime < Night)
			{
				return TimeOfDay.Evening;
			}

			return TimeOfDay.Day;
		}
	}

	public enum TimeOfDay
	{
		Morning,
		Day,
		Evening,
		Night
	}
}
