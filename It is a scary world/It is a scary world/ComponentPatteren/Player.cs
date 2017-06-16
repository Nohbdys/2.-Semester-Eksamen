using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using System.Drawing;
using Microsoft.Xna.Framework.Graphics;

namespace It_is_a_scary_world
{
    enum DIRECTION { Front, Back, Left, Right };

    class Player : Component, IUpdateable, ILoadable, IAnimateable, ICollisionEnter, ICollisionExit, ICollisionStay, IDrawable
    {
        private IStrategy strategy;

        private bool canMove = true;

        private DIRECTION direction;


        //WallCollision
        public bool rightWallCollision;
        public bool leftWallCollision;
        //WallCollision

        //Jump
        public bool doubleJump;
        public int currentJump = 0;
        public int jumpTimer;
        //Jump

        private SpriteFont mainMenuT;

        //test
        private bool platformCheck;
        private int platformTimer;
        private bool isAttacking;
        private GameObject go;
        private Transform transform;
        public List<Collider> collidingObjects { get; private set; }
        //testslut

        /// <summary>
        /// A reference to the player's animator
        /// </summary>
        private Animator animator;

        //     public Vector2 position { get; set; }

        #region Stats (player)
        public float health = 1;
        public float armor = 2;
        public float maxArmor = 3;
        public float exp;
        private float expToLevel = 100;
        private int level = 1;
        private int levelReward;
        private bool checkLevelReward;
        public float damage { get; set; } = 25;
        public float movementSpeed { get; set; } = 100;
        public float attackSpeed = 1;
        #endregion

        public int iFrameSkeleton;
        public int iFrameGhost;

        public Player(GameObject gameObject, Transform transform) : base(gameObject)
        {
            this.go = gameObject;
            this.transform = transform;
            collidingObjects = new List<Collider>();
        }

        /// <summary>
        /// Creates all the player's animations
        /// </summary>
        private void CreateAnimations()
        {
            SpriteRenderer spriteRenderer = (SpriteRenderer)gameObject.GetComponent("SpriteRenderer");

            animator.CreateAnimation("IdleFront", new Animation(2, 0, 0, 29, 43, 3, Vector2.Zero, spriteRenderer.Sprite));

            animator.CreateAnimation("AttackRight", new Animation(3, 129, 0, 31, 43, 12 * attackSpeed, Vector2.Zero, spriteRenderer.Sprite));
            animator.CreateAnimation("AttackLeft", new Animation(3, 172, 0, 31, 43, 12 * attackSpeed, Vector2.Zero, spriteRenderer.Sprite));
            animator.CreateAnimation("AttackFront", new Animation(3, 129, 0, 31, 43, 12 * attackSpeed, Vector2.Zero, spriteRenderer.Sprite));
            animator.CreateAnimation("AttackBack", new Animation(3, 172, 0, 31, 43, 12 * attackSpeed, Vector2.Zero, spriteRenderer.Sprite));

            animator.CreateAnimation("IdleBack", new Animation(2, 0, 0, 29, 43, 3, Vector2.Zero, spriteRenderer.Sprite));

            animator.CreateAnimation("WalkFront", new Animation(5, 86, 0, 29, 43, 8, Vector2.Zero, spriteRenderer.Sprite));
            animator.CreateAnimation("WalkBack", new Animation(5, 43, 0, 29, 43, 8, Vector2.Zero, spriteRenderer.Sprite));

            animator.CreateAnimation("IdleLeft", new Animation(2, 0, 0, 29, 43, 7, Vector2.Zero, spriteRenderer.Sprite));
            animator.CreateAnimation("IdleRight", new Animation(2, 0, 0, 29, 43, 7, Vector2.Zero, spriteRenderer.Sprite));

            animator.CreateAnimation("WalkLeft", new Animation(5, 86, 0, 29, 43, 8, Vector2.Zero, spriteRenderer.Sprite));
            animator.CreateAnimation("WalkRight", new Animation(5, 43, 0, 29, 43, 8, Vector2.Zero, spriteRenderer.Sprite));

            animator.CreateAnimation("Pause", new Animation(1, 0, 0, 29, 43, 0, Vector2.Zero, spriteRenderer.Sprite));

            //Plays an animation to make sure that we have an animation to play
            //If we don't do this we will get an exception
            animator.PlayAnimation("IdleLeft");

            strategy = new Idle(animator);
            }


