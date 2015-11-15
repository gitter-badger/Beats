using Beats.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beats.Scenes
{
    /// <summary>
	/// Represents a scene. A scene has a list of sprites it will draw, can be reset and transition to other scenes.
	/// </summary>
    class Scene
    {
        /// <summary>
        /// The list of sprites the scene uses.
        /// </summary>
        private Sprite[] spriteList;

        /// <summary>
        /// Constructor for the Scene class. Creates an empty list of Sprites with the specified size.
        /// </summary>
        /// <param name="n">The size of the sprite list.</param>
        public Scene(int n)
        {
            spriteList = new Sprite[n];
        }

        /// <summary>
        /// Draws the scene.
        /// </summary>
        public void draw()
        {
        }

        /// <summary>
        /// Resets the scene back to a new one.
        /// </summary>
        public void reset()
        {
            spriteList = null;
        }
    }
}
