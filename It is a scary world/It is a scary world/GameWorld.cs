using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace It_is_a_scary_world
{
    public enum GameState { MainMenu, InGame}

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
        /// 
        
        public List<GameObject> gameObjects;

        private List<GameObject> newObjects;

        public List<GameObject> objectsToRemove;

        public List<Collider> Colliders { get; private set; }

        private Random rnd = new Random();

        private bool addEnemy;

        private bool removeEnemy;

        private bool firstRun;

        //MainmenuTest (den er sat 2 steder til loadcontent og draw funktionerne
        private SpriteFont mainMenuT;
        private SpriteFont mainMenuTL;
        private int mainMenuID = 1;
        private int mainMenuMinID = 1;
        private int mainMenuMaxID = 3;

        private int mainMenuTimer = 1;
        private int mainMenuTimerCheck = 0;
        private int clickDelay = 0;
        private bool options = false;
        //MainMenuTestSlut

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
            this.IsMouseVisible = true;
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


            //test map
            
            //Adds a GameObject to the game
            Director director = new Director(new PlayerBuilder());

            gameObjects.Add(director.Construct(new Vector2(500,0)));

            //gameObjects.Add(EnemyPool.Create(new Vector2(400, 400), Content));

            //Platforms
            gameObjects.Add(ObjectPool.Create(new Vector2(0, 660), Content, 400, 100));
           

            
            gameObjects.Add(ObjectPool.Create(new Vector2(400, 660), Content, 400, 100));
            

            gameObjects.Add(ObjectPool.Create(new Vector2(800, 660), Content, 400, 100));

            gameObjects.Add(ObjectPool.Create(new Vector2(800, 360), Content, 400, 100));

            //Wall 
            gameObjects.Add(WallPool.Create(new Vector2(1000, 360), Content, 100, 400));
            gameObjects.Add(WallPool.Create(new Vector2(400, 360), Content, 50,600));
            
            //Weapon
            Director weapon = new Director(new WeaponBuilder());

            //gameObjects.Add(weapon.Construct(Vector2.Zero));
            

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


            //MainMenuTest
            mainMenuT = Content.Load<SpriteFont>("MainMenu");
            mainMenuTL = Content.Load<SpriteFont>("MainMenuLarge");

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

            SpawnEnemy();
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

        public void TileSet()
        {
            //Builders
            Director director = new Director(new PlayerBuilder());


            int tileSet = rnd.Next(1, 3);
            int lastRun = tileSet;

            if (firstRun)
            {
                tileSet = 0;
                firstRun = false;
            }

            if (lastRun == tileSet && !firstRun)
            {
                tileSet++;
            }

            if (tileSet == 0)
            {

                gameObjects.Add(ObjectPool.Create(new Vector2(400, 450), Content, 1050, 50));

                gameObjects.Add(ObjectPool.Create(new Vector2(0, 200), Content, 1050, 50));

                gameObjects.Add(ObjectPool.Create(new Vector2(00, 700), Content, 1050, 50));


                gameObjects.Add(director.Construct(new Vector2(500, 0)));


                //ClientBounds
                gameObjects.Add(WallPool.Create(new Vector2(0, 0), Content, 25, 50));
                gameObjects.Add(WallPool.Create(new Vector2(0, 200), Content, 25, Window.ClientBounds.Bottom));


            }
            if (tileSet == 1)
            {
                gameObjects.Add(ObjectPool.Create(new Vector2(0, 660), Content, 400, 100));



                gameObjects.Add(ObjectPool.Create(new Vector2(400, 660), Content, 400, 100));


                gameObjects.Add(ObjectPool.Create(new Vector2(800, 660), Content, 400, 100));

                gameObjects.Add(ObjectPool.Create(new Vector2(1200, 660), Content, 400, 100));


                //Wall test

            }
            if (tileSet == 2)
            {

                gameObjects.Add(ObjectPool.Create(new Vector2(0, 660), Content, 400, 100));



                gameObjects.Add(ObjectPool.Create(new Vector2(400, 660), Content, 400, 100));


                gameObjects.Add(ObjectPool.Create(new Vector2(800, 660), Content, 400, 100));

                gameObjects.Add(ObjectPool.Create(new Vector2(1200, 660), Content, 400, 100));

            }
            if (tileSet == 3)
            {

                gameObjects.Add(ObjectPool.Create(new Vector2(0, 660), Content, 400, 100));



                gameObjects.Add(ObjectPool.Create(new Vector2(400, 660), Content, 400, 100));


                gameObjects.Add(ObjectPool.Create(new Vector2(800, 660), Content, 400, 100));

                gameObjects.Add(ObjectPool.Create(new Vector2(1200, 660), Content, 400, 100));


            }
        }

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

            //MainMenuTest
            #region MainMenu

            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Up) && mainMenuTimer == 1 && options == false)
            {
                mainMenuID -= 1;
                mainMenuTimer = 0;
            }
            if (keyState.IsKeyDown(Keys.Down) && mainMenuTimer == 1 && options == false)
            {
                mainMenuID += 1;
                mainMenuTimer = 0;
            }

            //Hvis man er i bunden af menuen og klikker ned kommer man op til toppen og omvendt
            if (mainMenuID < mainMenuMinID)
            {
                mainMenuID = mainMenuMaxID;
            }
            else if (mainMenuID > mainMenuMaxID)
            {
                mainMenuID = mainMenuMinID;
            }

            //Et delay så man kan gå rundt i menuen i et normalt tempo
            if (mainMenuTimer == 0)
            {
                mainMenuTimerCheck += 1;
                if (mainMenuTimerCheck >= 15)
                {
                    mainMenuTimer = 1;
                    mainMenuTimerCheck = 0;
                }
            }

            if (keyState.IsKeyDown(Keys.Enter) && clickDelay == 0)
            {
                if (mainMenuID == 1)
                {
                    //TileSet();
                    clickDelay = 30;
                }

                if (mainMenuID == 2 && options == false)
                {
                    options = true;
                    clickDelay = 30;
                }

                if (mainMenuID == 2 && options == true)
                {
                    options = false;
                    clickDelay = 30;
                }

                if (mainMenuID == 3)
                {
                    Environment.Exit(0);
                }
            }

            if (options == false)
            {
                //Skriver menu teksten ud til skræmen
                if (mainMenuID == 1)
                {
                    spriteBatch.DrawString(mainMenuTL, "New Game", new Vector2(10, 10), Color.Black);
                    spriteBatch.DrawString(mainMenuT, "Options", new Vector2(10, 60), Color.Black);
                    spriteBatch.DrawString(mainMenuT, "Exit to desktop", new Vector2(10, 110), Color.Black);
                }

                if (mainMenuID == 2)
                {
                    spriteBatch.DrawString(mainMenuT, "New Game", new Vector2(10, 10), Color.Black);
                    spriteBatch.DrawString(mainMenuTL, "Options", new Vector2(10, 60), Color.Black);
                    spriteBatch.DrawString(mainMenuT, "Exit to desktop", new Vector2(10, 110), Color.Black);
                }

                if (mainMenuID == 3)
                {
                    spriteBatch.DrawString(mainMenuT, "New Game", new Vector2(10, 10), Color.Black);
                    spriteBatch.DrawString(mainMenuT, "Options", new Vector2(10, 60), Color.Black);
                    spriteBatch.DrawString(mainMenuTL, "Exit to desktop", new Vector2(10, 110), Color.Black);
                }
            }
            
            #endregion

            #region ClickDelay
            //Delay før man kan klikke på enter igen
            if (clickDelay > 0)
            {
                clickDelay -= 1;
            }
            else if (clickDelay < 0)
            {
                clickDelay = 0;
            }
            #endregion
            //MainMenuTestSlut

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
