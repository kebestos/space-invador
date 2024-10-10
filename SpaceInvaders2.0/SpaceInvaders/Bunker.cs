using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceInvaders
{
    class Bunker : SimpleObject
    {
        #region Constructor
        /// <summary>
        /// Constructor of Bunker
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="s"></param>
        public Bunker(double x, double y,Side s) : base(x, y, 10, SpaceInvaders.Properties.Resources.bunker,s){}
        #endregion

        #region Methods
        /// <summary>
        /// Do nothing
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <param name="deltaT"></param>
        public override void Update(Game gameInstance, double deltaT) { }

        /// <summary>
        /// Collision between a bunker and a missile
        /// </summary>
        /// <param name="m"></param>
        protected override void OnCollision(Missile m)
        {
            //Remove a live of the missile
            m.Lives -= 1;
            //Change the pixel of the bunker
            Image.SetPixel((int)missile_x, (int)missile_y, Color.FromArgb(00000000));
        }

        #endregion
    }
}
