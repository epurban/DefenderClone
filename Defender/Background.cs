using System;
using Nez;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Defender
{
	public class Background : Component, IUpdatable
	{
		private Texture2D Texture;      //The image to use
		private Vector2 Offset;         //Offset to start drawing our image
		public Vector2 Speed;           //Speed of movement of our parallax effect
		public float Zoom;              //Zoom level of our image
		private Viewport Viewport;      //Our game viewport

		//Calculate Rectangle dimensions, based on offset/viewport/zoom values
		private Rectangle Rectangle
		{
			get { return new Rectangle((int)(Offset.X), (int)(Offset.Y), (int)(Viewport.Width / Zoom), (int)(Viewport.Height / Zoom)); }
		}

		public Background(Texture2D texture, Vector2 speed, float zoom, Viewport viewport)
		{
			Texture = texture;
			Offset = Vector2.Zero;
			Speed = speed;
			Zoom = zoom;
			Viewport = viewport;
		}


		public void update()
		{

			Vector2 direction = new Vector2(5, 5);

			//Calculate the distance to move our image, based on speed
			Vector2 distance = direction * Speed * Time.deltaTime;

			//Update our offset
			Offset += distance;

			entity.position = Offset;
		}

		//public void draw(SpriteBatch spriteBatch)
		//{
		//spriteBatch.Draw(Texture, new Vector2(Viewport.x, Viewport.y), Rectangle, Color.White, 0, Vector2.Zero, Zoom, SpriteEffects.None, 1);
		//}
	}
}