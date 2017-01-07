using System;
using System.Collections.Generic;
using Nez;
using Nez.Sprites;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio; // using statements

namespace Defender
{
	/// <summary>
	/// CREATED BY EDWARD URBAN IN OCTOBER 2016.
	/// </summary>
	public class Game1 : Core
	{

		public Game1() : base(1920, 1080, false, true, "Blitzkrieg", "Content") { }


		List<Entity> Backgrounds;

		protected override void Initialize()
		{
			Window.AllowUserResizing = true;

			base.Initialize();

			var spaceScene = Scene.createWithDefaultRenderer( Color.Black );


			// entities
			var myPlayer = spaceScene.createEntity("myPlayer");
			var backgroundTextures = spaceScene.createEntity("backgroundTextures");

			//Backgrounds.Add(spaceScene.createEntity("spaceNebulas"));
			                //Backgrounds.Add(spaceScene.createEntity("spaceStars1"));
			                //Backgrounds.Add(spaceScene.createEntity("spaceStars2"));
			                                


			// textures

			var _textureShip = spaceScene.content.Load<Texture2D>("stallion");
			var _textureNebulas = spaceScene.content.Load<Texture2D>("nebulas");
            var _textureStars60 = spaceScene.content.Load<Texture2D>("Parallax60");
			var _textureStars100 = spaceScene.content.Load<Texture2D>("Parallax100");

			// components

			myPlayer.addComponent(new Sprite(_textureShip));
			myPlayer.addComponent(new Ship(25, 5000, 0.25f, 0.25f, 5, 74, 54));
			myPlayer.addComponent(new MovementInput());

			spaceScene.camera.entity.addComponent(new FollowCamera(myPlayer));

			backgroundTextures.addComponent(new SpaceBackgroundSprite(_textureStars100, new Vector2(5, 5), 2.5f));
			backgroundTextures.addComponent(new SpaceBackgroundSprite(_textureNebulas, new Vector2(1, 1), 5f));
			backgroundTextures.addComponent(new SpaceBackgroundSprite(_textureStars60, new Vector2(15, 15), 3f));

			// transform

			myPlayer.transform.position = new Vector2(100, 400);
			myPlayer.transform.scale = new Vector2(0.15f);
			//spaceScene.camera.transform.position = myPlayer.transform.position;


			Core.scene = spaceScene;

			//introGalaxy = new Galaxy(0.99f, 1);
			//planetTextures = new List<Texture2D>(2);

			//Camera camera = new Camera(GraphicsDevice.Viewport);
		}

	}
}
