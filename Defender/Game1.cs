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

			Physics.spatialHashCellSize = 100;

			var spaceScene = new Scene();

			// renderers for background, main scene, and UI
			spaceScene.addRenderer(new RenderLayerRenderer(-1, MAIN_RENDER_LAYER));
			spaceScene.addRenderer(new ScreenSpaceRenderer(-10, BACKGROUND_RENDER_LAYER));

			// SFX

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
			var _textureOceanic = spaceScene.content.Load<Texture2D>("water");
			var _textureSunRed = spaceScene.content.Load<Texture2D>("sun_red");


			// COMPONENTS

			var introGalaxy = new Galaxy(0.99f, 1);

			var sprite = new Sprite(_textureShip);
			sprite.renderLayer = MAIN_RENDER_LAYER;

			//SoundEffect collisionSound;
			//collisionSound = spaceScene.content.Load<SoundEffect>("def_star");

			myPlayer.addComponent( sprite );
			myPlayer.addComponent(new Ship(25, 5000, 0.25f, 0.25f, 5, 74, 54, introGalaxy.decayVal()));
			myPlayer.addComponent(new MovementInput());
			myPlayer.addComponent<CircleCollider>();

			// add an entity for the star in the center of the system
			float x = introGalaxy.getSystem(myPlayer.getComponent<Ship>().curSystemIndex).getStar().X;
			float y = introGalaxy.getSystem(myPlayer.getComponent<Ship>().curSystemIndex).getStar().Y;

			var tempStar = spaceScene.createEntity("star", new Vector2(x, y));
			Texture2D texture;

			// determine which star type it is
			switch (introGalaxy.getSystem(myPlayer.getComponent<Ship>().curSystemIndex).getStar().getType())
			{
				case starType.red:
					texture = _textureSunRed;
					break;
				case starType.blue:
					texture = _textureSunRed;
					break;
				case starType.white:
					texture = _textureSunRed;
					break;
				default:
					texture = _textureSunRed;
					break;
			}

			tempStar.addComponent(new Sprite(_textureSunRed));
			var tempSprite = tempStar.getComponent<Sprite>();
			tempSprite.transform.scale = new Vector2(introGalaxy.getSystem(myPlayer.getComponent<Ship>().curSystemIndex).getStar().getScale());
			var tempCollider = new CircleCollider();
			tempCollider.setRadius(400);
			tempStar.addComponent<CircleCollider>(tempCollider);


			// adds an entity to a list for every planet in the system we are in
			for (int i = 0; i < introGalaxy.getSystem(myPlayer.getComponent<Ship>().curSystemIndex).numberOfPlanets(); i++)
			{
				x = introGalaxy.getSystem(myPlayer.getComponent<Ship>().curSystemIndex).getPlanet(i).X;
				y = introGalaxy.getSystem(myPlayer.getComponent<Ship>().curSystemIndex).getPlanet(i).Y;

				var tempPlanet = spaceScene.createEntity("planet" + i, new Vector2(x, y));

				// determine which planet type it is
				switch(introGalaxy.getSystem(myPlayer.getComponent<Ship>().curSystemIndex).getPlanet(i).getType())
				{
					case planetType.earth:
						texture = _textureEarthlike;
						break;
					case planetType.volcanic:
						texture = _textureVolcanic;
						break;
					case planetType.oceanic:
						texture = _textureOceanic;
						break;
					default:
						texture = _textureEarthlike;
						break;
				}

				tempPlanet.addComponent(new Sprite(texture));
				tempSprite = tempPlanet.getComponent<Sprite>();
				tempSprite.transform.scale = new Vector2(introGalaxy.getSystem(myPlayer.getComponent<Ship>().curSystemIndex).getPlanet(i).getScale());
				tempPlanet.addComponent<CircleCollider>();

				currentSystemPlanets.Add(tempPlanet);

			}


			var followCamera = new FollowCamera(myPlayer);
			followCamera.mapSize = new Vector2(5000, 5000);
			spaceScene.camera.entity.addComponent(followCamera);

			var backgroundNebulas = new SpaceBackgroundSprite(_textureNebulas, new Vector2(1, 1), 5f);
			var backgroundStars1 = new SpaceBackgroundSprite(_textureStars100, new Vector2(5, 5), 2.5f);
			var backgroundStars2 = new SpaceBackgroundSprite(_textureStars60, new Vector2(15, 15), 3f);

			backgroundStars1.renderLayer = BACKGROUND_RENDER_LAYER;
			backgroundStars2.renderLayer = BACKGROUND_RENDER_LAYER;
			backgroundNebulas.renderLayer = BACKGROUND_RENDER_LAYER;

			backgroundTextures.addComponent( backgroundStars1 );
			backgroundTextures.addComponent( backgroundNebulas );
			backgroundTextures.addComponent( backgroundStars2 );


			// transform
			myPlayer.transform.position = new Vector2(500, 0);
			myPlayer.transform.scale = new Vector2(0.25f);

			Core.scene = spaceScene;

		}

		protected void InitializeCurrentSolarSystem()
		{

		}

	}
}
