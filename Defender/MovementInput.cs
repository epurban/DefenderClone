using System;
using Microsoft.Xna.Framework.Input;
using Nez;

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

            if (Input.isKeyDown(Keys.Right) || Input.isKeyDown(Keys.D))
            {
                myShip.TurnRight();
            }
            if (Input.isKeyDown(Keys.Left) || Input.isKeyDown(Keys.A))
            {
                myShip.TurnLeft();
            }
            if (Input.isKeyDown(Keys.Up) || Input.isKeyDown(Keys.W))
            {
                myShip.Thrust();
            }
            if (Input.isKeyDown(Keys.Down) || Input.isKeyDown(Keys.S))
            {
                myShip.ReverseThrust();
            }
            if (Input.isKeyDown(Keys.LeftControl))
            {
                myShip.FirePrimary();
            }
		}

	}
}
