using System;
using Nez;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Defender
{
	public class Player : Component
	{
		private static Texture2D playerSprite;
		private Animation currentAnimation;
		private Animation stopLeft, stopRight, flyLeft, flyRight, flying;
		public Ship ship;
		private int curSystemIndex;

		public Player(Texture2D texture, float x, float y)
		{
			if (playerSprite == null)
			{
				playerSprite = texture;
			}

			ship = new Ship(25, 5000, 0.25f, 0.25f, 5, 74, 54);

			// starting loc
			X = x;
			Y = y;
			curSystemIndex = 0;

			flying = new Animation();
			flying.AddFrame(new Rectangle(0, 0, 147, 108), TimeSpan.FromSeconds(.25));

			stopLeft = new Animation();
			stopLeft.AddFrame(new Rectangle(160, 145, 80, 45), TimeSpan.FromSeconds(.25));

			stopRight = new Animation();
			stopRight.AddFrame(new Rectangle(0, 145, 80, 45), TimeSpan.FromSeconds(.25));

			flyLeft = new Animation();
			flyLeft.AddFrame(new Rectangle(160, 145, 80, 45), TimeSpan.FromSeconds(.25));


			flyRight = new Animation();
			flyRight.AddFrame(new Rectangle(0, 145, 80, 45), TimeSpan.FromSeconds(.25));

			currentAnimation = flying;
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

		public Texture2D getSprite()
		{
			return playerSprite;
		}

		public int getCurSys()
		{
			return curSystemIndex;
		}

		public float rotationAngle { get; set; }


		// RotationAngle is set using a value in degrees (the float f).
		public void TurnLeft()
		{
			rotationAngle -= MathHelper.ToRadians(ship.turnSpeed);
		}

		// RotationAngle is set using a value in degrees (the float f).
		public void TurnRight()
		{
			rotationAngle += MathHelper.ToRadians(ship.turnSpeed);
		}


		public void MoveEntity()
		{
			double s = Math.Sqrt((ship.xSpeed * ship.xSpeed) + (ship.ySpeed * ship.ySpeed));
			ship.setSpeed((float)s);

			if (ship.getSpeed() > ship.maxSpeed)
			{
				ship.xSpeed *= ship.maxSpeed / ship.getSpeed();
				ship.ySpeed *= ship.maxSpeed / ship.getSpeed();
			}

			X += ship.xSpeed;
			Y += ship.ySpeed;

		}

		public void Thrust()
		{
			ship.xSpeed += ship.acceleration * (float)Math.Cos(rotationAngle);
			ship.ySpeed += ship.acceleration * (float)Math.Sin(rotationAngle);
		}

		public void ReverseThrust()
		{
			ship.xSpeed -= ship.deceleration * (float)Math.Cos(rotationAngle);
			ship.ySpeed -= ship.deceleration * (float)Math.Sin(rotationAngle);

		}

		public Vector2 getSpeedVector()
		{
			Vector2 returnVector;
			returnVector = new Vector2(ship.xSpeed, ship.ySpeed);

			return returnVector;
		}

		public void StopThrust()
		{
			ship.setSpeed(0);
		}

		public void ApplyDecay(float decay)
		{
			ship.xSpeed *= decay;
			ship.ySpeed *= decay;
		}

		public void Update(GameTime gameTime)
		{

			if (this.X == this.lastX && currentAnimation == flyLeft)
			{
				currentAnimation = flying;
			}

			if (this.X == this.lastX && currentAnimation == flyRight)
			{
				currentAnimation = flying;
			}

			if (this.X > this.lastX)
			{
				currentAnimation = flying;
			}
			else if (this.X < this.lastX)
			{
				currentAnimation = flying;
			}

			this.lastX = this.X;
			this.lastY = this.Y;

			currentAnimation.Update(gameTime);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			Vector2 Position = new Vector2(X, Y);
			Color tintColor = Color.White;
			// determine the current animation rectangle
			var srcRect = currentAnimation.CurrentRectangle;
			//spriteBatch.Draw(playerSprite, topLeftOfSprite, srcRect, tintColor);
			spriteBatch.Draw(playerSprite, Position, srcRect, tintColor, rotationAngle,
							 new Vector2(74, 54),new Vector2(1), SpriteEffects.None, 0f);
		}
	}
}
