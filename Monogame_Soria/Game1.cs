using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using monogame_soria;
using System;
using System.Collections.Generic;
using System.IO;

namespace Monogame_Soria
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        //Propieades de la clase Game1
        public GraphicsDeviceManager graphics { get; private set; }
        public SpriteBatch spriteBatch { get; private set; }
        public static Game1 Instance { get; private set; }
        public enum States { Play, Paused, Menu, Ranking, GameOver, Finish }
        public States State { get; set; }

        // Listas de Sprites
       
        internal List<Updatable> SpritesMenu { get; private set; }
        internal List<Updatable> SpritesActive { get; private set; }
        internal List<Updatable> SpritesInactive { get; private set; }
        internal List<Updatable> BossesSpritesActive { get; private set; }
        internal List<Updatable> BossesSpritesInactive { get; private set; }
        internal List<String> rankingScores;

        
        //Objetos del Juego
        Level level;

        Boss boss1; 
        public int levelcurrentimage;
        public int score;
        Song gameSong, gameBoss1Song;
        Song boss1song;
        //Tamaño de la Pantalla
        int width;
        int height;

        //Obtengo la direccion del Escritorio
        public string mydesk = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Instancio
            Instance = this;
            SpritesMenu = new List<Updatable>();
            SpritesActive = new List<Updatable>();
            SpritesInactive = new List<Updatable>();
            BossesSpritesActive = new List<Updatable>();
            BossesSpritesInactive = new List<Updatable>();
            State = States.Menu;

        
            //Ajusto el formulario
            width = 700; 
            height = 600; 
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
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

            //Cargar Puntajes Guardados o Crear el file por primera vez
         
            #region Score
            rankingScores = new List<String>();
            String line;
            string path = (mydesk + "\\monogame_Soria_scores.txt");

            if (File.Exists(path))
            {
                StreamReader sr = new StreamReader(path);

                line = sr.ReadLine();

                while (line != null)
                {
                    //write the lie to console window
                    rankingScores.Add(line);
                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
            }
            else
            {
                File.Create(path);
            }
        #endregion

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.boss1
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //Cargo los Elementos del Menu   
            SpritesMenu.Add(new Menu(Point.Zero, new Point(width, height)));
            SpritesMenu.Add(new MenuSelect(new Point(70, 190), new Point(33, 32)));
                        
            //Cargo los Elementos del Level
            level = new Level(width, height, 3, 0, 0);
            
            SpritesActive.Add(level);
            SpritesActive.Add(new Player(new Point(150, 300),  new Point(70, 69), 10));
            SpritesActive.Add(new EnemyFabric());
            SpritesActive.Add(new StoneFactory());
            boss1 = new Boss(Point.Zero, 3, 3);
            BossesSpritesActive.Add(boss1);

            //Cargo la Musica del Juego
            gameSong = Content.Load<Song>("sounds/Opening");
            gameBoss1Song = Content.Load<Song>("sounds/Boss1");
            boss1song = Content.Load<Song>("sounds/Boss1");
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here

        }
        bool firstRun = true;
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


            if (firstRun)
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(gameSong);
                MediaPlayer.Volume = 0.5f;
                firstRun = false;
                MediaPlayer.IsRepeating = true;
            }

            if (State == States.Menu || State == States.Ranking ||
                State == States.GameOver || State == States.Finish)
            {
              
                foreach (var item in SpritesMenu)
                {
                    item.Update(gameTime);
                }
            }

            if (State == States.Play)
            {

                levelcurrentimage = level.CurrentImage;
                

                //Si se llego al final del Level, ejecutar la logica de Boss
                if (levelcurrentimage == 0)
                {
                             
                   // MediaPlayer.Play(boss1song);

                    
                    foreach (var item in BossesSpritesActive)
                    {
                        
                        item.Update(gameTime);

                    }

                    foreach (var item in BossesSpritesInactive)
                    {
                        if (BossesSpritesActive.Contains(item))
                            BossesSpritesActive.Remove(item);
                        else
                           
                        BossesSpritesActive.Add(item);
                    }
                    BossesSpritesInactive.Clear();

                }
                

                foreach (var item in SpritesActive)
                {
                    item.Update(gameTime);
                }

                foreach (var item in SpritesInactive)
                {
                    if (SpritesActive.Contains(item))
                        SpritesActive.Remove(item);
                    else
                        SpritesActive.Add(item);
                }
                SpritesInactive.Clear();
            }

            
            base.Update(gameTime);

        }

        private void CheckEnemyImpacts(GameTime gameTime)
        {

                        //Crear animacion y sonido de impacto
                        //AddExplosion(player.X, player.Y);

        }

        private void CheckCollisions(GameTime gameTime)
        {

                    //generar explosion y eliminar enemigo
                    //AddExplosion(enemies[i].Rectangle.X, enemies[i].Rectangle.Y);
        }

        private void AddExplosion(int x, int y)
        {
            //Animation explosion = new Animation();
            //explosion.Initialize(explosionTexture, new Vector2(x, y), 60, 59, 3, 80, Color.White, 2f, false);
            //explosions.Add(explosion);
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

            if (State == States.Menu || State == States.Ranking ||
                State == States.GameOver || State == States.Finish)
            {
                foreach (var item in SpritesMenu)
                {
                    Sprite aux = item as Sprite;
                    if (aux != null)
                        aux.Draw(gameTime);
                }
            }
            
            if (State == States.Play)
            {
                foreach (var item in SpritesActive)
                {
                    BackgroundSprite aux = item as BackgroundSprite;
                    if (aux != null)
                        aux.Draw(gameTime);
                }

                foreach (var item in SpritesActive)
                {
                    Sprite aux = item as Sprite;
                    if (aux != null)
                        aux.Draw(gameTime);
                }

                if (levelcurrentimage == 0)
                {
                    foreach (var item in BossesSpritesActive)
                    {
                        Sprite aux = item as Sprite;
                        if (aux != null)
                            aux.Draw(gameTime);
                    }
                }
                
            }

            if (State== States.GameOver || State == States.Finish)
            {


                foreach (var item in SpritesMenu)
                {
                    Sprite aux = item as Sprite;
                    if (aux != null)
                        aux.Draw(gameTime);
                }
                levelcurrentimage = -1;

                levelcurrentimage = level.CurrentImage;
                level = new Level(width, height, 3, 0, 0);
                BossesSpritesActive.Clear();
                SpritesActive.Clear();
                SpritesActive.Add(level);
                SpritesActive.Add(new StoneFactory());
                SpritesActive.Add(new EnemyFabric());
                boss1 = new Boss(Point.Zero, 3, 3);
                BossesSpritesActive.Add(boss1);

               
                if (levelcurrentimage == 0)
                {
                    foreach (var item in BossesSpritesActive)
                    {
                        Sprite aux = item as Sprite;
                        if (aux != null)
                            aux.Draw(gameTime);
                    }
                }

               SpritesActive.Add(new Player(new Point(150, 300), new Point(70, 69), 10));
                                
            }
            
                spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
