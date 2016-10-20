using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Defender
{
	public class Player
	{
		static Texture2D playerSprite;
		Animation currentAnimation;
		Animation stopLeft, stopRight, flyLeft, flyRight;

		public Player(Texture2D texture)
		{
			if (playerSprite == null)
			{
				playerSprite = texture;
			}

			stopLeft = new Animation();
			stopLeft.AddFrame(new Rectangle(160, 145, 80, 45), TimeSpan.FromSeconds(.25));

			stopRight = new Animation();
			stopRight.AddFrame(new Rectangle(0, 145, 80, 45), TimeSpan.FromSeconds(.25));

			flyLeft = new Animation();
			flyLeft.AddFrame(new Rectangle(160, 145, 80, 45), TimeSpan.FromSeconds(.25));


			flyRight = new Animation();
			flyRight.AddFrame(new Rectangle(0, 145, 80, 45), TimeSpan.FromSeconds(.25));

			currentAnimation = flyLeft;
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

		public float lastX
		{
			get;
			set;
		}

		public float lastY
		{
			get;
			set;
		}

		public void Update(GameTime gameTime)
		{
			// temporary - we'll replace this with logic based off of which way the
			// character is moving when we add movement logic
			if (this.X == this.lastX && currentAnimation == flyLeft)
			{
				currentAnimation = stopLeft;
			}

			if (this.X == this.lastX && currentAnimation == flyRight)
			{
				currentAnimation = stopRight;
			}

			if (this.X > this.lastX)
			{
				currentAnimation = flyRight;
			}
			else if (this.X < this.lastX)
			{
				currentAnimation = flyLeft;
			}

			this.lastX = this.X;
			this.lastY = this.Y;

			currentAnimation.Update(gameTime);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			Vector2 topLeftOfSprite = new Vector2(this.X, this.Y);
			Color tintColor = Color.White;
			// determine the current animation rectangle
			var srcRect = currentAnimation.CurrentRectangle;
			spriteBatch.Draw(playerSprite, topLeftOfSprite, srcRect, tintColor);
		}
	}
}
