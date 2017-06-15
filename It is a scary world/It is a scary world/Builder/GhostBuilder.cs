﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace It_is_a_scary_world
{
    class GhostBuilder : IBuilder
    {
        private GameObject gameObject;

        public void BuildGameObject(Vector2 position)
        {
            gameObject = new GameObject();

            gameObject.AddComponent(new SpriteRenderer(gameObject, "GhostSheet", 1));

            gameObject.AddComponent(new Animator(gameObject));

            gameObject.AddComponent(new Ghost(gameObject));

            gameObject.AddComponent(new Collider(gameObject) { UsePixelCollision = true });

            gameObject.transform.position = position;
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
