using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace It_is_a_scary_world
{
    public class Transform : Component
    {
        /// <summary>
        /// The transform's position
        /// </summary>
        public Vector2 position { get; set; }

        /// <summary>
        /// The constructor of the transform
        /// </summary>
        /// <param name="gameObject">Parent object</param>
        /// <param name="position">The transform's position</param>
        public Transform(GameObject gameObject, Vector2 position) : base(gameObject)
        {
            this.position = position;
        }

        public void Translate(Vector2 translation)
        {
            position += translation;
        }
    }
}
