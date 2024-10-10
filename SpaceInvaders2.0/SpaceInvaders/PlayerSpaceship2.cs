using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace SpaceInvaders
{
    class PlayerSpaceship2 : SpaceShip
    {
        #region Constructor
        /// <summary>
        /// Constructor of PlayerSpaceship
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="lives"></param>
        /// <param name="image"></param>
        /// <param name="s"></param>
        public PlayerSpaceship2(double x, double y, int lives, Bitmap image, Side s) : base(x, y, lives, image, s) { }
        #endregion

        #region Methods
        /// <summary>
        /// Move of the PlayerShip
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <param name="deltaT"></param>
        public override void Update(Game gameInstance, double deltaT)
        {
            if (gameInstance.keyPressed.Contains(Keys.D) && Position.X < gameInstance.gameSize.Width - Image.Width)
            {
                Position.X += speedPixelPerSecond * deltaT;
            }
            if (gameInstance.keyPressed.Contains(Keys.Q) && Position.X > 0)
            {
                Position.X -= speedPixelPerSecond * deltaT;
            }
            if (gameInstance.keyPressed.Contains(Keys.S))
            {
                Shoot(gameInstance);
            }
        }

        /// <summary>
        /// Draw the Playership
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <param name="g"></param>
        public override void Draw(Game gameInstance, Graphics g)
        {
            base.Draw(gameInstance, g);
            g.DrawString("Vies 2 : " + this.Lives, defaultFont, Brushes.Black, 200, gameInstance.gameSize.Height - 20);
        }
        #endregion
    }
}

