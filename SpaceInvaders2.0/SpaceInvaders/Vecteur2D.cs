using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class Vecteur2D
    {
        #region fields and properties
        /// <summary>
        /// coordinate in X
        /// </summary>
        public double x;

        /// <summary>
        /// coordinate in Y 
        /// </summary>
        public double y;

        /// <summary>
        /// Propertie of X
        /// </summary>
        public double X
        {
            get{return x;}
            set{x = value;}
        }

        /// <summary>
        /// Propertie of Y
        /// </summary>
        public double Y
        {
            get{return y;}
            set{y = value;}
        }
        /// <summary>
        /// Propertie of the norme of two vectors
        /// </summary>
        public double Norme => Math.Sqrt(x * x) + (y * y);
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor of Vector
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vecteur2D(double x = 0.0, double y = 0.0)
        {
            this.x = x;
            this.y = y;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Change operator + to the rules vector
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        public static Vecteur2D operator +(Vecteur2D v1, Vecteur2D v2)
        {
            return new Vecteur2D(v1.x + v2.x, v1.y + v2.y);
        }
        /// <summary>
        /// Change operator - to the rules vector
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vecteur2D operator -(Vecteur2D v1, Vecteur2D v2)
        {
            return new Vecteur2D(v1.x - v2.x, v1.y - v2.y);
        }
        /// <summary>
        /// Reverse the sign 
        /// </summary>
        /// <param name="v1"></param>
        /// <returns></returns>
        public static Vecteur2D operator -(Vecteur2D v1)
        {
            return new Vecteur2D(-v1.x, -v1.y);
        }
        /// <summary>
        /// Change operator * to the rules vector, multiply by a constant
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static Vecteur2D operator *(Vecteur2D v1, double k)
        {
            return new Vecteur2D(v1.x * k, v1.y * k);
        }
        /// <summary>
        /// Same thing but the constant position is reverse
        /// </summary>
        /// <param name="k"></param>
        /// <param name="v1"></param>
        /// <returns></returns>
        public static Vecteur2D operator *(double k, Vecteur2D v1)
        {
            return new Vecteur2D(k * v1.x, k * v1.y);
        }
        /// <summary>
        /// Change operator / to the rules vector
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static Vecteur2D operator /(Vecteur2D v1, double k)
        {
            return new Vecteur2D(v1.x / k, v1.y / k);
        }
        #endregion
    }
}
