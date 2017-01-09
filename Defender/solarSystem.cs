using System;
using System.Collections.Generic;
using Nez;
using Microsoft.Xna.Framework.Graphics;

namespace Defender
{
	public class solarSystem
	{
		private List<planet> planets;
		private star Star;

		public solarSystem(int numberOfPlanets, List<planetType> planetTypes)
		{
			planets = new List<planet>(numberOfPlanets);
			System.Random rand = new System.Random();
			float randScale;
			float randX, randY;

			Star = new star(starType.red, 1, 0, 0);

			for (int i = 0; i < numberOfPlanets; i++)
			{
				randScale = (float)0.25 * rand.Next(1, 5);
				randX = rand.Next(-2000, 2000);
				randY = rand.Next(-2000, 2000);

				planets.Add(new planet(planetTypes[i], randScale, randX, randY));

			}
		}

		public planet getPlanet(int index)
		{
			return planets[index];
		}

		public star getStar()
		{
			return Star;
		}

		public int numberOfPlanets()
		{
			return planets.Count;
		}
	}
}
