using System.Collections.Generic;

namespace HydraMenu.assets
{
	internal class MapAssets
	{
		public static Dictionary<string, TaskTypes> skeldAnimations = new Dictionary<string, TaskTypes>()
		{
			{ "Clear Asteroids", TaskTypes.ClearAsteroids },
			{ "Empty Garbage", TaskTypes.EmptyGarbage },
			{ "Prime Shields", TaskTypes.PrimeShields }
		};

		public static Dictionary<string, TaskTypes> polusAnimations = new Dictionary<string, TaskTypes>()
		{
			{ "Clear Asteroids", TaskTypes.ClearAsteroids }
		};

		public static Dictionary<string, TaskTypes> GetAnimations()
		{
			MapNames currentMap = Utilities.GetCurrentMap();

			switch(currentMap)
			{
				case MapNames.Skeld:
				case MapNames.Dleks:
					return skeldAnimations;

				case MapNames.Polus:
					return polusAnimations;

				// These maps do not have any task animations, other than medbay scan
				case MapNames.MiraHQ:
				case MapNames.Airship:
				case MapNames.Fungle:
					return [];

				// If we do not any known animations for the current map then just default to the Skeld ones:
				default:
					return skeldAnimations;
			}
		}
	}
}