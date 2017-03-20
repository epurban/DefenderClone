using System;
using System.Collections.Generic;
using Nez;
using Nez.Sprites;
using Nez.Particles;
using Nez.Farseer;
using FarseerPhysics.Dynamics;
using FarseerPhysics;

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

		public Game1() : base(1920, 1080, false, false, "Blitzkrieg", "Content") { }

		const int GUI_RENDER_LAYER = -1;
		const int BACKGROUND_RENDER_LAYER = 1;
		const int MAIN_RENDER_LAYER = 0;


		protected override void Initialize()
		{
			Window.AllowUserResizing = true;

            base.Initialize();

			//Physics.spatialHashCellSize = 100;

			var spaceScene = new Scene();

            var world = spaceScene.getOrCreateSceneComponent<FSWorld>();

            var introGalaxy = new Galaxy(0.99f, 1); // create a new galaxy, with 0.99 decay and 1 solar system



            // renderers for background, main scene, and UI
            spaceScene.addRenderer(new RenderLayerRenderer(0, MAIN_RENDER_LAYER));
			spaceScene.addRenderer(new ScreenSpaceRenderer(-1, BACKGROUND_RENDER_LAYER));

            // ENTITY PROCESSING SYSTEMS

            //spaceScene.addEntityProcessor(new bulletSpawnerSystem(new Matcher().all(typeof(laser))));


			/* *****************************
               ENTITIES      
            *******************************
            */
			var myPlayer = spaceScene.createEntity("myPlayer");
			var backgroundTextures = spaceScene.createEntity("backgroundTextures");

			// add the ParticleSystemSelector which handles input for the scene and a SimpleMover to move it around with the keyboard
			//var particlesEntity = spaceScene.createEntity("particles");
			//particlesEntity.addComponent(new ParticleSystemSelector());

            // Farseer Debug rendering
            var FSDebug = spaceScene.createEntity("FSDebug");
            var FSDebugView = new FSDebugView(world);
            //FSDebugView.appendFlags(FSDebugView.DebugViewFlags.ContactPoints);
            //FSDebugView.appendFlags(FSDebugView.DebugViewFlags.ContactNormals);
            //FSDebug.addComponent(FSDebugView);

            // a list of all planet entities in the current system
            var currentSystemPlanets = new List<Entity>();

            /* *****************************
               TEXTURES    
            *******************************
            */
            var _textureShip = spaceScene.content.Load<Texture2D>("stallion");
			var _textureNebulas = spaceScene.content.Load<Texture2D>("nebulas");
            var _textureStars60 = spaceScene.content.Load<Texture2D>("Parallax60");
			var _textureStars100 = spaceScene.content.Load<Texture2D>("Parallax100");
			var _textureEarthlike = spaceScene.content.Load<Texture2D>("earthlike");
			var _textureVolcanic = spaceScene.content.Load<Texture2D>("volcanic");
			var _textureOceanic = spaceScene.content.Load<Texture2D>("water");
			var _textureSunRed = spaceScene.content.Load<Texture2D>("sun_red");
            var _textureFire = spaceScene.content.Load<Texture2D>("fireparticle");


            /* *****************************
               COMPONENTS
            *******************************
            */

			var sprite = new Sprite(_textureShip);
			sprite.renderLayer = MAIN_RENDER_LAYER;

            myPlayer.addComponent<FSRigidBody>().setBodyType(BodyType.Kinematic);
            var FSmyPlayer = myPlayer.getComponent<FSRigidBody>();
            var FSmyPlayerCollider = new FSCollisionCircle(_textureShip.Width / 2);

            FSmyPlayerCollider.setRestitution(0.8f);

            myPlayer.addComponent<FSCollisionCircle>(FSmyPlayerCollider);
            myPlayer.addComponent( sprite );
			myPlayer.addComponent(new ParticleSystemSelector());
			myPlayer.addComponent(new Ship(25, 5000, 0.25f, 0.25f, 5, 74, 54, introGalaxy.decayVal(), 1, introGalaxy.getSystem(0)));
			myPlayer.addComponent(new MovementInput());


            // add an entity for the star in the center of the system
            float x = introGalaxy.getSystem(myPlayer.getComponent<Ship>().curSystemIndex).getStar().X;
            float y = introGalaxy.getSystem(myPlayer.getComponent<Ship>().curSystemIndex).getStar().Y;

            var tempStar = spaceScene.createEntity("star", new Vector2(x, y));

            Texture2D textureStar;

			// determine which star type it is
            switch (introGalaxy.getSystem(myPlayer.getComponent<Ship>().curSystemIndex).getStar().getType())
			{
				case starType.red:
					textureStar = _textureSunRed;
					break;
				case starType.blue:
					textureStar = _textureSunRed;
					break;
				case starType.white:
					textureStar = _textureSunRed;
					break;
				default:
					textureStar = _textureSunRed;
					break;
			}

			tempStar.addComponent(new Sprite(textureStar));
			var tempSprite = tempStar.getComponent<Sprite>();
			tempSprite.transform.scale = new Vector2(introGalaxy.getSystem(myPlayer.getComponent<Ship>().curSystemIndex).getStar().getScale());
            tempSprite.setLayerDepth(1f);
            
            // rigid body
            tempStar.addComponent<FSRigidBody>().setBodyType(BodyType.Static);
            var FStempStar = tempStar.getComponent<FSRigidBody>();
            FStempStar.setMass(100);

            // collision circle
            var starCollider = new FSCollisionCircle((_textureSunRed.Width / 2.4f) * introGalaxy.getSystem(myPlayer.getComponent<Ship>().curSystemIndex).getStar().getScale());
            starCollider.setRestitution(0.2f);
            starCollider.setFriction(0.2f);

            tempStar.addComponent<FSCollisionCircle>(starCollider);



            // adds an entity to a list for every planet in the system we are in
            for (int i = 0; i < introGalaxy.getSystem(myPlayer.getComponent<Ship>().curSystemIndex).numberOfPlanets(); i++)
			{
				x = introGalaxy.getSystem(myPlayer.getComponent<Ship>().curSystemIndex).getPlanet(i).X;
				y = introGalaxy.getSystem(myPlayer.getComponent<Ship>().curSystemIndex).getPlanet(i).Y;

				var tempPlanet = spaceScene.createEntity("planet" + i, new Vector2(x, y));
                Texture2D texturePlanet;

                // determine which planet type it is
                switch (introGalaxy.getSystem(myPlayer.getComponent<Ship>().curSystemIndex).getPlanet(i).getType())
				{
					case planetType.earth:
						texturePlanet = _textureEarthlike;
						break;
					case planetType.volcanic:
						texturePlanet = _textureVolcanic;
						break;
					case planetType.oceanic:
						texturePlanet = _textureOceanic;
						break;
					default:
						texturePlanet = _textureEarthlike;
						break;
				}

				tempPlanet.addComponent(new Sprite(texturePlanet));
				tempSprite = tempPlanet.getComponent<Sprite>();
				tempSprite.transform.scale = new Vector2(introGalaxy.getSystem(myPlayer.getComponent<Ship>().curSystemIndex).getPlanet(i).getScale());

                tempPlanet.addComponent<FSRigidBody>().setBodyType(BodyType.Static);
                tempPlanet.addComponent<FSCollisionCircle>(new FSCollisionCircle((texturePlanet.Width / 2) * introGalaxy.getSystem(myPlayer.getComponent<Ship>().curSystemIndex).getPlanet(i).getScale()));

                currentSystemPlanets.Add(tempPlanet);

			}


			var followCamera = new FollowCamera(myPlayer);
			followCamera.mapSize = new Vector2(5000, 5000);
			spaceScene.camera.entity.addComponent(followCamera);

			var backgroundNebulas = new SpaceBackgroundSprite(_textureNebulas, new Vector2(1, 1), 2f);
			var backgroundStars1 = new SpaceBackgroundSprite(_textureStars100, new Vector2(5, 5), 1f);
			var backgroundStars2 = new SpaceBackgroundSprite(_textureStars60, new Vector2(10, 10), 1.5f);

			backgroundStars1.renderLayer = BACKGROUND_RENDER_LAYER;
			backgroundStars2.renderLayer = BACKGROUND_RENDER_LAYER;
			backgroundNebulas.renderLayer = BACKGROUND_RENDER_LAYER;

            backgroundStars1.layerDepth = 1;
            backgroundNebulas.layerDepth = 0;

			backgroundTextures.addComponent( backgroundStars1 );
			backgroundTextures.addComponent( backgroundNebulas );
			backgroundTextures.addComponent( backgroundStars2 );


			// transform
			myPlayer.transform.position = new Vector2(500, 0);
            myPlayer.transform.scale = new Vector2(0.8f);
			//particlesEntity.setPosition(new Vector2(myPlayer.position.X, myPlayer.position.Y);

			Core.scene = spaceScene;

		}

		protected void InitializeCurrentSolarSystem()
		{

		}

	}
}
