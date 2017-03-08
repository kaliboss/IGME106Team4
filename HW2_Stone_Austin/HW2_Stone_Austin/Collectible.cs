using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
/*
 * Austin Stone
 * Section 2
 * Collectible Class
 */
namespace HW2_Stone_Austin
{
    class Collectible : GameObject
    {

        // variable for collectible state
        bool active;

        // accessor for active
        public bool Active
        {
            get
            {
                return active;
            }

            set
            {
                active = value;
            }
        }
        public Collectible(int x, int y, int width, int height) : base(x, y, width, height)
        {
            active = true;
        }

        public bool CheckCollision(GameObject game)
        {
            if (Active)
            {
                return game.Position.Intersects(this.Position);
            }
            else
            {
                return false;
            }
        }

        // override for the GameObject Draw method
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                base.Draw(spriteBatch);
            }
        }
    }
}
