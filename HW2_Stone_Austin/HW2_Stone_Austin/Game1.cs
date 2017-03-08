using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
/*
 * Austin Stone
 * Section 2
 * HW2
 */
namespace HW2_Stone_Austin
{
    enum GameState { Menu, Game, GameOver };
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D sprite;
        Texture2D pickupSprite;
        SpriteFont font;
        Player player;
        List<Collectible> collectible;
        int level;
        double time;
        Random rng;
        
        GameState gameState;
        Boolean[] wasd = { false, false, false, false };
        string[] wasdStr = { "W", "A", "S", "D" };
        KeyboardState kbState;
        KeyboardState previousKbState;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

       
        
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player = new Player(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2, 100, 100);
            gameState = GameState.Menu;
            collectible = new List<Collectible>();
            rng = new Random();
            kbState = Keyboard.GetState();
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            sprite = Content.Load<Texture2D>("characterSprite");
            pickupSprite = Content.Load<Texture2D>("enemySprite");
            font = Content.Load<SpriteFont>("mainFont");
            player.Texture = sprite;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            

            // gets new keyborad state
            kbState = Keyboard.GetState();
            // finite state machine checks
            if (gameState == GameState.Menu)
            {
                
                if (SingleKeyPress(Keys.Enter))
                {
                    
                    gameState = GameState.Game;
                    ResetGame();
                }
            }

            else if(gameState == GameState.Game)
            {
                

                time -= gameTime.ElapsedGameTime.TotalSeconds;

                // if statements to check if each of the movement keys are being pressed
                if (kbState.IsKeyDown(Keys.W))
                {
                    wasd[0] = true;
                    player.Position = new Rectangle(player.X, player.Y - 5, player.Width, player.Height);
                }

                else
                {
                    wasd[0] = false;
                }

                if (kbState.IsKeyDown(Keys.A))
                {
                    wasd[1] = true;
                    player.Position = new Rectangle(player.X - 5, player.Y, player.Width, player.Height);
                }

                else
                {
                    wasd[1] = false;
                }

                if (kbState.IsKeyDown(Keys.S))
                {
                    wasd[2] = true;
                    player.Position = new Rectangle(player.X, player.Y + 5, player.Width, player.Height);
                }

                else
                {
                    wasd[2] = false;
                }

                if (kbState.IsKeyDown(Keys.D))
                {
                    wasd[3] = true;
                    player.Position = new Rectangle(player.X + 5, player.Y, player.Width, player.Height);
                }

                else
                {
                    wasd[3] = false;
                }

                ScreenWrap(player);

                foreach(Collectible col in collectible)
                {
                    bool collision = col.CheckCollision(player);
                    if (collision)
                    {
                        col.Active = false;
                        player.LevelScore += 1;
                        player.TotalScore += 1;
                    }
                    
                    
                }

               if(time < 0)
                {
                    gameState = GameState.GameOver;
                }

                if(player.LevelScore == collectible.Count)
                {
                    NextLevel();
                }

            }

            else
            {
                if (SingleKeyPress(Keys.Enter))
                {
                    gameState = GameState.Menu;
                    
                }
            }

            // stores old keyboard state for check
            previousKbState = kbState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            // check game state and draw what is needed in each
            if (gameState == GameState.Menu)
            {
                spriteBatch.DrawString(font, "Homework Two Electric Boogaloo", new Vector2(250f, 10f), Color.Black);
                spriteBatch.DrawString(font, "Press Enter to continue and move with WASD", new Vector2(200f, 400f), Color.Black);
            }

            else if (gameState == GameState.Game)
            {
                foreach(Collectible coll in collectible)
                {
                    coll.Draw(spriteBatch);
                }
                
                spriteBatch.Draw(player.Texture, player.Position, Color.White);
                spriteBatch.DrawString(font, "Level: " + level, new Vector2(10, 10), Color.Black);
                spriteBatch.DrawString(font, string.Format("Time: {0:0.00}", time), new Vector2(200, 10), Color.Black);
                spriteBatch.DrawString(font, "Score: " + player.LevelScore, new Vector2(400, 10), Color.Black);

            }

            else
            {
                spriteBatch.DrawString(font, "Game Over", new Vector2(250f, 10f), Color.Black);
                spriteBatch.DrawString(font, String.Format("{0:0.00}", level), new Vector2(250f, 100f), Color.Black);
                spriteBatch.DrawString(font, String.Format("{0:0.00}", player.TotalScore), new Vector2(250f, 200f), Color.Black);
                spriteBatch.DrawString(font, "Press Enter to continue", new Vector2(250f, 400f), Color.Black);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        // Method for setting up the next level for the player
        void NextLevel()
        {
            // variable for storing the amount of collectibles to be made
            int collectNum;

            // sets all the variables created above
            level += 1;
            time = 10.00;
            player.LevelScore = 0;
            player.X = GraphicsDevice.Viewport.Width / 2;
            player.Y = GraphicsDevice.Viewport.Height / 2;
            collectible.Clear();
            collectNum = 5 + 3 * level;

            // loop for creating the collectibles and their positions
            for (int i = 0; i < collectNum; i++)
            {
                int X = rng.Next(0, GraphicsDevice.Viewport.Width + 1);
                int Y = rng.Next(0, GraphicsDevice.Viewport.Height + 1);
                Collectible collect = new Collectible(X, Y, 50, 50);
                collect.Texture = pickupSprite;
                collectible.Add(collect);
            }

        }

        // method for resetting the game when switching states
        void ResetGame()
        {
            level = 0;
            player.TotalScore = 0;
            NextLevel();
        }

        // method to keep player from going off screen
        void ScreenWrap(GameObject game)
        {
            if(game.X > GraphicsDevice.Viewport.Width)
            {
                game.X = 0;
            }

            else if (game.X < 0)
            {
                game.X = GraphicsDevice.Viewport.Width;
            }

            if(game.Y > GraphicsDevice.Viewport.Height)
            {
                game.Y = 0;
            }
            
            else if(game.Y < 0)
            {
                game.Y = GraphicsDevice.Viewport.Height;
            }
        }

        // method for checking for a single key press
        bool SingleKeyPress(Keys key)
        {
            if(kbState.IsKeyDown(key) && previousKbState.IsKeyUp(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
