using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders
{
    class EnemyBlock : GameObject
    {
        #region fields and Properties
        /// <summary>
        /// Colection of all enemyship, enemyblock 
        /// </summary>
        public HashSet<SpaceShip> enemmyships;

        /// <summary>
        /// Base width of enemyblock
        /// </summary>
        private int basewidth;

        /// <summary>
        /// Size of enemyblock
        /// </summary>
        private Size size;

        /// <summary>
        /// Propertie of Size
        /// </summary>
        public Size Size { get; set; }

        /// <summary>
        /// Live Number of enemyblock
        /// </summary>
        private int Lives;
        
        /// <summary>
        /// Probability Shoot of an enemyship
        /// </summary>
        private double randomShootProbability=1;

        /// <summary>
        /// Vector position of enemyblock
        /// </summary>
        public Vecteur2D Position { get; set; }

        /// <summary>
        /// Speed of enemyblock and enemyships
        /// </summary>
        protected double enemmyspeed;

        /// <summary>
        /// Dimensions of the enemyBlock
        /// </summary>
        private double positionXlaPluspetite;
        private double positionXlaPlusgrande;
        private double positionYlaPlusgrande;
        private double positionYlaPluspetite;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor of enemyblock
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="s"></param>
        public EnemyBlock(double x, double y, Side s) : base(s)
        {
            this.Position = new Vecteur2D(x, y);
            this.basewidth = 300;
            this.enemmyships = new HashSet<SpaceShip>();
            this.Size = new Size(basewidth, 0);
            this.enemmyspeed = 100.0;
            this.Lives = 1;
        }
        #endregion

        #region Methods

        #region AddLine
        /// <summary>
        /// Add a Line of enemyblock
        /// </summary>
        /// <param name="nbShips"></param>
        /// <param name="nbLives"></param>
        /// <param name="shipImage"></param>
        public void AddLine(int nbShips, int nbLives, Bitmap shipImage)
        {
            double positionx = Position.X;
            double taillecase = basewidth / nbShips;
            for (int i = nbShips; i != 0; i--)
            {
                SpaceShip NewSpaceship = new SpaceShip(positionx + (taillecase / 2) - (shipImage.Width / 2), size.Height, nbLives, shipImage,Side.Enemy);
                positionx += taillecase;
                enemmyships.Add(NewSpaceship);
            }
            size.Height += shipImage.Height;
        }
        #endregion

        #region UpdateSize
        /// <summary>
        /// Update the size of enemyblock in function of enemyships alive
        /// </summary>
        public void UpdateSize()
         {
                 positionXlaPluspetite = enemmyships.First().Position.X;
                 positionXlaPlusgrande = enemmyships.First().Position.x + enemmyships.First().Image.Width;
                 positionYlaPlusgrande = enemmyships.First().Position.y + enemmyships.First().Image.Height;
                 positionYlaPluspetite = enemmyships.First().Position.y;
             foreach (SpaceShip enemmyship in enemmyships)
             {
                 double PosMaxXEnemyship = enemmyship.Position.X + enemmyship.Image.Width;
                 double PosMaxYEnemyship = enemmyship.Position.Y + enemmyship.Image.Height;
                 double PosMinXEnemyship = enemmyship.Position.X;
                 double PosMinYEnemyship = enemmyship.Position.Y;
                 if (PosMaxXEnemyship > positionXlaPlusgrande)
                 {
                     positionXlaPlusgrande = PosMaxXEnemyship;
                 }
                 if (PosMaxYEnemyship > positionYlaPlusgrande)
                 {
                     positionYlaPlusgrande = PosMaxYEnemyship;
                 }
                 if (PosMinXEnemyship < positionXlaPluspetite)
                 {
                     positionXlaPluspetite = PosMinXEnemyship;
                 }
                 if (PosMinYEnemyship < positionYlaPluspetite)
                 {
                     positionYlaPluspetite = PosMinYEnemyship;
                 }
                 
                size.Height = (int) (positionYlaPlusgrande - positionYlaPluspetite);
                 size.Width = (int) (positionXlaPlusgrande - positionXlaPluspetite);
                 Position.X = positionXlaPluspetite;
                 Position.Y = positionYlaPluspetite;
             
             }
         }
        #endregion

        #region Update
        /// <summary>
        /// Move enemyblock and enemyships 
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <param name="deltaT"></param>
        public override void Update(Game gameInstance, double deltaT)
        {
            if (enemmyships.Count == 0)
            {
                Lives = 0;
            }
            if (Position.X + size.Width >= gameInstance.gameSize.Width)
            {
                Position.X = gameInstance.gameSize.Height - size.Width - 10;
            }
            else if (Position.X <= 0){Position.X = 10;}
            foreach (SpaceShip enemmyship in enemmyships)
            {
                enemmyship.Position.X += enemmyspeed * deltaT;
            }
            Position.X += enemmyspeed  * deltaT;
            if (Position.X + size.Width >= gameInstance.gameSize.Width || Position.X <= 0)
            {
                enemmyspeed = -enemmyspeed;
                if (enemmyspeed > 0){enemmyspeed += 05.0;}
                else{enemmyspeed -= 07.0;}
                Position.Y += 20;
                foreach (SpaceShip enemmyship in enemmyships)
                {
                    enemmyship.Position.Y += 20;
                }
                randomShootProbability += 0.1;
            }
            UpdateSize();
            EnemyShoot(gameInstance,deltaT);
        }
        #endregion

        #region Draw
        /// <summary>
        /// Draw all enemyships in enemyblock
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <param name="g"></param>
        public override void Draw(Game gameInstance, Graphics g)
        {
            foreach (SpaceShip enemmyship in enemmyships)
            {
                float positionX = (float)enemmyship.Position.X;
                float positionY = (float)enemmyship.Position.y;
                g.DrawImage(enemmyship.Image, positionX, positionY, enemmyship.Image.Width, enemmyship.Image.Height);
                Pen pen1 = new Pen(Color.Aqua);
               // g.DrawRectangle(pen1, positionX, positionY, enemmyship.Image.Width, enemmyship.Image.Height);
            }
            float positionX2 = (float)Position.X;
            float positionY2 = (float)Position.y;
            Pen pen = new Pen(Color.Aqua);
          //  g.DrawRectangle(pen, positionX2, positionY2, size.Width, size.Height);
            g.DrawString("Score : " + Score, defaultFont, Brushes.Black, Game.game.gameSize.Width - 100, Game.game.gameSize.Height - 20);
        }
        #endregion

        #region IsAlive
        /// <summary>
        /// Check if the enemyblock is alive
        /// </summary>
        /// <returns></returns>
        public override bool IsAlive()
        {
            if (Lives <= 0){return false;}
            return true;
        }
        #endregion

        #region Collision
        /// <summary>
        /// Collision between an enemyship and a missile
        /// </summary>
        /// <param name="m"></param>
        public override void Collision(Missile m)
        {
            //Browse all enemyship of the enemyBlock
                for (int i=0;i < enemmyships.Count; i++)
                {
                    //Check if the collision is between objects of different side
                    if (m.SideObjet != enemmyships.ElementAt(i).SideObjet)
                    {
                        //if the missil is in collision with an enemyship
                        if (m.Position.X > enemmyships.ElementAt(i).Position.X &&
                            m.Position.X < enemmyships.ElementAt(i).Position.X + enemmyships.ElementAt(i).Image.Width
                            && m.Position.Y > enemmyships.ElementAt(i).Position.Y &&
                            m.Position.Y < enemmyships.ElementAt(i).Position.Y + enemmyships.ElementAt(i).Image.Height)
                        {
                            //Remove a live of the enemyship
                            enemmyships.ElementAt(i).Lives -= 1;
                            //if the enemyship lives is zeros, the enemyship is remove
                            if (enemmyships.ElementAt(i).Lives <= 0)
                            {
                                enemmyships.Remove(enemmyships.ElementAt(i));
                                Score += 10;
                            }
                            //Remove Lives of missile
                            m.Lives = 0;
                        }
                    }
                }   
        }
        #endregion

        #region EnemyShoot
        /// <summary>
        /// Methods of randomshoot of enemyships
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <param name="deltaT"></param>
        public void EnemyShoot(Game gameInstance, double deltaT)
        {
            Random alea = new Random();
            double r;
            foreach (SpaceShip enemmyship in enemmyships)
            {
                r = alea.NextDouble();
                if (r <= randomShootProbability * deltaT)
                {enemmyship.Shoot(gameInstance);}
            }
        }
        #endregion
        #endregion
    }
}