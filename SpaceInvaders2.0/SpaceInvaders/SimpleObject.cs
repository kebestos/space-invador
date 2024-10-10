using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SpaceInvaders
{
    abstract class SimpleObject : GameObject
    {
        #region fields and properties
        /// <summary>
        /// Position of SimpleObject
        /// </summary>
        public Vecteur2D Position { get; set; }

        /// <summary>
        /// Image of SimpleObject
        /// </summary>
        public Bitmap Image;

        /// <summary>
        /// Position of missile for collision, accessibility in Bunker
        /// </summary>
        protected double missile_x;
        protected double missile_y;

        /// <summary>
        /// Lives of SimpleObject
        /// </summary>
        public int Lives { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor of SimpleObject
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="lives"></param>
        /// <param name="image"></param>
        /// <param name="s"></param>
        public SimpleObject(double x, double y, int lives, Bitmap image,Side s) : base (s)
        {
            this.Position = new Vecteur2D(x, y);
            this.Lives = lives;
            this.Image = image;
            
        }

        #endregion

        #region Methods
        /// <summary>
        /// Draw the SimpleObject
        /// </summary>
        /// <param name="gameInstance"></param>
        /// <param name="g"></param>
        public override void Draw(Game gameInstance, Graphics g)
        {
            float positionX = (float)Position.X;
            float positionY = (float)Position.y;
            g.DrawImage(Image, positionX, positionY, Image.Width, Image.Height);
        }

        /// <summary>
        /// Check if the SimpleObject is Alive
        /// </summary>
        /// <returns></returns>
        public override bool IsAlive()
        {
            if (Lives <= 0)
            {return false;}
            return true;
        }
        
        /// <summary>
        /// Collision between a missile and a SimpleObject
        /// </summary>
        /// <param name="m"></param>
        public override void Collision(Missile m)
        {
            if (Position.X < m.Position.X && Position.X + Image.Width > m.Position.X && Position.Y + Image.Height > m.Position.Y && Position.Y < m.Position.Y)
            {
                //Browse the Height of the missile
                for (int i = 0; i < m.Image.Height; i++)
                {
                    //Browse the Width of the missile
                    for (int j = 0; j < m.Image.Width; j++)
                    {
                        //Check if the missile is alive
                        if (m.Lives > 0)
                        {
                            //Check if the missile is in the area of the SimpleObject
                            if (Position.X < m.Position.X + j && Position.X + Image.Width > m.Position.X + j && Position.Y + Image.Height > m.Position.Y + i && Position.Y < m.Position.Y + i)
                            {
                                //Pixel of the missile
                                Color cM = m.Image.GetPixel(j, i);
                                 missile_x = m.Position.X + j - Position.X;
                                 missile_y = m.Position.Y + i - Position.Y;
                                
                                //Pixel of the SimpleObject
                                Color cB = Image.GetPixel((int)missile_x, (int)missile_y);
                                int opacity_M = cM.A;
                                int opacity_B = cB.A;
                                //Check if pixels aren't remove
                                if (opacity_B > 0 && opacity_M > 0)
                                {
                                    if (this.SideObjet != m.SideObjet)
                                    {
                                        OnCollision(m);
                                    }
                                }
                            }
                        }

                    }
                }

            }

        }

        /// <summary>
        /// Methods abstract for a specific SimpleObject
        /// </summary>
        /// <param name="m"></param>
        protected abstract void OnCollision(Missile m);

        #endregion
    }
}
