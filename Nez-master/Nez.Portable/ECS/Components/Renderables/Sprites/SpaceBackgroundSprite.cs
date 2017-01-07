using Nez.Textures;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Defender;

namespace Nez.Sprites
{
	public class SpaceBackgroundSprite : TiledSprite, IUpdatable
	{
		private Vector2 Offset;
		public Vector2 Speed;           //Speed of movement of our parallax effect
		public float Zoom;              //Zoom level of our image
		private Viewport viewport;

		public SpaceBackgroundSprite(Subtexture subtexture) : base( subtexture )
		{ }

		public SpaceBackgroundSprite(Texture2D texture, Vector2 speed, float zoom) : this( new Subtexture( texture ) )
		{ 
			material.samplerState = SamplerState.LinearWrap;
			Offset = Vector2.Zero;
			Speed = speed;
			Zoom = zoom;
			viewport = Core.graphicsDevice.Viewport;
		}

		//Calculate Rectangle dimensions, based on offset/viewport/zoom values
		private Rectangle Rectangle
		{
			get { return new Rectangle((int)(Offset.X), (int)(Offset.Y), (int)(viewport.Width / Zoom), (int)(viewport.Height / Zoom)); }
		}


		void IUpdatable.update()
		{
			viewport = Core.graphicsDevice.Viewport;
			var myPlayer = Core.scene.findEntity("myPlayer");
			Offset += myPlayer.transform.position * Speed * Time.deltaTime;

		}

		public override void render(Graphics graphics, Camera camera)
		{
			graphics.batcher.draw(subtexture, new Vector2(viewport.X, viewport.Y), Rectangle, Color.White, 0, Vector2.Zero, Zoom, SpriteEffects.None, 1);
		}

	}


}
