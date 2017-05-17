using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace It_is_a_scary_world
{

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameWorld : Game
    {

        GraphicsDeviceManager graphics;

        SpriteBatch spriteBatch;

        private static GameWorld instance;

        public float deltaTime { get; private set; }

        /// <summary>
        /// Creates a list of GameObjects
        /// </summary>
        private List<GameObject> gameObjects;

        private List<GameObject> newObjects;

        public List<GameObject> objectsToRemove;

        public List<Collider> Colliders { get; private set; }

        private Random rnd = new Random();

        private bool addEnemy;

        private bool removeEnemy;

        public static GameWorld Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameWorld();
                }
                return instance;
            }
        }

        public GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 760;
            graphics.PreferredBackBufferWidth = 1400;
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
            gameObjects = new List<GameObject>();

            newObjects = new List<GameObject>();

            objectsToRemove = new List<GameObject>();

            Colliders = new List<Collider>();

            //Adds a GameObject to the game
            Director director = new Director(new PlayerBuilder());

            gameObjects.Add(director.Construct(Vector2.Zero));

            gameObjects.Add(EnemyPool.Create(new Vector2(400, 400), Content));

            //Platforms
            gameObjects.Add(ObjectPool.Create(new Vector2(0, 660), Content));
           

            
            gameObjects.Add(ObjectPool.Create(new Vector2(400, 660), Content));
            

            gameObjects.Add(ObjectPool.Create(new Vector2(800, 660), Content));

            gameObjects.Add(ObjectPool.Create(new Vector2(1200, 660), Content));



            //Weapon
            //gameObjects.Add(TestPool.Create(new Vector2(500, 400), Content));

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

            // TODO: use this.Content to load your game content here
            foreach (GameObject go in gameObjects)
            {
                go.LoadContent(Content);
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardState keyState = Keyboard.GetState();
            // TODO: Add your update logic here

            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            UpdateMouse();


            //Updates all GameObjects
            foreach (GameObject go in gameObjects)
            {
                go.Update();
            }

            

            base.Update(gameTime);
        }
        /// <summary>
        /// Remove This Later. ITs a Note
        /// </summary>

        private void SpawnEnemy()
        {

            if (Keyboard.GetState().IsKeyDown(Keys.M) && addEnemy)
            {
                newObjects.Add(EnemyPool.Create(new Vector2(rnd.Next(0, 1000/*Window.ClientBounds.Width*/), rnd.Next(0, 600/*Window.ClientBounds.Height*/)), Content));

                addEnemy = false;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.M))
            {
                addEnemy = true;
            }
            

                
            if (Keyboard.GetState().IsKeyDown(Keys.N) && removeEnemy)
            {
                
                foreach (GameObject go in gameObjects)
                {
                    if (go.Tag == "Enemy")
                    {
                        objectsToRemove.Add(go);
                        break;
                    }
                }
                removeEnemy = false;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.N))
            {
                removeEnemy = true;
            }

            UpdateLevel();
        }

        /// <summary>
        ///    Bliver kaldet fra Player Class
        /// </summary>
        public void SpawnBullet()
        {
            foreach (GameObject go in gameObjects)
            {
                if (go.Tag == "Player")
                {
                    float x = (go.GetComponent("Player") as Player).gameObject.transform.position.X + (50 - 10) / 2; //The attack's position on the X-axis, based on the Player object's position (should be in the middle)
                    float y = (go.GetComponent("Player") as Player).gameObject.transform.position.Y - 30; //The attack's position on the Y-axis, based on the Player object's position (Edit last number to place it probably based on the Player object's sprite)

                    newObjects.Add(BulletPool.Create(new Vector2(x, y), Content));
                    break;
                }
            }

        }
        public void UpdateMouse()
        {
            MouseState current_mouse = Mouse.GetState();

            int mouseX = current_mouse.X;
            int mouseY = current_mouse.Y;

        }
        public void UpdateLevel()
        {
            if (objectsToRemove.Count > 0)
            {
                foreach (GameObject go in objectsToRemove)
                {
                    gameObjects.Remove(go);
                    EnemyPool.ReleaseObject(go);
                }

                objectsToRemove.Clear();
            }
            if (newObjects.Count > 0)
            {

                gameObjects.AddRange(newObjects);
                newObjects.Clear();
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            // TODO: Add your drawing code here

            //Draws all GameObjects
            foreach (GameObject go in gameObjects)
            {
                go.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
        public GameObject FindGameObjectWithTag(string tag)
        {
            return gameObjects.Find(x => x.Tag == tag);
        }
    }
}
