using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Defender
{
	public class Camera
	{
		public Matrix transform;
		Viewport view;
		Vector2 center;
		Vector2 topLeft;

		public Camera(Viewport newView)
		{
			view = newView;
		}

		public void Update(GameTime gameTime, Player myplayer, GraphicsDevice graphicsDevice)
		{
			center = new Vector2(myplayer.X + (myplayer.ship.width / 2) - (graphicsDevice.Viewport.Width / 2), 
			                     myplayer.Y + (myplayer.ship.height / 2) - (graphicsDevice.Viewport.Height / 2));
			
			topLeft = new Vector2(myplayer.X + (myplayer.ship.width / 2) - graphicsDevice.Viewport.Width,
			                      myplayer.Y + (myplayer.ship.height / 2) - graphicsDevice.Viewport.Height);
			
			transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0));
		}

	}
}
