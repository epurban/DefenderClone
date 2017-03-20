using System;
using System.Collections.Generic;
using Nez;
using Nez.Sprites;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Defender
{
	public class star
	{

		private starType type;
		private float scale;

		// for gravity
		public float gravityRange;
		public float mass;

		public star(starType planetNum, float Scale, float pX, float pY)
		{
			type = planetNum;
			X = pX;
			Y = pY;

			// gravity
			scale = Scale;
			mass = scale * 100;
			gravityRange = scale * 1000;

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

		public starType getType()
		{
			return type;
		}

		public float getScale()
		{
			return scale;
		}

	}
}
