using System;
using System.Collections.Generic;
using Nez;
using Microsoft.Xna.Framework.Graphics;

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
				var systemPlanets = new List<planetType>(3);
				systemPlanets.Add(planetType.earth);
				systemPlanets.Add(planetType.volcanic);
				systemPlanets.Add(planetType.earth);

				systems.Add(new solarSystem(3, systemPlanets));
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
