using System;
using Microsoft.Xna.Framework;

namespace Defender
{

/*
* CREDIT: The following code is created by Darren Sweeney.
* Twitter: @NerdyNinja95
*/

	public static class UniversalGravitation
	{
		/* Newton's law of universal gravitation states that every point mass in
		// the universe attracts every other point mass with a force that is directly
		// proportional to the product of their masses and inversely proportional
		   to the square of the distance between them. */

		//          E.g Force = GravitationalConstant * MoonMass * PlanetMass / distance^2
		//                          or
		//
		//                  Force = GravitationalConstant X MoonMass X PlanetMass
		//                  _____________________________________________________
		//                                   distance X distance


		public static float force; //The force changes as the moon comes closer to the planet , so therfore it accelerates.
		public const float gravitationalConstant = 0.5f; //In our universe its 6.67300 × 10-11 m3 kg-1 s-2 
		public static float distance; //The distance is meassured from the center of each sprite
		public static Vector2 direction; //Gravity is always an attracitve force.
	}

}
