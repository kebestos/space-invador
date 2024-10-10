using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// This is the generic abstact base class for any entity in the game
    /// </summary>
    abstract class GameObject
    {
        #region fields and properties
        /// <summary>
        /// enumeration of Sides of Objecc of the game
        /// </summary>
        public enum Side { Ally, Enemy, Neutral }

        /// <summary>
        /// Current side of the game
        /// </summary>
        private Side side;

        /// <summary>
        /// Score of the player
        /// </summary>
        public static int Score =0;

        /// <summary>
        /// default font
        /// </summary>
        protected static Font defaultFont = new Font("Times New Roman", 15, FontStyle.Bold, GraphicsUnit.Pixel);

        /// <summary>
        /// Propertie of Side
        /// </summary>
        public Side SideObjet
        {
            get { return side; }
            set { side = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor of GameObject
        /// </summary>
        /// <param name="s"></param>
        public GameObject(Side s)
        {
            this.side = s;
            
        }
        #endregion

        

        #region Abstract Methods
        /// <summary>
        /// Abstract Methods of Update
        /// </summary>
        /// <param name="gameInstance">instance of the current game</param>
        /// <param name="deltaT">time ellapsed in seconds since last call to Update</param>
        public abstract void Update(Game gameInstance, double deltaT);

        /// <summary>
        /// Abstract Methods of Draw
        /// </summary>
        /// <param name="g"></param>
        public abstract void Draw(Game gameInstance, Graphics g);

        /// <summary>
        ///Abstract Methods of IsAlive, the object will be removed automatically if is not alive
        /// </summary>
        /// <returns>Am I alive ?</returns>
        public abstract bool IsAlive();

        /// <summary>
        /// Abstract Methods of Collision
        /// </summary>
        /// <param name="m"></param>
        public abstract void Collision(Missile m);
        #endregion
    }
}