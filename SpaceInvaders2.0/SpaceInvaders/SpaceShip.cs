using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;

namespace SpaceInvaders
{
    class SpaceShip : SimpleObject
    {
        #region fields and properties 

        /// <summary>
        /// Speed of Spaceship
        /// </summary>
        protected double speedPixelPerSecond;

        /// <summary>
        /// Missile of the Player
        /// </summary>
        public Missile missile;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of SpaceShip
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="lives"></param>
        /// <param name="image"></param>
        public SpaceShip(double x, double y, int lives, Bitmap image, Side s) : base(x, y, lives, image, s)
        {
            this.speedPixelPerSecond = 200.0;
        }

        #endregion

        #region Methods 

        /// <summary>
        /// Do nothing
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <param name="deltaT"></param>
        public override void Update(Game gameInstance, double deltaT)
        {
        }

        /// <summary>
        /// Methods of shoot of the spaceship, create a missile of the same side of the spaceship
        /// </summary>
        /// <param name="gameInstance"></param>
        public void Shoot(Game gameInstance)
        {
            if (missile == null || missile.Lives == 0)
            {
                if (this.SideObjet == Side.Ally)
                {
                    missile = new Missile(Position.X + (Image.Width / 2), Position.Y, 20,
                        SpaceInvaders.Properties.Resources.shoot1, Side.Ally);
                    gameInstance.AddNewGameObject(missile);
                }
                if (this.SideObjet == Side.Enemy)
                {
                    missile = new Missile(Position.X + (Image.Width / 2), Position.Y, 20,
                        SpaceInvaders.Properties.Resources.shoot1, Side.Enemy);
                    gameInstance.AddNewGameObject(missile);
                }
            }
        }

        /// <summary>
        /// Collision between the spaceship and a missile
        /// </summary>
        /// <param name="m"></param>
        protected override void OnCollision(Missile m)
        {
            //Remove lives of the missile
            m.Lives = 0;
            //Remove a live of spaceship
            Lives -= 1;
            if (Score > 0)
            {
                Score -= 5;
            }
            
        }

        #endregion
    }
}