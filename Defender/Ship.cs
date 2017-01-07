﻿using System;
using Nez;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Defender
{
	public class Ship : Component, IUpdatable
	{
		private float health;
		private float speed;
		private float xSpeed, ySpeed;

		public float rotationAngle { get; set; }


		public Ship(float maxS, float maxH, float Accel, float Decel, float turnS, float w, float h)
		{
			maxSpeed = maxS;
			acceleration = Accel;
			deceleration = Decel;
			maxHealth = maxH;
			health = maxH;
			turnSpeed = turnS;
			width = w;
			height = h;
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
		}

	}
}