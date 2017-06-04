using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace It_is_a_scary_world
{
    public enum GameState { MainMenu, ShopMenu, InGame }

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameWorld : Game
    {
        public bool runTileset;
        private bool firstRun = true;

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

        //Mainmenu
        private SpriteFont mainMenuT;
        private SpriteFont mainMenuTL;
        private int menuID = 1;
        private int menuMinID = 1;
        private int menuMaxID = 3;

        private int menuTimer = 1;
        private int menuTimerCheck = 0;
        private int clickDelay = 0;
        private bool options = false;

        private int saves;
        //MainMenu

        //Shop
        private bool shopState;
        private bool upgradeWeapon;
        private bool upgradePlayer;
        //Shop

        //TileSet
        private int tileSet;
        private int lastRun;
        private int lastRunEnemySpawn;
        private int enemySpawnLevel;
        //TileSet

        private GameObject go;

        public GameState currentGameState = GameState.MainMenu;

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

            //Player
/*
            Director director = new Director(new PlayerBuilder());

            gameObjects.Add(director.Construct(new Vector2(- 100, 0)));

            //Shop
            Director shop = new Director(new ShopBuilder());

            gameObjects.Add(shop.Construct(new Vector2(- 200, 0)));
*/

            base.Initialize();
        }

        public void TileSet()
        {
            tileSet = rnd.Next(1, 3);
            enemySpawnLevel = rnd.Next(1, 3);



            #region ObjectRemove
            //Used to reset every block because of collision problems even after a object is removed
            foreach (GameObject go in gameObjects)
            {
                int wallNummer = 1;
                int platformNummer = 1;
                int enemyNumber = 1;

                if (go.Tag == "Enemy")
                {
                    switch (enemyNumber)
                    {
                        case 1:
                            go.transform.position = new Vector2(6000, 6000);
                            break;
                        case 2:
                            go.transform.position = new Vector2(6000, 6000);
                            break;
                        case 3:
                            go.transform.position = new Vector2(6000, 6000);
                            break;
                        case 4:
                            go.transform.position = new Vector2(6000, 6000);
                            break;
                        case 5:
                            go.transform.position = new Vector2(6000, 6000);
                            break;
                        default:
                            break;
                    }
                    enemyNumber++;
                }

                if (go.Tag == "Platform")
                {
                    switch (platformNummer)
                    {
                        case 1:
                            go.transform.position = new Vector2(4000, 4000);
                            break;
                        case 2:
                            go.transform.position = new Vector2(4000, 4000);
                            break;
                        case 3:
                            go.transform.position = new Vector2(4000, 4000);
                            break;
                        case 4:
                            go.transform.position = new Vector2(4000, 4000);
                            break;
                        case 5:
                            go.transform.position = new Vector2(4000, 4000);
                            break;
                        case 6:
                            go.transform.position = new Vector2(4000, 4000);
                            break;
                        case 7:
                            go.transform.position = new Vector2(4000, 4000);
                            break;
                        case 8:
                            go.transform.position = new Vector2(4000, 4000);
                            break;
                        case 9:
                            go.transform.position = new Vector2(4000, 4000);
                            break;
                        case 10:
                            go.transform.position = new Vector2(4000, 4000);
                            break;
                        case 11:
                            go.transform.position = new Vector2(4000, 4000);
                            break;
                        case 12:
                            go.transform.position = new Vector2(4000, 4000);
                            break;
                        case 13:
                            go.transform.position = new Vector2(4000, 4000);
                            break;
                        case 14:
                            go.transform.position = new Vector2(4000, 4000);
                            break;
                        case 15:
                            go.transform.position = new Vector2(4000, 4000);
                            break;
                        case 16:
                            go.transform.position = new Vector2(4000, 4000);
                            break;
                        case 17:
                            go.transform.position = new Vector2(4000, 4000);
                            break;
                        case 18:
                            go.transform.position = new Vector2(4000, 4000);
                            break;
                        case 19:
                            go.transform.position = new Vector2(4000, 4000);
                            break;
                        case 20:
                            go.transform.position = new Vector2(4000, 4000);
                            break;

                        default:
                            break;
                    }
                    platformNummer++;


                }
                if (go.Tag == "Wall")
                {
                    switch (wallNummer)
                    {
                        case 1:
                            go.transform.position = new Vector2(5000, 5000);
                            break;
                        case 2:
                            go.transform.position = new Vector2(5000, 5000);
                            break;
                        case 3:
                            go.transform.position = new Vector2(5000, 5000);
                            break;
                        case 4:
                            go.transform.position = new Vector2(5000, 5000);
                            break;
                        case 5:
                            go.transform.position = new Vector2(5000, 5000);
                            break;
                        case 6:
                            go.transform.position = new Vector2(5000, 5000);
                            break;
                        case 7:
                            go.transform.position = new Vector2(5000, 5000);
                            break;
                        case 8:
                            go.transform.position = new Vector2(5000, 5000);
                            break;
                        case 9:
                            go.transform.position = new Vector2(5000, 5000);
                            break;
                        case 10:
                            go.transform.position = new Vector2(5000, 5000);
                            break;
                        case 11:
                            go.transform.position = new Vector2(5000, 5000);
                            break;
                        case 12:
                            go.transform.position = new Vector2(5000, 5000);
                            break;
                        case 13:
                            go.transform.position = new Vector2(5000, 5000);
                            break;
                        case 14:
                            go.transform.position = new Vector2(5000, 5000);
                            break;
                        case 15:
                            go.transform.position = new Vector2(5000, 5000);
                            break;
                        case 16:
                            go.transform.position = new Vector2(5000, 5000);
                            break;
                        case 17:
                            go.transform.position = new Vector2(5000, 5000);
                            break;
                        case 18:
                            go.transform.position = new Vector2(5000, 5000);
                            break;
                        case 19:
                            go.transform.position = new Vector2(5000, 5000);
                            break;
                        case 20:
                            go.transform.position = new Vector2(5000, 5000);
                            break;

                        default:
                            break;

                    }
                    wallNummer++;

                }
                if (go.Tag == "Door")
                {
                    go.transform.position = new Vector2(5000, 5000);
                }

                if (go.Tag == "Player")
                {
                    go.transform.position = new Vector2(5000, 5000);

                }
                if (go.Tag == "Shop")
                {
                    go.transform.position = new Vector2(5000, 5000);
                }
            }

            gameObjects.Clear();
            #endregion


            //Player
            Director director = new Director(new PlayerBuilder());

            gameObjects.Add(director.Construct(new Vector2(-1000, 0)));

            //Shop
            Director shop = new Director(new ShopBuilder());

            gameObjects.Add(shop.Construct(new Vector2(-2000, 0)));

            //boundaries
            //Case wall 1-4 is long walls case 5 is special wall used in tileset 3 and 5+ is walls designed to platforms
            gameObjects.Add(WallPool.Create(new Vector2(2000, 2000), Content, 25, Window.ClientBounds.Bottom));
            gameObjects.Add(WallPool.Create(new Vector2(2000, 2000), Content, 25, Window.ClientBounds.Bottom));
            gameObjects.Add(WallPool.Create(new Vector2(2000, 2000), Content, 25, Window.ClientBounds.Bottom));
            gameObjects.Add(WallPool.Create(new Vector2(2000, 2000), Content, 25, Window.ClientBounds.Bottom));
            gameObjects.Add(WallPool.Create(new Vector2(2000, 2000), Content, 25, 250));

            //MapTiles
            for (int i = 0; i < 15; i++)
            {
                gameObjects.Add(ObjectPool.Create(new Vector2(2000, 2000), Content, 300, 50));
                gameObjects.Add(WallPool.Create(new Vector2(2000, 2000), Content, 25, 48));
            }
            gameObjects.Add(DoorPool.Create(new Vector2(2000, 2000), Content, 11, 30));

            //Enemies
            for (int i = 0; i < 5; i++)
            {
                gameObjects.Add(ObjectPool.Create(new Vector2(2000, 2000), Content, 75, 25));
                gameObjects.Add(WallPool.Create(new Vector2(2000, 2000), Content, 73, 23));
                gameObjects.Add(EnemyPool.Create(new Vector2(3000, 3000), Content));
            }

            //ClientBounds

            foreach (GameObject go in gameObjects)
            {
                if (go.Tag == "Player")
                {
                    (go.GetComponent("Player") as Player).gameObject.transform.position = new Vector2(500, 0);
                    break;
                }
            }

            if (lastRun == tileSet && !firstRun)
            {
                tileSet++;
                if (tileSet > 3)
                {
                    tileSet = 1;
                }
            }

            if (lastRunEnemySpawn == enemySpawnLevel && !firstRun)
            {
                enemySpawnLevel++;
                if (enemySpawnLevel > 3)
                {
                    enemySpawnLevel = 1;
                }
            }

            if (firstRun == true)
            {
                tileSet = 1;
                enemySpawnLevel = 1;
                firstRun = false;
            }
            
            #region Tileset 1
            if (tileSet == 1)
            {
                lastRun = 1;
                lastRunEnemySpawn = enemySpawnLevel;
                int wallNummer = 1;
                int platformNummer = 1;
                int enemyNumber = 1;
                foreach (GameObject go in gameObjects)
                {
                    if (go.Tag == "Enemy" && enemySpawnLevel == 1)
                    {
                        switch (enemyNumber)
                        {
                            case 1:
                                go.transform.position = new Vector2(300, 150);
                                break;
                            case 2:
                                go.transform.position = new Vector2(600, 150);
                                break;
                            case 3:
                                go.transform.position = new Vector2(900, 400);
                                break;
                            case 4:
                                go.transform.position = new Vector2(100, 675);
                                break;
                            default:
                                break;
                        }
                        enemyNumber++;                               
                    }

                    if (go.Tag == "Enemy" && enemySpawnLevel == 2)
                    {
                        switch (enemyNumber)
                        {
                            case 1:
                                go.transform.position = new Vector2(500, 150);
                                break;
                            case 2:
                                go.transform.position = new Vector2(500, 400);
                                break;
                            case 3:
                                go.transform.position = new Vector2(400, 675);
                                break;
                            case 4:
                                go.transform.position = new Vector2(600, 675);
                                break;
                            default:
                                break;
                        }
                        enemyNumber++;
                    }

                    if (go.Tag == "Enemy" && enemySpawnLevel == 3)
                    {
                        switch (enemyNumber)
                        {
                            case 1:
                                go.transform.position = new Vector2(400, 150);
                                break;
                            case 2:
                                go.transform.position = new Vector2(600, 400);
                                break;
                            case 3:
                                go.transform.position = new Vector2(800, 675);
                                break;
                            default:
                                break;
                        }
                        enemyNumber++;
                    }

                    if (go.Tag == "Platform")
                    {
                        switch (platformNummer)
                        {
                            case 1:
                                go.transform.position = new Vector2(0, 200);                                
                                break;
                            case 2:
                                go.transform.position = new Vector2(300, 200);
                                break;
                            case 3:
                                go.transform.position = new Vector2(600, 200);
                                break;
                            case 4:
                                go.transform.position = new Vector2(900, 200);
                                break;
                            case 5:
                                go.transform.position = new Vector2(1200, 450);
                                break;
                            case 6:
                                go.transform.position = new Vector2(900, 450);
                                break;
                            case 7:
                                go.transform.position = new Vector2(600, 450);
                                break;
                            case 8:
                                go.transform.position = new Vector2(300, 450);
                                break;
                            case 9:
                                go.transform.position = new Vector2(0, 725);
                                break;
                            case 10:
                                go.transform.position = new Vector2(300, 725);
                                break;
                            case 11:
                                go.transform.position = new Vector2(600, 725);
                                break;
                            case 12:
                                go.transform.position = new Vector2(900, 725);
                                break;
                            case 13:
                                go.transform.position = new Vector2(1200, 725);
                                break;


                            default:
                                break;
                        }
                        platformNummer++;


                    }
                    if (go.Tag == "Wall")
                    {

                        switch (wallNummer)
                        {
                            case 1:
                                go.transform.position = new Vector2(0, 0);
                                break;

                            case 2:
                                go.transform.position = new Vector2(1380, 0);
                                break;

                            case 6:
                                go.transform.position = new Vector2(1176, 201);
                                break;
                            case 7:
                                go.transform.position = new Vector2(299, 451);
                                break;

                            case 8:
                                go.transform.position = new Vector2(00, 726);
                                break;
                        }
                        wallNummer++;

                    }
                    if (go.Tag == "Door")
                    {
                        go.transform.position = new Vector2(100, 690);
                    }

                    if (go.Tag == "Player")
                    {
                        go.transform.position = new Vector2(50, 200);

                    }
                    if (go.Tag == "Shop")
                    {
                        go.transform.position = new Vector2(1100, 535);
                    }
                }
            }
            //Wall test

            #endregion

            #region Tileset 2
            if (tileSet == 2)
            {
                lastRun = 2;
                lastRunEnemySpawn = enemySpawnLevel;
                int wallNummer = 1;
                int platformNummer = 1;
                int enemyNumber = 1;
                foreach (GameObject go in gameObjects)
                {
                    if (go.Tag == "Enemy" && enemySpawnLevel == 1)
                    {
                        switch (enemyNumber)
                        {
                            case 1:
                                go.transform.position = new Vector2(950, 150);
                                break;
                            case 2:
                                go.transform.position = new Vector2(1100, 150);
                                break;
                            case 3:
                                go.transform.position = new Vector2(450, 400);
                                break;
                            case 4:
                                go.transform.position = new Vector2(750, 400);
                                break;
                            case 5:
                                go.transform.position = new Vector2(600, 675);
                                break;
                        }
                        enemyNumber++;
                    }

                    if (go.Tag == "Enemy" && enemySpawnLevel == 2)
                    {
                        switch (enemyNumber)
                        {
                            case 1:
                                go.transform.position = new Vector2(1050, 150);
                                break;
                            case 2:
                                go.transform.position = new Vector2(700, 400);
                                break;
                            case 3:
                                go.transform.position = new Vector2(450, 675);
                                break;
                            case 4:
                                go.transform.position = new Vector2(750, 675);
                                break;
                        }
                        enemyNumber++;
                    }

                    if (go.Tag == "Enemy" && enemySpawnLevel == 3)
                    {
                        switch (enemyNumber)
                        {
                            case 1:
                                go.transform.position = new Vector2(1200, 150);
                                break;
                            case 2:
                                go.transform.position = new Vector2(1000, 400);
                                break;
                            case 3:
                                go.transform.position = new Vector2(700, 675);
                                break;
                            case 4:
                                go.transform.position = new Vector2(1100, 675);
                                break;
                        }
                        enemyNumber++;
                    }

                    if (go.Tag == "Platform")
                    {
                        switch (platformNummer)
                        {
                            case 1:
                                go.transform.position = new Vector2(0, 200);
                                break;
                            case 2:
                                go.transform.position = new Vector2(900, 200);
                                break;
                            case 3:
                                go.transform.position = new Vector2(1200, 200);
                                break;
                            case 4:
                                go.transform.position = new Vector2(900, 450);
                                break;
                            case 5:
                                go.transform.position = new Vector2(600, 450);
                                break;
                            case 6:
                                go.transform.position = new Vector2(300, 450);
                                break;
                            case 7:
                                go.transform.position = new Vector2(0, 725);
                                break;
                            case 8:
                                go.transform.position = new Vector2(300, 725);
                                break;
                            case 9:
                                go.transform.position = new Vector2(600, 725);
                                break;
                            case 10:
                                go.transform.position = new Vector2(900, 725);
                                break;
                            case 11:
                                go.transform.position = new Vector2(1200, 725);
                                break;
                            case 16:
                                go.transform.position = new Vector2(1225, 525);
                                break;
                            case 17:
                                go.transform.position = new Vector2(1325, 600);
                                break;
                            case 18:
                                go.transform.position = new Vector2(700, 350);
                                break;
                            case 19:
                                go.transform.position = new Vector2(800, 250);
                                break;


                            default:
                                break;
                        }
                        platformNummer++;


                    }
                    if (go.Tag == "Wall")
                    {

                        switch (wallNummer)
                        {
                            
                            case 1:
                                go.transform.position = new Vector2(0, 0);
                                break;

                            case 2:
                                go.transform.position = new Vector2(1380, 0);
                                break;

                            case 3:
                                go.transform.position = new Vector2(600, -425);
                                break;

                            case 6:
                                go.transform.position = new Vector2(1176, 200);
                                break;

                            case 7:
                                go.transform.position = new Vector2(298, 450);
                                break;

                            case 8:
                                go.transform.position = new Vector2(0, 725);
                                break;
                            default:
                                break;
                        }
                        wallNummer++;

                    }
                    if (go.Tag == "Door")
                    {
                        go.transform.position = new Vector2(1300, 165);
                    }

                    if (go.Tag == "Player")
                    {
                        go.transform.position = new Vector2(50, 200);
                    }

                    if (go.Tag == "Shop")
                    {
                        go.transform.position = new Vector2(-1000, -1000);
                    }
                }
            }
            #endregion

            #region Tileset 3
            if (tileSet == 3)
            {
                lastRun = 3;
                lastRunEnemySpawn = enemySpawnLevel;
                int wallNummer = 1;
                int platformNummer = 1;
                int enemyNumber = 1;
                foreach (GameObject go in gameObjects)
                {
                    if (go.Tag == "Enemy" && enemySpawnLevel == 1)
                    {
                        switch (enemyNumber)
                        {
                            case 1:
                                go.transform.position = new Vector2(100, 150);
                                break;
                            case 2:
                                go.transform.position = new Vector2(800, 150);
                                break;
                            case 3:
                                go.transform.position = new Vector2(450, 400);
                                break;
                            case 4:
                                go.transform.position = new Vector2(750, 400);
                                break;
                            case 5:
                                go.transform.position = new Vector2(600, 675);
                                break;
                            default:
                                break;
                        }
                        enemyNumber++;
                    }

                    if (go.Tag == "Enemy" && enemySpawnLevel == 2)
                    {
                        switch (enemyNumber)
                        {
                            case 1:
                                go.transform.position = new Vector2(800, 150);
                                break;
                            case 2:
                                go.transform.position = new Vector2(450, 400);
                                break;
                            case 3:
                                go.transform.position = new Vector2(200, 675);
                                break;
                            case 4:
                                go.transform.position = new Vector2(800, 675);
                                break;
                        }
                        enemyNumber++;
                    }

                    if (go.Tag == "Enemy" && enemySpawnLevel == 3)
                    {
                        switch (enemyNumber)
                        {
                            case 1:
                                go.transform.position = new Vector2(700, 150);
                                break;
                            case 2:
                                go.transform.position = new Vector2(1300, 400);
                                break;
                            case 3:
                                go.transform.position = new Vector2(450, 400);
                                break;
                            case 4:
                                go.transform.position = new Vector2(750, 675);
                                break;
                            case 5:
                                go.transform.position = new Vector2(550, 675);
                                break;
                        }
                        enemyNumber++;
                    }

                    if (go.Tag == "Platform")
                    {
                        switch (platformNummer)
                        {
                            case 1:
                                go.transform.position = new Vector2(0, 200);
                                break;
                            case 2:
                                go.transform.position = new Vector2(600, 200);
                                break;
                            case 3:
                                go.transform.position = new Vector2(900, 200);
                                break;
                            case 4:
                                go.transform.position = new Vector2(1200, 200);
                                break;
                            case 5:
                                go.transform.position = new Vector2(1200, 450);
                                break;
                            case 6:
                                go.transform.position = new Vector2(600, 450);
                                break;
                            case 7:
                                go.transform.position = new Vector2(300, 450);
                                break;
                            case 8:
                                go.transform.position = new Vector2(0, 725);
                                break;
                            case 9:
                                go.transform.position = new Vector2(300, 725);
                                break;
                            case 10:
                                go.transform.position = new Vector2(600, 725);
                                break;
                            case 11:
                                go.transform.position = new Vector2(900, 725);
                                break;
                            case 12:
                                go.transform.position = new Vector2(1200, 725);
                                break;
                            case 16:
                                go.transform.position = new Vector2(900, 525);
                                break;
                            case 17:
                                go.transform.position = new Vector2(1000, 600);
                                break;
                            case 18:
                                go.transform.position = new Vector2(1100, 525);
                                break;
                            case 19:
                                go.transform.position = new Vector2(900, 675);
                                break;
                            case 20:
                                go.transform.position = new Vector2(1100, 675);
                                break;


                            default:
                                break;
                        }
                        platformNummer++;


                    }
                    if (go.Tag == "Wall")
                    {

                        switch (wallNummer)
                        {
                            case 1:
                                go.transform.position = new Vector2(0, 0);
                                break;

                            case 2:
                                go.transform.position = new Vector2(1380, 0);
                                break;
                            case 5:
                                go.transform.position = new Vector2(600, 200);
                                break;

                            case 6:
                                go.transform.position = new Vector2(1176, 200);
                                break;
                            case 7:
                                go.transform.position = new Vector2(298, 450);
                                break;

                            case 8:
                                go.transform.position = new Vector2(0, 725);
                                break;
                            default:
                                break;
                        }
                        wallNummer++;

                    }
                    if (go.Tag == "Door")
                    {
                        go.transform.position = new Vector2(1350, 400);
                    }

                    if (go.Tag == "Player")
                    {
                        go.transform.position = new Vector2(1200, 200);
                    }

                    if (go.Tag == "Shop")
                    {
                        go.transform.position = new Vector2(-1000, -1000);
                    }
                }
            }
            #endregion

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
            if (runTileset)
            {
                TileSet();
                runTileset = false;
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
                newObjects.Add(EnemyPool.Create(new Vector2( 400,  100), Content));

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
                    float x = (go.GetComponent("Player") as Player).gameObject.transform.position.X + 20 / 2; //The attack's position on the X-axis, based on the Player object's position (should be in the middle)
                    float y = (go.GetComponent("Player") as Player).gameObject.transform.position.Y +10; //The attack's position on the Y-axis, based on the Player object's position (Edit last number to place it probably based on the Player object's sprite)

                    newObjects.Add(BulletPool.Create(new Vector2(x, y), Content));
                    break;
                }
                
                /*
                if (go.Tag == "Bullet")
                {
                    (go.GetComponent("Bullet") as Projectiles).gameObject.transform.position = new Vector2(0, 0);
                    break;
                }
                */
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
                    /*
                    EnemyPool.ReleaseObject(go);
                    ObjectPool.ReleaseObject(go);
                    WallPool.ReleaseObject(go);
                    BulletPool.ReleaseObject(go);
                    DoorPool.ReleaseObject(go);
                    */
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
            KeyboardState keyState = Keyboard.GetState();

            GraphicsDevice.Clear(Color.CornflowerBlue);

           


            //Menues and clickdelays
            #region MainMenu


            if (currentGameState == GameState.MainMenu)
            {
                menuMaxID = 2;
                if (keyState.IsKeyDown(Keys.Up) && menuTimer == 1 && options == false)
                {
                    menuID -= 1;
                    menuTimer = 0;
                }
                if (keyState.IsKeyDown(Keys.Down) && menuTimer == 1 && options == false)
                {
                    menuID += 1;
                    menuTimer = 0;
                }

                if (keyState.IsKeyDown(Keys.Enter) && clickDelay == 0)
                {
                    if (menuID == 1)
                    {
                        currentGameState = GameState.InGame;
                        TileSet();
                        clickDelay = 30;
                    }

                    if (menuID == 2)
                    {
                        Environment.Exit(0);
                    }
                }
                spriteBatch.Begin();
                //Skriver menu teksten ud til skræmen
                    if (menuID == 1 && saves == 0)
                    {
                        spriteBatch.DrawString(mainMenuTL, "New Game", new Vector2(10, 10), Color.Black);
                        spriteBatch.DrawString(mainMenuT, "Exit to desktop", new Vector2(10, 60), Color.Black);
                    }

                    if (menuID == 2 && saves == 0)
                    {
                        spriteBatch.DrawString(mainMenuT, "New Game", new Vector2(10, 10), Color.Black);
                        spriteBatch.DrawString(mainMenuTL, "Exit to desktop", new Vector2(10, 60), Color.Black);
                    }

                    if (menuID == 1 && saves == 1)
                    {
                        spriteBatch.DrawString(mainMenuTL, "Continue", new Vector2(10, 10), Color.Black);
                        spriteBatch.DrawString(mainMenuT, "Exit to desktop", new Vector2(10, 60), Color.Black);
                    }

                    if (menuID == 2 && saves == 1)
                    {
                        spriteBatch.DrawString(mainMenuT, "Continue", new Vector2(10, 10), Color.Black);
                        spriteBatch.DrawString(mainMenuTL, "Exit to desktop", new Vector2(10, 60), Color.Black);
                    }
                spriteBatch.End();
            }

            #endregion

            #region Shop menu

            foreach (GameObject go in gameObjects)
            {
                if (go.Tag == "Shop")
                {
                    if (currentGameState == GameState.InGame && keyState.IsKeyDown(Keys.P) && shopState == false && (go.GetComponent("Shop") as Shop).shopActive == true)
                    {
                        menuID = 1;
                        menuMaxID = 3;
                        shopState = true;
                        currentGameState = GameState.ShopMenu;
                    }
                }
            }

            if (currentGameState == GameState.ShopMenu)
            {
                if (keyState.IsKeyDown(Keys.Up) && menuTimer == 1)
                {
                    menuID -= 1;
                    menuTimer = 0;
                }
                if (keyState.IsKeyDown(Keys.Down) && menuTimer == 1)
                {
                    menuID += 1;
                    menuTimer = 0;
                }

                foreach (GameObject go in gameObjects)
                {
                    if (go.Tag == "Shop")
                    {

                        #region ShopMenu Standard(actions)
                        if (keyState.IsKeyDown(Keys.Enter) && clickDelay == 0 && upgradePlayer == false && upgradeWeapon == false)
                        {
                            if (menuID == 1 && upgradeWeapon == false)
                            {
                                menuID = 1;
                                menuMaxID = 3;
                                upgradeWeapon = true;
                                clickDelay = 30;
                            }

                            if (menuID == 2 && upgradePlayer == false)
                            {
                                menuID = 1;
                                menuMaxID = 4;
                                upgradePlayer = true;
                                clickDelay = 30;
                            }

                            if (menuID == 3)
                            {
                                currentGameState = GameState.InGame;
                                shopState = false;
                                (go.GetComponent("Shop") as Shop).shopActive = false;
                            }
                        }
                        #endregion

                        #region UpgradePlayer Menu (actions)
                        if (keyState.IsKeyDown(Keys.Enter) && clickDelay == 0 && upgradePlayer == true)
                        {
                            if (menuID == 1)
                            {
                                clickDelay = 30;
                                if ((go.GetComponent("Shop") as Shop).playerArmorUpgrade == true)
                                {
                                    (go.GetComponent("Shop") as Shop).playerArmorPriceUp = true;
                                }
                            }

                            if (menuID == 2)
                            {
                                clickDelay = 30;
                            }

                            if (menuID == 3)
                            {
                                clickDelay = 30;
                                if ((go.GetComponent("Shop") as Shop).playerSpeedUpgrade == true)
                                {
                                    (go.GetComponent("Shop") as Shop).playerSpeedPriceUp = true;
                                }
                            }

                            if (menuID == 4)
                            {
                                menuID = 1;
                                menuMaxID = 3;
                                clickDelay = 30;
                                upgradePlayer = false;
                            }
                        }
                        #endregion

                        #region UpgradeWeapon Menu (actions)
                        if (keyState.IsKeyDown(Keys.Enter) && clickDelay == 0 && upgradeWeapon == true)
                        {
                            if (menuID == 1)
                            {
                                clickDelay = 30;
                                if ((go.GetComponent("Shop") as Shop).weaponDamageUpgrade == true)
                                {
                                    (go.GetComponent("Shop") as Shop).weaponDamagePriceUp = true;
                                }
                            }

                            if (menuID == 2)
                            {
                                clickDelay = 30;
                                if ((go.GetComponent("Shop") as Shop).weaponAttackSpeedUpgrade == true)
                                {
                                    (go.GetComponent("Shop") as Shop).weaponAttackSpeedPriceUp = true;
                                }
                            }

                            if (menuID == 3)
                            {
                                menuID = 1;
                                menuMaxID = 3;
                                clickDelay = 30;
                                upgradeWeapon = false;
                            }
                        }
                        #endregion

                        #region ShopMenu (standard)
                        if (upgradePlayer == false && upgradeWeapon == false)
                        {
                            spriteBatch.Begin();
                            if (menuID == 1)
                            {
                                spriteBatch.DrawString(mainMenuTL, "Upgrade Weapon", new Vector2(30, 10), Color.Black);
                                spriteBatch.DrawString(mainMenuT, "Upgrade Player", new Vector2(30, 60), Color.Black);
                                spriteBatch.DrawString(mainMenuT, "Close shop", new Vector2(30, 110), Color.Black);
                            }

                            if (menuID == 2)
                            {
                                spriteBatch.DrawString(mainMenuT, "Upgrade Weapon", new Vector2(30, 10), Color.Black);
                                spriteBatch.DrawString(mainMenuTL, "Upgrade Player", new Vector2(30, 60), Color.Black);
                                spriteBatch.DrawString(mainMenuT, "Close shop", new Vector2(30, 110), Color.Black);
                            }

                            if (menuID == 3)
                            {
                                spriteBatch.DrawString(mainMenuT, "Upgrade Weapon", new Vector2(30, 10), Color.Black);
                                spriteBatch.DrawString(mainMenuT, "Upgrade Player", new Vector2(30, 60), Color.Black);
                                spriteBatch.DrawString(mainMenuTL, "Close shop", new Vector2(30, 110), Color.Black);
                            }
                            spriteBatch.End();
                        }
                        #endregion

                        #region UpgradePlayer menu
                        if (upgradePlayer == true)
                        {
                            spriteBatch.Begin();
                            if (menuID == 1)
                            {
                                spriteBatch.DrawString(mainMenuT, "  -- Player --", new Vector2(30, 10), Color.Black, 0, Vector2.Zero, 0, SpriteEffects.None, -10f);
                                spriteBatch.DrawString(mainMenuTL, "Upgrade Armor", new Vector2(30, 100), Color.Black, 0, Vector2.Zero, 0, SpriteEffects.None, -10f);
                                spriteBatch.DrawString(mainMenuT, "Upgrade Speed", new Vector2(30, 160), Color.Black, 0, Vector2.Zero, 0, SpriteEffects.None, -10f);
                                spriteBatch.DrawString(mainMenuT, "Back", new Vector2(30, 210), Color.Black, 0, Vector2.Zero, 0, SpriteEffects.None, -10f);
                                spriteBatch.DrawString(mainMenuT, "Price:" + ((go.GetComponent("Shop") as Shop).playerArmorPrice), new Vector2(30, 60), Color.Black, 0, Vector2.Zero, 0, SpriteEffects.None, -10f);
                            }

                            if (menuID == 2)
                            {
                                spriteBatch.DrawString(mainMenuT, "  -- Player --", new Vector2(30, 10), Color.Black);
                                spriteBatch.DrawString(mainMenuT, "Upgrade Armor", new Vector2(30, 100), Color.Black);
                                spriteBatch.DrawString(mainMenuT, "Upgrade Speed", new Vector2(30, 160), Color.Black);
                                spriteBatch.DrawString(mainMenuT, "Back", new Vector2(30, 210), Color.Black);
                                spriteBatch.DrawString(mainMenuT, "Price:", new Vector2(30, 60), Color.Black);
                            }

                            if (menuID == 3)
                            {
                                spriteBatch.DrawString(mainMenuT, "  -- Player --", new Vector2(30, 10), Color.Black);
                                spriteBatch.DrawString(mainMenuT, "Upgrade Armor", new Vector2(30, 100), Color.Black);
                                spriteBatch.DrawString(mainMenuTL, "Upgrade Speed", new Vector2(30, 160), Color.Black);
                                spriteBatch.DrawString(mainMenuT, "Back", new Vector2(30, 210), Color.Black);
                                spriteBatch.DrawString(mainMenuT, "Price:" + ((go.GetComponent("Shop") as Shop).playerSpeedPrice), new Vector2(30, 60), Color.Black);
                            }
                            if (menuID == 4)
                            {
                                spriteBatch.DrawString(mainMenuT, "  -- Player --", new Vector2(30, 10), Color.Black);
                                spriteBatch.DrawString(mainMenuT, "Upgrade Armor", new Vector2(30, 100), Color.Black);
                                spriteBatch.DrawString(mainMenuT, "Upgrade Speed", new Vector2(30, 160), Color.Black);
                                spriteBatch.DrawString(mainMenuTL, "Back", new Vector2(30, 210), Color.Black);
                                spriteBatch.DrawString(mainMenuT, "Price:", new Vector2(30, 60), Color.Black);
                            }
                            spriteBatch.End();
                        }
                        #endregion

                        #region UpgradeWeapon Menu
                        if (upgradeWeapon == true)
                        {
                            spriteBatch.Begin();
                            if (menuID == 1)
                            {
                                spriteBatch.DrawString(mainMenuT, "  -- Weapon --", new Vector2(30, 10), Color.Black);
                                spriteBatch.DrawString(mainMenuTL, "Upgrade Damage", new Vector2(30, 110), Color.Black);
                                spriteBatch.DrawString(mainMenuT, "Upgrade AttackSpeed", new Vector2(30, 160), Color.Black);
                                spriteBatch.DrawString(mainMenuT, "Back", new Vector2(30, 210), Color.Black);
                                spriteBatch.DrawString(mainMenuT, "Price:" + ((go.GetComponent("Shop") as Shop).weaponDamagePrice), new Vector2(30, 60), Color.Black);
                            }

                            if (menuID == 2)
                            {
                                spriteBatch.DrawString(mainMenuT, "  -- Weapon --", new Vector2(30, 10), Color.Black);
                                spriteBatch.DrawString(mainMenuT, "Upgrade Damage", new Vector2(30, 110), Color.Black);
                                spriteBatch.DrawString(mainMenuTL, "Upgrade AttackSpeed", new Vector2(30, 160), Color.Black);
                                spriteBatch.DrawString(mainMenuT, "Back", new Vector2(30, 210), Color.Black);
                                spriteBatch.DrawString(mainMenuT, "Price:" + ((go.GetComponent("Shop") as Shop).weaponAttackSpeedPrice), new Vector2(30, 60), Color.Black);
                            }
                            if (menuID == 3)
                            {
                                spriteBatch.DrawString(mainMenuT, "  -- Weapon --", new Vector2(30, 10), Color.Black);
                                spriteBatch.DrawString(mainMenuT, "Upgrade Damage", new Vector2(30, 110), Color.Black);
                                spriteBatch.DrawString(mainMenuT, "Upgrade AttackSpeed", new Vector2(30, 160), Color.Black);
                                spriteBatch.DrawString(mainMenuTL, "Back", new Vector2(30, 210), Color.Black);
                                spriteBatch.DrawString(mainMenuT, "Price:", new Vector2(30, 60), Color.Black);
                            }
                            spriteBatch.End();
                        }
                        #endregion

                    }
                }
            }

            #endregion

            #region ClickDelay, MenuID check, MenuTimer
            //Delay før man kan klikke på enter igen
            if (clickDelay > 0)
            {
                clickDelay -= 1;
            }
            else if (clickDelay < 0)
            {
                clickDelay = 0;
            }

            //Hvis man er i bunden af menuen og klikker ned kommer man op til toppen og omvendt
            if (menuID < menuMinID)
            {
                menuID = menuMaxID;
            }
            else if (menuID > menuMaxID)
            {
                menuID = menuMinID;
            }

            //Et delay så man kan gå rundt i menuen i et normalt tempo
            if (menuTimer == 0)
            {
                menuTimerCheck += 1;
                if (menuTimerCheck >= 15)
                {
                    menuTimer = 1;
                    menuTimerCheck = 0;
                }
            }
            #endregion
            //Menues and clickdelays

            // TODO: Add your drawing code here

            //Draws all GameObjects
            spriteBatch.Begin();
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
