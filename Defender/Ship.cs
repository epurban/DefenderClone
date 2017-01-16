﻿using System;
using Nez;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Defender
{
	public class Ship : Component, IUpdatable
	{
		private float health;
		private float speed;
		private float xSpeed, ySpeed;
		private float decayVal;
		public int curSystemIndex;

		public float rotationAngle { get; set; }

		// for timers
		public float elapsedTime;
		public float timer100;


		public Ship(float maxS, float maxH, float Accel, float Decel, float turnS, float w, float h, float decay)
		{
			maxSpeed = maxS;
			acceleration = Accel;
			deceleration = Decel;
			maxHealth = maxH;
			health = maxH;
			turnSpeed = turnS;
			width = w;
			height = h;
			decayVal = decay;
		}

		public float width
		{
			get;
			set;
		}

		public float height
		{
			get;
			set;
		}

		public float acceleration
		{
			get;
			set;
		}

		public float deceleration
		{
			get;
			set;
		}

		public float maxSpeed
		{
			get;
			set;
		}

		public float maxHealth
		{
			get;
			set;
		}

		public float turnSpeed
		{
			get;
			set;
		}

		public float getHealth()
		{
			return health;
		}

		public void setHealth(float h)
		{
			health = h;
		}

		public float getSpeed()
		{
			return speed;
		}

		public Vector2 getSpeedVector()
		{
			return new Vector2(xSpeed, ySpeed);
		}

		public void setSpeed(float s)
		{
			speed = s;
		}

		public void TurnLeft()
		{
			rotationAngle -= MathHelper.ToRadians(turnSpeed);
		}

		public void TurnRight()
		{
			rotationAngle += MathHelper.ToRadians(turnSpeed);
		}

		public void Thrust()
		{
			xSpeed += acceleration * (float)Math.Cos(rotationAngle);
			ySpeed += acceleration * (float)Math.Sin(rotationAngle);
		}

		public void ReverseThrust()
		{
			xSpeed -= deceleration * (float)Math.Cos(rotationAngle);
			ySpeed -= deceleration * (float)Math.Sin(rotationAngle);
		}

		public void ApplyDecay(float decay)
		{
			xSpeed *= decay;
			ySpeed *= decay;
		}

		public void MoveEntity()
		{
			var moveVector = Vector2.Zero;
			double s = Math.Sqrt((xSpeed * xSpeed) + (ySpeed * ySpeed));
			speed = (float)s;

			if (speed > maxSpeed)
			{
				xSpeed *= maxSpeed / speed;
				ySpeed *= maxSpeed / speed;
			}

			moveVector = new Vector2(xSpeed, ySpeed);

			entity.transform.position += moveVector;
			entity.transform.rotation = rotationAngle;

		}

		public void update()
		{
			// every time we update the ship, we want to move it and rotate it
			MoveEntity();

			// TIMER LOGIC
			elapsedTime += Time.deltaTime;

			// 100 ms Timer
			if (elapsedTime >= timer100)
			{
				//if (state.IsKeyUp(Keys.Up) && state.IsKeyUp(Keys.W) && state.IsKeyUp(Keys.Down) && state.IsKeyUp(Keys.S))
				//{
					ApplyDecay(decayVal);
				//}

				timer100 = elapsedTime + 0.100f;
			}

			CollisionResult collisionResult;

			// do a check to see if entity.getComponent<Collider> (the first Collider on the Entity) collides with any other Colliders in the Scene
			// Note that if you have multiple Colliders you could fetch and loop through them instead of only checking the first one.
			if (entity.getComponent<CircleCollider>().collidesWithAny(out collisionResult))
			{
				// log the CollisionResult. You may want to use it to add some particle effects or anything else relevant to your game.
				//entity.transform.position += -collisionResult.minimumTranslationVector;
				Vector2 bounceVector = collisionResult.minimumTranslationVector;

				xSpeed -= bounceVector.X;
				ySpeed -= bounceVector.Y;

				Debug.log("collision result: {0}", collisionResult);
			}

		}

	}
}
