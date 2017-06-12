using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace It_is_a_scary_world
{
    class WallBuilder : IBuilder
    {
        private GameObject gameObject;

        public void BuildGameObject(Vector2 position)
        {
            gameObject = new GameObject();

            gameObject.AddComponent(new SpriteRenderer(gameObject, "Wall", 1));

            gameObject.AddComponent(new Animator(gameObject));

            gameObject.AddComponent(new Wall(gameObject));

            gameObject.AddComponent(new Collider(gameObject) { UsePixelCollision = false, DoCollisionChecks = false });

            gameObject.transform.position = position;
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}

