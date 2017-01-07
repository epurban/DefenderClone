using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Defender
{
	public class Parallax
	{


		Vector2 image1, image2, image3, image4;
		Texture2D spaceBack;

		public Parallax(Texture2D spaceback, float tempX, float tempY)
		{
			spaceBack = spaceback;


			image1.X = tempX;
			image1.Y = tempY;
			image2.X = tempX + spaceBack.Width;
			image2.Y = tempY;
			image3.X = tempX - spaceBack.Width;
			image3.Y = tempY;
			image4.X = image2.X + spaceBack.Width;
			image4.Y = tempY;

		}

		public void Update(GameTime gameTime, Ship ship)
		{
			image1.X -= 0.1f * ship.xSpeed;
			image1.Y -= 0.1f * ship.ySpeed;

			if (image1.X + spaceBack.Width <= 0)
			{
				image1.X = image2.X + spaceBack.Width;
			}

			if (image2.X + spaceBack.Width <= 0)
			{
				image2.X = image1.X + spaceBack.Width;
			}

			image2.X = image1.X + spaceBack.Width;
			image2.Y = image1.Y;
			image3.X = image1.X - spaceBack.Width;
			image3.Y = image1.Y;
			image4.X = image2.X + spaceBack.Width;
			image4.Y = image1.Y;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(spaceBack, image1, Color.White);
			//spriteBatch.Draw(spaceBack, image2, Color.White);
			//spriteBatch.Draw(spaceBack, image3, Color.White);
			//spriteBatch.Draw(spaceBack, image4, Color.White);
		}
	}
}
