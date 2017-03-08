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
 * Player Class
 */
namespace HW2_Stone_Austin
{
    class Player : GameObject
    {
        // variables for texture and position
        

        // variables for score
        int levelScore;
        int totalScore;

        public Player(int x, int y, int width, int height) : base(x, y, width, height)
        {
            
            levelScore = 0;
            totalScore = 0;
        }
        
        // accessors for score
        public int LevelScore
        {
            get
            {
                return levelScore;
            }

            set
            {
                levelScore = value;
            }
        }

        public int TotalScore
        {
            get
            {
                return totalScore;
            }

            set
            {
                totalScore = value;
            }
        }

    }
}
