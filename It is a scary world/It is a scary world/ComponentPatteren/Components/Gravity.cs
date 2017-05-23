using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace It_is_a_scary_world
{
    class Gravity : Component, IUpdateable, ICollisionExit, ICollisionEnter
    {
        public Vector2 velocity { get; set; }
        private Transform transform;
        private readonly Vector2 gravity = new Vector2(0, 15.8F);

        private Vector2 oldPos;
        private Vector2 newPos;
        public bool isFalling { get; set; }
        public Collider collidingObject;
        public List<Collider> collidingObjects { get; private set; }
        public bool grounded = false;

        public int movementSpeed = 200;

        private GameObject go;

        public Gravity(Transform transform, GameObject gameObject) : base(gameObject)
        {
            this.transform = transform;
            this.go = gameObject;
            collidingObjects = new List<Collider>();
        }

        public void Update()
        {
            if (grounded == false)
            {
                oldPos = go.transform.position;
                movementSpeed = 200;
                KeyboardState keyState = Keyboard.GetState();
                Vector2 translation = Vector2.Zero;

                if (isFalling)
                {
                    if (velocity.Y > 500)
                    {
                        velocity = new Vector2(velocity.X, 500);
                    }
                    else
                    {
                        velocity += gravity;
                    }

                    newPos = go.transform.position += velocity * GameWorld.Instance.deltaTime;
                }
                transform.Translate(translation * movementSpeed * GameWorld.Instance.deltaTime);
            }
        }



        public void OnCollisionEnter(Collider other)
        {
            
            if (other.gameObject.Tag == "Platform")
            {
                collidingObjects.Add(other);
            }
            Collider box = (gameObject.GetComponent("Collider") as Collider);

            if (other.gameObject.Tag == "Platform")
            {
                //grounded = true;
                collidingObject = other;
                isFalling = false;
                velocity = Vector2.Zero;
                this.transform.position = new Vector2(this.transform.position.X, other.CollisionBox.Y - box.CollisionBox.Height + 3);
            }
            
        }

        public void OnCollisionExit(Collider other)
        {
            
            if (other.gameObject.Tag == "Platform")
            {
                Collider box = (gameObject.GetComponent("Collider") as Collider);
                this.transform.position = new Vector2(this.transform.position.X, other.CollisionBox.Y - box.CollisionBox.Height + 3);
                //if (collidingObjects.Count <= 2)
                //{
                collidingObject = null;
                isFalling = true;
                //}                
                collidingObjects.Remove(other);
            }
            
        }
    }
}
