using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace It_is_a_scary_world
{
    enum DIRECTION { Front, Back, Left, Right };

    class Player : Component, IUpdateable, ILoadable, IAnimateable, ICollisionEnter, ICollisionExit
    {
        private IStrategy strategy;

        private bool canMove = true;

        private DIRECTION direction;

        /// <summary>
        /// A reference to the player's animator
        /// </summary>
        private Animator animator;

        //Gravity
        public bool grounded { get; set; } = false;
        public Vector2 position { get; set; }
        //GravityEnd

        #region Stats (jump in testing)

        public float movementSpeed { get; set; } = 100;

        #endregion

        public Player(GameObject gameObject) : base(gameObject)
        {

        }

        /// <summary>
        /// Creates all the player's animations
        /// </summary>
        private void CreateAnimations()
        {
            SpriteRenderer spriteRenderer = (SpriteRenderer)gameObject.GetComponent("SpriteRenderer");

            animator.CreateAnimation("IdleFront", new Animation(4, 0, 0, 90, 150, 6, Vector2.Zero, spriteRenderer.Sprite));
            animator.CreateAnimation("IdleBack", new Animation(4, 0, 4, 90, 150, 6, Vector2.Zero, spriteRenderer.Sprite));
            animator.CreateAnimation("IdleLeft", new Animation(4, 0, 8, 90, 150, 6, Vector2.Zero, spriteRenderer.Sprite));
            animator.CreateAnimation("IdleRight", new Animation(4, 0, 12, 90, 150, 6, Vector2.Zero, spriteRenderer.Sprite));
            animator.CreateAnimation("WalkFront", new Animation(4, 150, 0, 90, 150, 6, Vector2.Zero, spriteRenderer.Sprite));
            animator.CreateAnimation("WalkBack", new Animation(4, 150, 4, 90, 150, 6, Vector2.Zero, spriteRenderer.Sprite));
            animator.CreateAnimation("WalkLeft", new Animation(4, 150, 8, 90, 150, 6, Vector2.Zero, spriteRenderer.Sprite));
            animator.CreateAnimation("WalkRight", new Animation(4, 150, 12, 90, 150, 6, Vector2.Zero, spriteRenderer.Sprite));
            animator.CreateAnimation("AttackFront", new Animation(4, 300, 0, 145, 160, 8, new Vector2(-50, 0), spriteRenderer.Sprite));
            animator.CreateAnimation("AttackBack", new Animation(4, 465, 0, 170, 155, 8, new Vector2(-20, 0), spriteRenderer.Sprite));
            animator.CreateAnimation("AttackRight", new Animation(4, 620, 0, 150, 150, 8, Vector2.Zero, spriteRenderer.Sprite));
            animator.CreateAnimation("AttackLeft", new Animation(4, 770, 0, 150, 150, 8, new Vector2(-60, 0), spriteRenderer.Sprite));
            animator.CreateAnimation("DieFront", new Animation(3, 920, 0, 150, 150, 5, Vector2.Zero, spriteRenderer.Sprite));
            animator.CreateAnimation("DieBack", new Animation(3, 920, 3, 150, 150, 5, Vector2.Zero, spriteRenderer.Sprite));
            animator.CreateAnimation("DieLeft", new Animation(3, 1070, 0, 150, 150, 5, Vector2.Zero, spriteRenderer.Sprite));
            animator.CreateAnimation("DieRight", new Animation(3, 1070, 3, 150, 150, 5, Vector2.Zero, spriteRenderer.Sprite));

            //Plays an animation to make sure that we have an animation to play
            //If we don't do this we will get an exception
            animator.PlayAnimation("IdleFront");

            strategy = new Idle(animator);
        }

        public void Update()
        {
            KeyboardState keyState = Keyboard.GetState();

            //Gravity

            if (position.Y <= 500)
            {               
                grounded = true;
                position = new Vector2(0, 0);
            }

            //GravityEnd

            if (canMove)
            {
                if (keyState.IsKeyDown(Keys.W) || keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.S) || keyState.IsKeyDown(Keys.D))
                {
                    if (!(strategy is Walk))
                    {
                        strategy = new Walk(gameObject.transform, animator);
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
                        if (true) //If canAttack is true, if reloading is false, and if the Player's current Weapon is the Pistol
                        {

                            GameWorld.Instance.SpawnBullet();
                            
                        }
                    }
                    canMove = false;
                }
            }

            strategy.Execute(ref direction);
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
                canMove = true;
            }
        }

        public void OnCollisionEnter(Collider other)
        {
            (other.gameObject.GetComponent("SpriteRenderer") as SpriteRenderer).Color = Color.Red;
        }

        public void OnCollisionExit(Collider other)
        {
            (other.gameObject.GetComponent("SpriteRenderer") as SpriteRenderer).Color = Color.White;
        }
    }
}
