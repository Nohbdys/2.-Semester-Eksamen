using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace It_is_a_scary_world
{
    class ShopBuilder : IBuilder
    {
        private GameObject gameObject;

        public void BuildGameObject(Vector2 position)
        {
            gameObject = new GameObject();

            gameObject.AddComponent(new SpriteRenderer(gameObject, "Shopkeeper", 1));

            gameObject.AddComponent(new Animator(gameObject));

            gameObject.AddComponent(new Shop(gameObject, gameObject.transform));

            gameObject.AddComponent(new Collider(gameObject));

            gameObject.transform.position = position;

            gameObject.Tag = "Shop";
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
