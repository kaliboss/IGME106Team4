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
 * Object Base Class
 */
namespace HW2_Stone_Austin
{
    class GameObject
    {
        // variable for storing texture
        Texture2D texture;

        // variable for position
        Rectangle position;

        // constructor
        public GameObject(int x, int y, int width, int height)
        {
            position = new Rectangle(x, y, width, height);
        }

        // accessors for returning width and height of object for use later
        public virtual int Width
        {
            get
            {
                return position.Width;
            }

            set
            {
                position.Width = value;
            }
        }

        public virtual int Height
        {
            get
            {
                return position.Height;
            }

            set
            {
                position.Height = value;
            }
        }

        // accessor for texture
        public virtual Texture2D Texture
        {
            get
            {
                return texture;
            }

            set
            {
                texture = value;
            }
        }

        // accessor for rectangle
        public virtual Rectangle Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        // accessors for X and Y of texture rectangle
        public virtual int X
        {
            get
            {
                return position.X;
            }

            set
            {
                position.X = value;
            }
        }

        public virtual int Y
        {
            get
            {
                return position.Y;
            }

            set
            {
                position.Y = value;
            }
        }
        // use this method to set the start values of the object being created
        protected virtual void Initalize(Texture2D tex, Rectangle pos)
        {
            // default values

            texture = tex;

            position = pos;

            
        }



        // this method will be used to store draw data for our object 
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
