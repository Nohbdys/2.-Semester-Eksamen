using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace It_is_a_scary_world
{
    class PlayerBuilder : IBuilder
    {
        private GameObject gameObject;

        public void BuildGameObject(Vector2 position)
        {
            gameObject = new GameObject();

            gameObject.AddComponent(new SpriteRenderer(gameObject, "HeroSheet", 1));

            gameObject.AddComponent(new Animator(gameObject));

            gameObject.AddComponent(new Player(gameObject));

            gameObject.AddComponent(new Collider(gameObject));

            //gameObject.AddComponent(new Gravity(gameObject.transform, gameObject) { isFalling = true });

            gameObject.transform.position = position;

            gameObject.Tag = "Player";
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
