using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using static It_is_a_scary_world.DIRECTION;

namespace It_is_a_scary_world
{
    class Weapons : Component
    {
        private IStrategy strategy;

        private Vector2 position;

        private GameObject go;
        private Transform transform;
        private Animator animator;



        public Weapons(GameObject gameObject, Transform transform) : base(gameObject)
        {
            this.go = gameObject;
            this.transform = transform;
            gameObject.Tag = "Weapon";
        }      
        
        public void LoadContent(ContentManager content)
        {
            animator = (Animator)gameObject.GetComponent("Animator");

            Texture2D sprite = content.Load<Texture2D>("Katana");

            animator.CreateAnimation("IdleFront", new Animation(1, 0, 0, 100, 100, 0, Vector2.Zero, sprite));

            animator.PlayAnimation("IdleFront");

            strategy = new Idle(animator);
        }

        //Note til mig om sværd position (casper)
        /*   
        position.X = (go.GetComponent("Player") as Player).gameObject.transform.position.X + 1;
        position.Y = (go.GetComponent("Player") as Player).gameObject.transform.position.Y;         
        */
    }
}
