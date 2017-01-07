using System;
using System.Collections.Generic;

namespace Defender
{
	public class solarSystem
	{
		private List<planet> planets;

		public solarSystem(int numberOfPlanets, List<planetType> planetTypes)
		{
			planets = new List<planet>(numberOfPlanets);
			Random rand = new Random();
			float randScale;

			for (int i = 0; i < numberOfPlanets; i++)
			{
				randScale = (float)0.5 * rand.Next(1, 4);
				planets.Add(new planet(planetTypes[i], randScale, 500 * i, 500 * i));

			}
		}

		public planet getPlanet(int index)
		{
			return planets[index];
		}

		public int numberOfPlanets()
		{
			return planets.Count;
		}
	}
}
