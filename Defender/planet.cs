using System;
using System.Collections.Generic;

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


		public void Draw(SpriteBatch spriteBatch, Texture2D texture)
		{
			Vector2 Position = new Vector2(X, Y);
			Color tintColor = Color.White;


			// determine the current animation rectangle
			spriteBatch.Draw(texture, Position, tintColor);
							 
		}
	}
}
