using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders
{
    class Missile : SimpleObject
    {
        #region Propertie
        /// <summary>
        /// Speed of Missile
        /// </summary>
        public double Vitesse { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor of Missile
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="lives"></param>
        /// <param name="image"></param>
        /// <param name="s"></param>
        public Missile(double x, double y, int lives, Bitmap image, Side s) : base(x, y, lives, image, s)
        {
            this.Vitesse = 500.0;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Move the missile 
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <param name="deltaT"></param>
        public override void Update(Game gameInstance, double deltaT)
        {
            #region deplacement

            if (SideObjet == Side.Ally)
            {
                Position.Y -= Vitesse * deltaT;
            }
            if (SideObjet == Side.Enemy)
            {
                Position.Y += Vitesse * deltaT;
            }
            if (Position.Y <= 0 || Position.Y >= gameInstance.gameSize.Height)
            {
                Lives = 0;
            }

            #endregion

            foreach (GameObject gameObject in gameInstance.gameObjects)
            {
                gameObject.Collision(this);
            }
        }

        /// <summary>
        /// Collision between two missile
        /// </summary>
        /// <param name="m"></param>
        protected override void OnCollision(Missile m)
        {
            //Retire la vie des deux missiles
            m.Lives = 0;
            Lives = 0;
            Score += 1;
        }
        #endregion
        }
}