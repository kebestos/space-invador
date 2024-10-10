using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace SpaceInvaders
{
    class Game
    {
        #region GameObjects management
        /// <summary>
        /// Set of all game objects currently in the game
        /// </summary>
        public HashSet<GameObject> gameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Set of new game objects scheduled for addition to the game
        /// </summary>
        private HashSet<GameObject> pendingNewGameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Schedule a new object for addition in the game.
        /// The new object will be added at the beginning of the next update loop
        /// </summary>
        /// <param name="gameObject">object to add</param>
        public void AddNewGameObject(GameObject gameObject)
        {
            pendingNewGameObjects.Add(gameObject);
        }
        #endregion

        #region game technical elements
        /// <summary>
        /// Size of the game area
        /// </summary>
        public Size gameSize;

        /// <summary>
        /// State of the keyboard
        /// </summary>
        public HashSet<Keys> keyPressed = new HashSet<Keys>();

        /// <summary>
        /// enumeration states of the game
        /// </summary>
        public enum GameState { Play, Pause, Win, Lost, Start }

        /// <summary>
        /// Current state of the game
        /// </summary>
        public GameState state;

        /// <summary>
        /// Spaceship of the player
        /// </summary>
        public PlayerSpaceship playerShip;

        /// <summary>
        /// Spaceship of a second player
        /// </summary>
        public PlayerSpaceship2 playerShip2;

        /// <summary>
        /// List of bunkers
        /// </summary>
        List<Bunker> bunkers;

        /// <summary>
        /// enemyblock, contain enemyships
        /// </summary>
        public EnemyBlock enemies;
        #endregion

        #region static fields (helpers)
        /// <summary>
        /// Singleton for easy access
        /// </summary>
        public static Game game { get; private set; }

        /// <summary>
        /// A shared black brush
        /// </summary>
        private static Brush blackBrush = new SolidBrush(Color.Black);

        /// <summary>
        /// A shared simple font
        /// </summary>
        private static Font defaultFont = new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel);
        #endregion

        #region constructors
        /// <summary>
        /// Singleton constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param> 
        /// <returns></returns>
        public static Game CreateGame(Size gameSize)
        {

            if (game == null)
                game = new Game(gameSize);
            return game;
        }

        /// <summary>
        /// Private constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        private Game(Size gameSize)
        {
           
            this.gameSize = gameSize;
            InitialisationGame();

        }

        /// <summary>
        /// Constructeur Initialisation du Jeu 
        /// </summary>
        public void InitialisationGame()
        {
            this.state = GameState.Start;
            this.playerShip = new PlayerSpaceship((this.gameSize.Width / 2) - 75, this.gameSize.Height - 110, 100, SpaceInvaders.Properties.Resources.ship3, GameObject.Side.Ally);
            this.playerShip2 = new PlayerSpaceship2((this.gameSize.Width / 2) + 75, this.gameSize.Height - 110, 100, SpaceInvaders.Properties.Resources.PlayerShip2, GameObject.Side.Ally);
            AddNewGameObject(playerShip);
            AddNewGameObject(playerShip2);
            double ecart = (this.gameSize.Width / 3 - 87) / 2;
            this.bunkers = new List<Bunker>();
            this.bunkers.Add(new Bunker(ecart, this.gameSize.Height - 150.0, GameObject.Side.Neutral));
            this.bunkers.Add(new Bunker(ecart + (this.gameSize.Width / 3), this.gameSize.Height - 150.0, GameObject.Side.Neutral));
            this.bunkers.Add(new Bunker(ecart + 2 * (this.gameSize.Width / 3), this.gameSize.Height - 150.0, GameObject.Side.Neutral));
            foreach (Bunker bunker in bunkers)
            {
                AddNewGameObject(bunker);
            }
            this.enemies = new EnemyBlock(0, 0, GameObject.Side.Enemy);
            AddNewGameObject(enemies);
            enemies.AddLine(5, 1, SpaceInvaders.Properties.Resources.ship4);
            enemies.AddLine(5, 1, SpaceInvaders.Properties.Resources.ship2);
            enemies.AddLine(6, 1, SpaceInvaders.Properties.Resources.ship1);
            enemies.AddLine(6, 1, SpaceInvaders.Properties.Resources.ship7);
            enemies.AddLine(6, 1, SpaceInvaders.Properties.Resources.ship9);

            foreach (SpaceShip enemyShip in enemies.enemmyships)
            {
                AddNewGameObject(enemyShip);
            }
        }
        #endregion

        #region methods
        /// <summary>
        /// Force a given key to be ignored in following updates until the user
        /// explicitily retype it or the system autofires it again.
        /// </summary>
        /// <param name="key">key to ignore</param>
        public void ReleaseKey(Keys key)
        {
            keyPressed.Remove(key);
        }

        #region Draw
        /// <summary>
        /// Draw the whole game
        /// </summary>
        /// <param name="g">Graphics to draw in</param>
        public void Draw(Graphics g)
        {
           
            if (state == GameState.Pause)
            {
                g.DrawString("Pause", defaultFont, blackBrush, (this.gameSize.Width / 2) - 24, (this.gameSize.Height / 2) - 32);
            }
            else if (state == GameState.Start)
            {
                g.DrawString("Appuyer sur espace pour commencer la partie.", defaultFont, blackBrush, (this.gameSize.Width / 2) - 240, (this.gameSize.Height / 2) - 32);
                g.DrawString("Score toutch : enemmie +10 ; Allier -5 ; missile +1", defaultFont, blackBrush, (this.gameSize.Width / 2) - 240, (this.gameSize.Height / 2) - 10);
            }
            else if (state == GameState.Win)
            {
                gameObjects.RemoveWhere(gameObject => gameObject.IsAlive());
                g.DrawString("Gagné ! Score = "+GameObject.Score, defaultFont, blackBrush, (this.gameSize.Width / 2) - 115, (this.gameSize.Height / 2) - 32);
                g.DrawString("Appuyer sur espace pour recommencer", defaultFont, blackBrush, (this.gameSize.Width / 2) - 230, (this.gameSize.Height / 2) - 10);
            }
            else if (state == GameState.Lost)
            {
                gameObjects.RemoveWhere(gameObject => gameObject.IsAlive());
                g.DrawString("Perdu !", defaultFont, blackBrush, (this.gameSize.Width / 2) - 24, (this.gameSize.Height / 2) - 32);
                g.DrawString("Appuyer sur espace pour recommencer", defaultFont, blackBrush, (this.gameSize.Width / 2) - 230, (this.gameSize.Height / 2) - 10);
            }
            else if (state == GameState.Play)
            {
                float positionY1 = (float)playerShip.Position.y ;
                float positionX2 = (float)gameSize.Width;
                Pen pen = new Pen(Color.Aqua);
               // g.DrawLine(pen, 0, positionY1, positionX2, positionY1);
                foreach (GameObject gameObject in gameObjects)
                    gameObject.Draw(this, g);
            }

        }
        #endregion

        #region Update
        /// <summary>
        /// Manage states of the game, his initialisation and the remove of objects of the game
        /// </summary>
        public void Update(double deltaT)
        {
            gameObjects.UnionWith(pendingNewGameObjects);
            pendingNewGameObjects.Clear();

            // remove dead objects
            gameObjects.RemoveWhere(gameObject => !gameObject.IsAlive());

            if (state == GameState.Play)
            {
                foreach (GameObject gameObject in gameObjects)
                {
                    gameObject.Update(this, deltaT);
                }
                if (keyPressed.Contains(Keys.P))
                {
                    state = GameState.Pause;
                    ReleaseKey(Keys.P);
                }
            }

            if (state == GameState.Pause && keyPressed.Contains(Keys.P))
            {
                state = GameState.Play;
                ReleaseKey(Keys.P);
            }

            else if (state == GameState.Start)
            {
                if (keyPressed.Contains(Keys.Space))
                {
                    state = GameState.Play;
                }
            }

            if (state == GameState.Win || state == GameState.Lost)
            {
                if (keyPressed.Contains(Keys.Space))
                {
                    InitialisationGame();
                    GameObject.Score = 0;
                }
            }

            if (playerShip.IsAlive() == false || enemies.Position.Y + enemies.Size.Height >= playerShip.Position.Y-75)
            {
                state = GameState.Lost;
            }

            int nbShips = this.enemies.enemmyships.Count();
            if (nbShips== 0)
            { 
                state = GameState.Win;
            }   
        }
        #endregion
        #endregion
    }
}
