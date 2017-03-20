using System;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Particles;

namespace Defender
{
	public class MovementInput : Component, IUpdatable
	{
		// stores the playing player's ship
		Ship myShip;

		public override void onAddedToEntity()
		{
			myShip = entity.getComponent<Ship>();
		}

        public void update()
        {

			var thrustParticles = entity.getComponent<ParticleEmitter>();

			thrustParticles._emitting = true;

            if (Input.isKeyDown(Keys.Right) || Input.isKeyDown(Keys.D))
            {
                myShip.TurnRight();
            }
            else if (Input.isKeyDown(Keys.Left) || Input.isKeyDown(Keys.A))
            {
                myShip.TurnLeft();
            }

			if (Input.isKeyDown(Keys.Up) || Input.isKeyDown(Keys.W))
			{
				myShip.Thrust();
			}
			else if (Input.isKeyDown(Keys.Down) || Input.isKeyDown(Keys.S))
			{
				myShip.ReverseThrust();
			}
			else
			{
				thrustParticles._emitting = false;
			}


            if (Input.isKeyDown(Keys.LeftControl))
            {
                myShip.FirePrimary();
            }
		}

	}
}
