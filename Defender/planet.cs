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
		private static Texture2D planetSprite;
		private float scale;

		public planet(planetType planetNum, float Scale, float pX, float pY)
		{
			type = planetNum;
			scale = Scale;
			X = pX;
			Y = pY;

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
