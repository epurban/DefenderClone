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

		const int GUI_RENDER_LAYER = -1;
		const int BACKGROUND_RENDER_LAYER = 1;
		const int MAIN_RENDER_LAYER = 0;


		protected override void Initialize()
		{
			Window.AllowUserResizing = true;

			base.Initialize();

			var spaceScene = new Scene();

			// renderers for background, main scene, and UI
			spaceScene.addRenderer(new RenderLayerRenderer(-1, MAIN_RENDER_LAYER));
			spaceScene.addRenderer(new ScreenSpaceRenderer(-10, BACKGROUND_RENDER_LAYER));


			// entities
			var myPlayer = spaceScene.createEntity("myPlayer");
			var backgroundTextures = spaceScene.createEntity("backgroundTextures");
			var currentSystem = spaceScene.createEntity("currentSystem");

			// a list of all planet entities in the current system
			var currentSystemPlanets = new List<Entity>();


			// textures
			var _textureShip = spaceScene.content.Load<Texture2D>("stallion");
			var _textureNebulas = spaceScene.content.Load<Texture2D>("nebulas");
            var _textureStars60 = spaceScene.content.Load<Texture2D>("Parallax60");
			var _textureStars100 = spaceScene.content.Load<Texture2D>("Parallax100");
			var _textureEarthlike = spaceScene.content.Load<Texture2D>("earthlike");
			var _textureVolcanic = spaceScene.content.Load<Texture2D>("volcanic");


			// COMPONENTS

			var introGalaxy = new Galaxy(0.99f, 1);

			var sprite = new Sprite(_textureShip);
			sprite.renderLayer = MAIN_RENDER_LAYER;

			myPlayer.addComponent( sprite );
			myPlayer.addComponent(new Ship(25, 5000, 0.25f, 0.25f, 5, 74, 54, introGalaxy.decayVal()));
			myPlayer.addComponent(new MovementInput());
			myPlayer.addComponent<CircleCollider>();

			// adds an entity to a list for every planet in the system we are in
			for (int i = 0; i < introGalaxy.getSystem(myPlayer.getComponent<Ship>().curSystemIndex).numberOfPlanets(); i++)
			{
				float x = introGalaxy.getSystem(myPlayer.getComponent<Ship>().curSystemIndex).getPlanet(i).X;
				float y = introGalaxy.getSystem(myPlayer.getComponent<Ship>().curSystemIndex).getPlanet(i).Y;

				var tempPlanet = spaceScene.createEntity("planet" + i, new Vector2(x, y));
				                                                     
				tempPlanet.addComponent(new Sprite(_textureEarthlike));

				currentSystemPlanets.Add(tempPlanet);

			}


			spaceScene.camera.entity.addComponent(new FollowCamera(myPlayer));


			var backgroundStars1 = new SpaceBackgroundSprite(_textureStars100, new Vector2(5, 5), 2.5f);
			var backgroundStars2 = new SpaceBackgroundSprite(_textureStars60, new Vector2(15, 15), 3f);
			var backgroundNebulas = new SpaceBackgroundSprite(_textureNebulas, new Vector2(1, 1), 5f);

			backgroundStars1.renderLayer = BACKGROUND_RENDER_LAYER;
			backgroundStars2.renderLayer = BACKGROUND_RENDER_LAYER;
			backgroundNebulas.renderLayer = BACKGROUND_RENDER_LAYER;

			backgroundTextures.addComponent( backgroundStars1 );
			backgroundTextures.addComponent( backgroundNebulas );
			backgroundTextures.addComponent( backgroundStars2 );


			// transform
			myPlayer.transform.position = new Vector2(0, 0);
			myPlayer.transform.scale = new Vector2(0.15f);

			Core.scene = spaceScene;

			//planetTextures = new List<Texture2D>(2);

			//Camera camera = new Camera(GraphicsDevice.Viewport);
		}

	}
}
