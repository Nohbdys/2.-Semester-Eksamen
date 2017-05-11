﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace It_is_a_scary_world
{
    enum DIRECTION { Front, Back, Left, Right };

    class Player : Component, IUpdateable, ILoadable, IAnimateable, ICollisionEnter, ICollisionExit, ICollisionStay
    {
        private IStrategy strategy;

        private bool canMove = true;

        private DIRECTION direction;

        //test
        private bool isAttacking;
        private GameObject go;
        private Transform transform;
        public List<Collider> collidingObjects { get; private set; }
        //testslut

        /// <summary>
        /// A reference to the player's animator
        /// </summary>
        private Animator animator;

        public Vector2 position { get; set; }

        #region Stats (jump in testing)

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

            //Test gravity
            if (keyState.IsKeyDown(Keys.Space) && canMove)
            {
                strategy = new Attack(animator);
                canMove = false;
                isAttacking = true;
            }
            //test gravity slut

            if (canMove)
            {
                if (keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.D))
                {
                    if (!(strategy is Walk))
                    {
                        strategy = new Walk(gameObject.transform, animator);
                    }
                }
                else if (keyState.IsKeyDown(Keys.W))
                {
                    if (!(strategy is Jump))
                    {
                        strategy = new Jump(gameObject.transform, animator, gameObject);
                    }
                }
                else
                {
                    strategy = new Idle(animator);
                }
                /*
                if (keyState.IsKeyDown(Keys.Space))
                {
                    strategy = new Attack(animator);

                    canMove = false;
                }
                */
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
            Collider box = (gameObject.GetComponent("Collider") as Collider);

            (other.gameObject.GetComponent("SpriteRenderer") as SpriteRenderer).Color = Color.Red;
        }

        public void OnCollisionExit(Collider other)
        {
            (other.gameObject.GetComponent("SpriteRenderer") as SpriteRenderer).Color = Color.White;
        }

        public void OnCollisionStay(Collider other)
        {
            /*
            KeyboardState keyState = Keyboard.GetState();
            Collider playerBox = (this.gameObject.GetComponent("Collider") as Collider);


            if (other.gameObject.Tag == "Platform")
            {
                //player left side collision
                if (playerBox.CollisionBox.Left >= other.CollisionBox.Left &&
                    playerBox.CollisionBox.Left <= other.CollisionBox.Right + 5 &&
                    playerBox.CollisionBox.Top <= other.CollisionBox.Bottom - 10 &&
                    playerBox.CollisionBox.Bottom >= other.CollisionBox.Top + 10)
                {
                    this.transform.position = new Vector2(other.CollisionBox.X + other.CollisionBox.Width + 2, this.transform.position.Y);
                }
                //player right side collision
                if (playerBox.CollisionBox.Right <= other.CollisionBox.Right &&
                    playerBox.CollisionBox.Right >= other.CollisionBox.Left - 5 &&
                    playerBox.CollisionBox.Top <= other.CollisionBox.Bottom - 10 &&
                    playerBox.CollisionBox.Bottom >= other.CollisionBox.Top + 10)
                {
                    this.transform.position = new Vector2(other.CollisionBox.X - playerBox.CollisionBox.Width - 2, playerBox.CollisionBox.Y);
                }
                //player top side collision
                if (playerBox.CollisionBox.Top <= other.CollisionBox.Bottom + (other.CollisionBox.Height / 5) &&
                    playerBox.CollisionBox.Top >= other.CollisionBox.Bottom - 3 &&
                    playerBox.CollisionBox.Right >= other.CollisionBox.Left + 10 &&
                    playerBox.CollisionBox.Left <= other.CollisionBox.Right - 10)
                {
                    (go.GetComponent("Gravity") as Gravity).velocity = new Vector2(0, 0);

                    this.transform.position = new Vector2(this.transform.position.X, other.CollisionBox.Y + other.CollisionBox.Height);

                }
            }
            */
        }
    }
}
