using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using System.Drawing;

namespace It_is_a_scary_world
{
    enum DIRECTION { Front, Back, Left, Right };

    class Player : Component, IUpdateable, ILoadable, IAnimateable, ICollisionEnter, ICollisionExit, ICollisionStay
    {
        private IStrategy strategy;

        private bool canMove = true;

        private DIRECTION direction;


        //WallCollision
        public bool rightWallCollision;
        public bool leftWallCollision;
        //WallCollision

        //Jump
        public bool doubleJump = true;
        public int maxJump = 15;
        public int currentJump = 0;
        public int jumpTimer;
        //Jump

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
        private int health = 1;
        private int armor = 2;
        private int gold;
        private double exp;
        private double expToLevel = 100;
        private int level = 1;
        private int levelReward;
        private bool checkLevelReward;
        private int damage = 100;
        public float movementSpeed { get; set; } = 100;
        #endregion

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
            animator.CreateAnimation("Attack", new Animation(2, 0, 0, 29, 43, 12, Vector2.Zero, spriteRenderer.Sprite));

            animator.CreateAnimation("IdleBack", new Animation(2, 0, 0, 29, 43, 3, Vector2.Zero, spriteRenderer.Sprite));

            animator.CreateAnimation("WalkFront", new Animation(5, 86, 0, 29, 43, 5, Vector2.Zero, spriteRenderer.Sprite));
            animator.CreateAnimation("WalkBack", new Animation(5, 43, 0, 29, 43, 5, Vector2.Zero, spriteRenderer.Sprite));
            //

            animator.CreateAnimation("IdleLeft", new Animation(2, 0, 0, 29, 43, 6, Vector2.Zero, spriteRenderer.Sprite));
            animator.CreateAnimation("IdleRight", new Animation(2, 0, 0, 29, 43, 3, Vector2.Zero, spriteRenderer.Sprite));

            animator.CreateAnimation("WalkLeft", new Animation(5, 86, 0, 29, 43, 5, Vector2.Zero, spriteRenderer.Sprite));
            animator.CreateAnimation("WalkRight", new Animation(5, 43, 0, 29, 43, 5, Vector2.Zero, spriteRenderer.Sprite));

            //Plays an animation to make sure that we have an animation to play
            //If we don't do this we will get an exception
            animator.PlayAnimation("IdleLeft");

            strategy = new Idle(animator);
        }

        public void Update()
        {
            KeyboardState keyState = Keyboard.GetState();

            #region LevelSystem and perks

            if ((int)Math.Ceiling(exp) >= (int)Math.Ceiling(expToLevel))
            {
                level += 1;
                levelReward += 1;
                exp -= (int)Math.Ceiling(expToLevel);
                expToLevel = (int)Math.Ceiling(expToLevel) * 1.2;

                #region LevelRewards 
                if (levelReward == 1)
                {
                    armor += 1;
                }

                if (levelReward == 2)
                {
                    armor += 1;
                }

                if (levelReward == 3)
                {
                    armor += 1;
                }

                if (levelReward == 4)
                {
                    armor += 1;
                }

                if (levelReward == 5)
                {
                    armor += 1;
                }
                #endregion
            }

            #endregion

            #region Death

            if (health <= 0)
            {

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



        /// <summary>
        /// Loads the player's content
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            //Sets up a reference to the palyer's animator
            animator = (Animator)gameObject.GetComponent("Animator");

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
            (other.gameObject.GetComponent("SpriteRenderer") as SpriteRenderer).Color = Color.Red;

            //Gives the players collisionbox
            Collider playerBox = (this.gameObject.GetComponent("Collider") as Collider);

            if (other.gameObject.Tag == "Platform")
            {
                //TopCollision
                if (playerBox.CollisionBox.Bottom >= other.CollisionBox.Top)
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

            /*
            if (other.gameObject.Tag == "Platform")
            {
                if (playerBox.CollisionBox.Bottom >= other.CollisionBox.Top)
                {
                    (go.GetComponent("Gravity") as Gravity).grounded = true;
                    platformTimer = 5;
                }
            }
            */
            
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

            if (other.gameObject.Tag != "Wall")
            {
                leftWallCollision = false;
                rightWallCollision = false;
            }
            
        }
        
    }
}

