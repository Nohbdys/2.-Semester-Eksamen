using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace It_is_a_scary_world
{
    public class SpriteRenderer : Component, ILoadable, IDrawable
    {
        public Rectangle Rectangle { get; set; }

        public Texture2D Sprite { get; set; }

        public Vector2 Offset { get; set; }

        public Color Color { get; set; } = Color.White;

        private string spriteName;

        private float layerDepth;


        public SpriteRenderer(GameObject gameObject, string spriteName, float layerDepth) : base(gameObject)
        {
            this.spriteName = spriteName;
            this.layerDepth = layerDepth;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, gameObject.transform.position + Offset, Rectangle, Color, 0, Vector2.Zero, 1, SpriteEffects.None, layerDepth);

        }

        public void LoadContent(ContentManager content)
        {
            Sprite = content.Load<Texture2D>(spriteName);

            this.Rectangle = new Rectangle(0, 0, Sprite.Width, Sprite.Height);
        }

    }
}
