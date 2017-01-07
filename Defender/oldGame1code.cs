using System;
namespace Defender
{
	public class oldGame1code
	{
		public oldGame1code()
		{
		}

		SpriteBatch spriteBatch;

		Texture2D shipSpriteSheet;
		Texture2D earthlike;
		Texture2D volcanic;

		List<Background> Backgrounds;
		List<Texture2D> planetTextures;

		Player player1;

		Galaxy introGalaxy;

		Camera camera;

		float timer100;
		float elapsedTime;

		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			shipSpriteSheet = Content.Load<Texture2D>("shipsheet");
			earthlike = Content.Load<Texture2D>("earthlike");
			volcanic = Content.Load<Texture2D>("volcanic");
			planetTextures.Add(earthlike);
			planetTextures.Add(volcanic);

			// Load font
			font = Content.Load<SpriteFont>("Default");

			//Load the background images
			Backgrounds = new List<Background>();
			Backgrounds.Add(new Background(Content.Load<Texture2D>(@"Parallax100"), new Vector2(5, 5), 2.5f));
			Backgrounds.Add(new Background(Content.Load<Texture2D>(@"nebulas"), new Vector2(1, 1), 5f));
			Backgrounds.Add(new Background(Content.Load<Texture2D>(@"Parallax60"), new Vector2(15, 15), 3f));

			//TODO: use this.Content to load your game content here 
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
#if !__IOS__ && !__TVOS__
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
#endif

			// TIMER LOGIC
			elapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;


			// KEYBOARD PRESSES
			KeyboardState state = Keyboard.GetState();

			// for backgrounds
			Vector2 direction = Vector2.Zero;

			if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D))
			{
				player1.TurnRight();
			}
			if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.A))
			{
				player1.TurnLeft();
			}
			if (state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.W))
			{
				player1.Thrust();
			}
			if (state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.S))
			{
				player1.ReverseThrust();
			}


			//Update backgrounds
			direction = player1.getSpeedVector();

			foreach (Background bg in Backgrounds)
			{
				bg.Update(gameTime, direction, GraphicsDevice.Viewport);
			}

			// 100 ms Timer
			if (elapsedTime >= timer100)
			{
				if (state.IsKeyUp(Keys.Up) && state.IsKeyUp(Keys.W) && state.IsKeyUp(Keys.Down) && state.IsKeyUp(Keys.S))
				{
					//player1.StopThrust();
					player1.ApplyDecay(introGalaxy.decayVal());
				}

				timer100 = elapsedTime + 100;
			}

			//UPDATE SPRITES & GAME
			player1.Update(gameTime);
			player1.MoveEntity();
			camera.Update(gameTime, player1, GraphicsDevice);

			// collision detection


			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			graphicsDevice.Clear(Color.Black);

			// Draw our parallax backgrounds using a Linear Wrap
			spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);
			foreach (Background bg in Backgrounds)
				bg.Draw(spriteBatch);
			spriteBatch.End();


			// Main gameplay elements
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);

			for (int i = 0; i < introGalaxy.getSystem(player1.getCurSys()).numberOfPlanets(); i++)
			{
				introGalaxy.getSystem(player1.getCurSys()).getPlanet(i).Draw(spriteBatch,
						 planetTextures[(int)introGalaxy.getSystem(player1.getCurSys()).getPlanet(i).getType()]);
			}

			player1.Draw(spriteBatch);

			spriteBatch.End();

			// UI
			spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null);

			// debug text
			spriteBatch.DrawString(font, "myX: " + player1.X + "\n myY: " + player1.Y + "\n Speed: " +
								   player1.getSpeedVector(), new Vector2(25, 25), Color.White);

			spriteBatch.End();

			base.Draw(gameTime);
		}



	}
}
