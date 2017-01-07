using System;
using System.Collections.Generic;

namespace Defender
{
	public class Galaxy
	{
		private float decay;
		private List<solarSystem> systems;


		public Galaxy(float decayVal, int numberOfSystems)
		{
			decay = decayVal;
			systems = new List<solarSystem>(numberOfSystems);

			for (int i = 0; i < numberOfSystems; i++)
			{
				List<planetType> planetTypes = new List<planetType>(3);
				planetTypes.Add(planetType.earth);
				planetTypes.Add(planetType.volcanic);
				planetTypes.Add(planetType.earth);

				systems.Add(new solarSystem(3, planetTypes));
			}
		}

		public int numberOfSystems()
		{
			return systems.Count;
		}

		public solarSystem getSystem(int index)
		{
			return systems[index];
		}

		public float decayVal()
		{
			return decay;
		}
	}
}
