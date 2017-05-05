using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace It_is_a_scary_world
{
    public class Collider : Component, IDrawable, ILoadable, IUpdateable
    {
        /// <summary>
        /// A reference to the boxcolliders spriterenderer
        /// </summary>
        public SpriteRenderer spriteRenderer { get; private set; }

        private Animator animator;

        /// <summary>
        /// A reference to the colliders texture
        /// </summary>
        private Texture2D texture;

        /// <summary>
        /// Indicates if we need to use pixel collision or not
        /// </summary>
        public bool UsePixelCollision { get; set; }

        /// <summary>
        /// Indicates if this collider needs to check for collisions
        /// </summary>
        public bool DoCollisionChecks { get; set; }

        /// <summary>
        /// Contains all the colliders that this collider is colliding with
        /// </summary>
        private HashSet<Collider> otherColliders = new HashSet<Collider>();

        /// <summary>
        /// Dictionary that contains pixels for all animations
        /// </summary>
        private Dictionary<string, Color[][]> pixels = new Dictionary<string, Color[][]>();

        private Color[] CurrentPixels
        {
            get
            {
                return pixels[animator.AnimationName][animator.CurrentIndex];
            }
        }

        /// <summary>
        /// The colliders collisionbox
        /// </summary>
        public Rectangle CollisionBox
        {
            get
            {
                return new Rectangle
                    (
                        (int)(gameObject.transform.position.X + spriteRenderer.Offset.X),
                        (int)(gameObject.transform.position.Y + spriteRenderer.Offset.Y),
                        spriteRenderer.Rectangle.Width,
                        spriteRenderer.Rectangle.Height
                    );
            }
        }

        public Collider(GameObject gameObject) : base(gameObject)
        {
            DoCollisionChecks = true;

            UsePixelCollision = true;

            spriteRenderer = (SpriteRenderer)gameObject.GetComponent("SpriteRenderer");

            animator = (Animator)gameObject.GetComponent("Animator");

            GameWorld.Instance.Colliders.Add(this);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("CollisionTexture");

            CachePixels();
        }

        public void Update()
        {
            CheckCollision();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle topLine = new Rectangle(CollisionBox.X, CollisionBox.Y, CollisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(CollisionBox.X, CollisionBox.Y + CollisionBox.Height, CollisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(CollisionBox.X + CollisionBox.Width, CollisionBox.Y, 1, CollisionBox.Height);
            Rectangle leftLine = new Rectangle(CollisionBox.X, CollisionBox.Y, 1, CollisionBox.Height);

            spriteBatch.Draw(texture, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        private void CachePixels()
        {
            foreach (KeyValuePair<string, Animation> pair in animator.MyAnimations)
            {
                Animation animation = pair.Value;

                Color[][] colors = new Color[animation.MyFrames][];

                for (int i = 0; i < animation.MyFrames; i++)
                {
                    colors[i] = new Color[animation.Rectangles[i].Width * animation.Rectangles[i].Height];

                    spriteRenderer.Sprite.GetData(0, animation.Rectangles[i], colors[i], 0, animation.Rectangles[i].Width * animation.Rectangles[i].Height);
                }

                pixels.Add(pair.Key, colors);
            }
        }

        private void CheckCollision()
        {
            if (DoCollisionChecks)
            {
                foreach (Collider other in GameWorld.Instance.Colliders)
                {
                    if (other != this)
                    {
                        if (CollisionBox.Intersects(other.CollisionBox) && ((UsePixelCollision && CheckPixelCollision(other)) || !UsePixelCollision))
                        {
                            gameObject.OnCollisionStay(other);

                            if (!otherColliders.Contains(other))
                            {
                                otherColliders.Add(other);
                                gameObject.OnCollisionEnter(other);
                            }
                        }
                        else if ((otherColliders.Contains(other) && !UsePixelCollision) || (CollisionBox.Intersects(other.CollisionBox) && (UsePixelCollision && !CheckPixelCollision(other))))
                        {
                            otherColliders.Remove(other);
                            gameObject.OnCollisionExit(other);
                        }
                    }

                }
            }

        }

        private bool CheckPixelCollision(Collider other)
        {
            // Find the bounds of the rectangle intersection
            int top = Math.Max(CollisionBox.Top, other.CollisionBox.Top);
            int bottom = Math.Min(CollisionBox.Bottom, other.CollisionBox.Bottom);
            int left = Math.Max(CollisionBox.Left, other.CollisionBox.Left);
            int right = Math.Min(CollisionBox.Right, other.CollisionBox.Right);

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    int firstIndex = (x - CollisionBox.Left) + (y - CollisionBox.Top) * CollisionBox.Width;
                    int secondIndex = (x - other.CollisionBox.Left) + (y - other.CollisionBox.Top) * other.CollisionBox.Width;

                    //Get the color of both pixels at this point 
                    Color colorA = CurrentPixels[firstIndex];
                    Color colorB = other.CurrentPixels[secondIndex];

                    // If both pixels are not completely transparent
                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        //Then an intersection has been found
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
