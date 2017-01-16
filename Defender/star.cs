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

		public star(starType planetNum, float Scale, float pX, float pY)
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
