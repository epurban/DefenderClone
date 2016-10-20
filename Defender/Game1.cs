using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Defender
{
	/// <summary>
	/// CREATED BY EDWARD URBAN IN OCTOBER 2016.
	/// THIS CLONE IS FOR DEMONSTRATION OF CODING ABILITIES ONLY.
	/// Defender was originally programmed by Eugene Jarvis in the 1980s.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		Texture2D spritesheet;
		Player player1;

		SoundEffect captureHumanoid;
		SoundEffect startGame;
		SoundEffect swarm;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = 1920;
			graphics.PreferredBackBufferHeight = 1080;
			Content.RootDirectory = "Content";

		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			player1 = new Player(Content.Load<Texture2D>("defendersprites"));
			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			spritesheet = Content.Load<Texture2D>("defendersprites");

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

			// KEYBOARD PRESSES
			KeyboardState state = Keyboard.GetState();

			if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D))
			{
				player1.X = player1.X + 4;
			}
			if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.A))
			{
				player1.X = player1.X - 4;
			}
			if (state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.W))
			{
				player1.Y = player1.Y - 4;
			}
			if (state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.S))
			{
				player1.Y = player1.Y + 4;
			}

			//UPDATE SPRITES & GAME
			player1.Update(gameTime);
			base.Update(gameTime);
		}
		
		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.Black);

			spriteBatch.Begin();
			//spriteBatch.Draw(spritesheet, Vector2.Zero);
			player1.Draw(spriteBatch);
			spriteBatch.End();

			base.Draw(gameTime);
		}
		
	}
}
