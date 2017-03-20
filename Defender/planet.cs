using System;
using System.Collections.Generic;
using Nez;
using Nez.Sprites;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Defender
{
	public class planet
	{
		private planetType type;
		private float scale;

		// for gravity
		public float gravityRange;
		public float mass;

		public planet(planetType planetNum, float Scale, float pX, float pY)
		{
			type = planetNum;
			X = pX;
			Y = pY;

			// gravity
			scale = Scale;
			mass = scale * 50;
			gravityRange = scale * 750;

		}

		public float X
		{
			get;
			set;
		}

		public float Y
		{
			get;
			set;
		}

		public planetType getType()
		{
			return type;
		}

		public float getScale()
		{
			return scale;
		}


	}
}
