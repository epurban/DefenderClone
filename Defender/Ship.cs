using System;
using Nez;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Nez.Farseer;
using FarseerPhysics.Dynamics;
using FarseerPhysics;
using Nez.Particles;

namespace Defender
{
	public class Ship : Component, IUpdatable
	{
		private float health;
		private float speed;
		private float xSpeed, ySpeed;
		private float decayVal;
        private float fireSpeed;

        private bool canFire;
        private laser[] bullets;

		public int curSystemIndex; // index of the players system
        private solarSystem curSystem; // reference to player's solar system obj

		public float rotationAngle { get; set; }

		// for timers
		public float elapsedTime;
		public float timer10;
		public float timer100;
        public float timerCanFire;

		// for gravity
		public float mass;


		public Ship(float maxS, float maxH, float Accel, float Decel, float turnS, float w, float h, float decay, float fs, solarSystem curSys)
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
            fireSpeed = fs;
			bullets = new laser[25];
			CurSystem = curSys;
			mass = 10;
		}

		public solarSystem CurSystem
		{
			get { return curSystem; }
			set { curSystem = value; }
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

        public void FirePrimary()
        {
            if (canFire)
            {
                bullets[1].rotationAngle = 5;
                timerCanFire = Time.deltaTime + fireSpeed; // let player fire again after the fire timer is up
                canFire = false;
      
            }
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

			// 10 ms Timer
			if (elapsedTime >= timer10)
			{

				// GRAVITY FROM PLANETS AND SUN
				for (int i = 0; i<curSystem.numberOfPlanets(); i++)
				{
					var p = curSystem.getPlanet(i);

					var pVector = new Vector2(p.X, p.Y);

					UniversalGravitation.distance = Vector2.Distance(pVector, entity.position);

					if (UniversalGravitation.distance <= p.gravityRange) // we give it a pretend gravity range based on size of planet
					{
						UniversalGravitation.force = (UniversalGravitation.gravitationalConstant* mass * p.mass) / (UniversalGravitation.distance* 10);
						UniversalGravitation.direction = entity.position - pVector;
						UniversalGravitation.direction.Normalize();
						var velocityChangeVector = UniversalGravitation.direction * UniversalGravitation.force;

						xSpeed -= velocityChangeVector.X;
						ySpeed -= velocityChangeVector.Y;

						Debug.log($"gravity applied:{velocityChangeVector.X},{velocityChangeVector.Y}");

					}

				}

				var s = curSystem.getStar();

				var sVector = new Vector2(s.X, s.Y);

				UniversalGravitation.distance = Vector2.Distance(sVector, entity.position);

				if (UniversalGravitation.distance <= s.gravityRange) // we give it a pretend gravity range based on size of planet
				{
					UniversalGravitation.force = (UniversalGravitation.gravitationalConstant* mass * s.mass) / (UniversalGravitation.distance* 10);
					UniversalGravitation.direction = entity.position - sVector;
					UniversalGravitation.direction.Normalize();
					var velocityChangeVector = UniversalGravitation.direction * UniversalGravitation.force;

					xSpeed -= velocityChangeVector.X;
					ySpeed -= velocityChangeVector.Y;

					Debug.log($"gravity from STAR applied:{velocityChangeVector.X},{velocityChangeVector.Y}");

				}

				timer10 = elapsedTime + 0.010f;
			}

            // allow the player to fire again after the fire timer is up
            if (canFire == false)
            {
                if (elapsedTime >= timerCanFire)
                {
                    canFire = true;
                }
            }

			/*
			 ****** COLLISIONS *****
			 */

            //CollisionResult collisionResult;
            FSCollisionResult collisionResult;

            var rigidBody = entity.getComponent<FSRigidBody>();

            var fixtures = rigidBody.body.fixtureList;
            var speedVector = new Vector2(speed);

			foreach (var fixture in fixtures)
			{
				if (FixtureExt.collidesWithAnyFixtures(fixture, ref speedVector, out collisionResult))
				{
					Vector2 bounceVector = collisionResult.minimumTranslationVector;

					xSpeed += bounceVector.X * 5 * fixture.restitution;
					ySpeed += bounceVector.Y * 5 * fixture.restitution;

					Debug.log("collision result: {0}", collisionResult);
				}
			}
				

            // do a check to see if entity.getComponent<Collider> (the first Collider on the Entity) collides with any other Colliders in the Scene
            // Note that if you have multiple Colliders you could fetch and loop through them instead of only checking the first one.
            //if (entity.getComponent<CircleCollider>().collidesWithAny(out collisionResult))
            //{
            // log the CollisionResult. You may want to use it to add some particle effects or anything else relevant to your game.
            //Vector2 bounceVector = collisionResult.minimumTranslationVector;

            //xSpeed -= bounceVector.X;
            //ySpeed -= bounceVector.Y;

            //Debug.log("collision result: {0}", collisionResult);
            //}
          
           
		}

	}
}
