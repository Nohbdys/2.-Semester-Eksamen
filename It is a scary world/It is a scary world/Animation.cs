using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace It_is_a_scary_world
{
    public class Animation
    {
        /// <summary>
        /// This animations fps, higher fps means faster animations
        /// </summary>
        public float FPS { get; private set; }

        /// <summary>
        /// The rectangles associated with this animation
        /// </summary>
        public Rectangle[] Rectangles { get; private set; }

        /// <summary>
        /// The offset for this animation
        /// </summary>
        public Vector2 Offset { get; private set; }

        /// <summary>
        /// Contains all colors for pixel collision
        /// </summary>
        public Color[][] Colors { get; private set; }

        public int MyFrames { get; private set; }

        /// <summary>
        /// The animations constructor
        /// </summary>
        /// <param name="frames">Amount of frames</param>
        /// <param name="yPos">The y position of the topleft corner of the sprite on the sprite sheet in pixels</param>
        /// <param name="xStartFrame">The frame number from left to right on the sprite sheet, first frame is index 0</param>
        /// <param name="width">The width of each frame</param>
        /// <param name="height">The hight of each frame</param>
        /// <param name="FPS">The fps for this animation</param>
        /// <param name="offset">The offset for this animation</param>
        public Animation(int frames, int yPos, int xStartFrame, int width, int height, float FPS, Vector2 offset, Texture2D sprite)
        {
            Rectangles = new Rectangle[frames];

            Offset = offset;

            this.FPS = FPS;

            this.MyFrames = frames;

            for (int i = 0; i < frames; i++) //Creates the rectangles based on the parameters
            {
                Rectangles[i] = new Rectangle((i + xStartFrame) * width, yPos, width, height);
            }


        }
    }
}