        public void Update()
        {
            if (GameWorld.Instance.currentGameState == GameState.InGame)
            {
                KeyboardState keyState = Keyboard.GetState();

                #region LevelSystem and perks

                if ((int)Math.Ceiling(exp) >= (int)Math.Ceiling(expToLevel) && level < 50)
                {
                    level += 1;
                    levelReward += 1;
                    exp -= (int)Math.Ceiling(expToLevel);
                    expToLevel = (int)Math.Ceiling(expToLevel) * 1.2f;

                    if (maxArmor < 5)
                    {
                        maxArmor += 1;
                    }

                    #region LevelRewards 
                    if (levelReward == 5)
                    {
                        //doubleJump = true;
                    }
                    #endregion
                }

                #endregion

                #region Death

                if (health <= 0)
                {
                    Death();
                }

                #endregion

                #region Checks collision with platform
                if (platformTimer > 0)
                {
                    platformTimer -= 1;
                }
                else if (platformTimer <= 0)
                {
                    platformCheck = false;
                }
                #endregion

                #region JumpTimer

                if (jumpTimer > 0)
                {
                    jumpTimer -= 1;
                }
                else if (jumpTimer < 0)
                {
                    jumpTimer = 0;
                }

                #endregion

                #region IFrame

                if (iFrameSkeleton > 0)
                {
                    iFrameSkeleton -= 1;
                }
                if (iFrameSkeleton < 0)
                {
                    iFrameSkeleton = 0;
                }

                if (iFrameGhost > 0)
                {
                    iFrameGhost -= 1;
                }
                if (iFrameGhost < 0)
                {
                    iFrameGhost = 0;
                }
                #endregion

                if (canMove)
                {
                    if (keyState.IsKeyDown(Keys.W) || keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.D))
                    {
                        if (!(strategy is Movement))
                        {
                            //There is made a new movement every time so i need to add dobulejump currentjump and maxjump to constructer 
                            strategy = new Movement(gameObject.transform, animator, gameObject);
                        }
                    }
                    else
                    {
                        strategy = new Idle(animator);
                    }

                    if (keyState.IsKeyDown(Keys.Space))
                    {
                        strategy = new Attack(animator);
                        canMove = false;

                        GameWorld.Instance.SpawnBullet();
                    }

                    strategy.Execute(ref direction);
                }

            }
            
            if (GameWorld.Instance.currentGameState == GameState.ShopMenu)
            {
                strategy = new Pause(animator);

                strategy.Execute(ref direction);
            }
            
        }



        /// <summary>
        /// Loads the player's content
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            //Sets up a reference to the palyer's animator
            animator = (Animator)gameObject.GetComponent("Animator");

            mainMenuT = content.Load<SpriteFont>("MainMenu");
        

            //We can make our animations when we have a reference to the player's animator.
            CreateAnimations();
        }

        public void OnAnimationDone(string animationName)
        {
            if (animationName.Contains("Attack"))
            {
                isAttacking = false;
                canMove = true;
            }
        }

        public void OnCollisionEnter(Collider other)
        {
            //used to test (see) collision
       //     (other.gameObject.GetComponent("SpriteRenderer") as SpriteRenderer).Color = Color.Red;

            //Gives the players collisionbox
            Collider playerBox = (this.gameObject.GetComponent("Collider") as Collider);


            if (other.gameObject.Tag == "Door")
            {

                GameWorld.Instance.runTileset = true;
                (go.GetComponent("Gravity") as Gravity).isFalling = true;
            }
        }

        public void OnCollisionExit(Collider other)
        {

            if (other.gameObject.Tag == "Wall")
            {
                rightWallCollision = false;
                leftWallCollision = false;
            }
            if (other.gameObject.Tag == "Platform" && platformCheck == false)
            {
                (go.GetComponent("Gravity") as Gravity).isFalling = true;
                (other.gameObject.GetComponent("SpriteRenderer") as SpriteRenderer).Color = Color.White;
            }



        }

        public void OnCollisionStay(Collider other)
        {
            
            Collider playerBox = (this.gameObject.GetComponent("Collider") as Collider);
            
            if (other.gameObject.Tag == "Wall")
            {       
                  
                //player left side collision
                if (playerBox.CollisionBox.Left >= other.CollisionBox.Left &&
                    playerBox.CollisionBox.Left <= other.CollisionBox.Right &&
                    playerBox.CollisionBox.Top <= other.CollisionBox.Bottom &&
                    playerBox.CollisionBox.Bottom >= other.CollisionBox.Top)
                {
                    rightWallCollision = true;
                }
                //player right side collision
                if (playerBox.CollisionBox.Right <= other.CollisionBox.Right &&
                    playerBox.CollisionBox.Right >= other.CollisionBox.Left  &&
                    playerBox.CollisionBox.Top <= other.CollisionBox.Bottom  &&
                    playerBox.CollisionBox.Bottom >= other.CollisionBox.Top )
                {

                    leftWallCollision = true;
                }
            }

            if (other.gameObject.Tag == "Platform")
            {
                //TopCollision
                if (transform.position.Y < other.gameObject.transform.position.Y)//.CollisionBox.Bottom >= other.CollisionBox.Top)
                {
                    currentJump = 0;
                    platformCheck = true;
                    (go.GetComponent("Gravity") as Gravity).isFalling = false;
                }
                if (playerBox.CollisionBox.Y >= other.CollisionBox.Y)
                {
                    (go.GetComponent("Gravity") as Gravity).isFalling = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (GameWorld.Instance.currentGameState == GameState.InGame)
            {
                spriteBatch.DrawString(mainMenuT, "Level:" + level, new Vector2(30, 10), Color.White);
                spriteBatch.DrawString(mainMenuT, "Health:" + health, new Vector2(30, 30), Color.White);
                spriteBatch.DrawString(mainMenuT, "Armor:" + armor, new Vector2(30, 50), Color.White);
                foreach (GameObject go in GameWorld.Instance.gameObjects)
                {
                    if (go.Tag == "Shop")
                    {
                        spriteBatch.DrawString(mainMenuT, "Gold:" + (go.GetComponent("Shop") as Shop).gold, new Vector2(30, 70), Color.White);
                        break;
                    }                  
                }
                spriteBatch.DrawString(mainMenuT, "Exp:" + exp + " / " + (int)Math.Ceiling(expToLevel), new Vector2(30, 100), Color.White);

                if (health <= 0)
                {
                    spriteBatch.DrawString(mainMenuT, "You died", new Vector2(500, 300), Color.White);
                }
            }
        }

        public void Death()
        {
            GameWorld.Instance.currentGameState = GameState.Dead;
        }
    }
}

