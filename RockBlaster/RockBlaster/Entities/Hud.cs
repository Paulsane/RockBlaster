using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using FlatRedBall.Math;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;


#endif

namespace RockBlaster.Entities
{
	public partial class Hud
	{
        PositionedObjectList<MainShip> mMainShipList;
        public PositionedObjectList<MainShip> MainShipList
        {
            set
            {
                mMainShipList = value;
                CreateHealthBarInstances();
            }
        }

        void CreateHealthBarInstances()
        {
            // First we instantiate them
            for (int i = 0; i < mMainShipList.Count; i++)
            {
                HealthBar healthBar = new HealthBar(ContentManagerName, false);
                healthBar.MainShip = mMainShipList[i];
                healthBar.AddToManagers(this.LayerProvidedByContainer);
                healthBar.AttachTo(this, false);
                this.HealthBarList.Add(healthBar);
            }

            int divisions = mMainShipList.Count + 1;

            // Now let's position the HealthBars
            for (int i = 0; i < divisions - 1; i++)
            {
                HealthBarList[i].RelativeX = -SpriteManager.Camera.OrthogonalWidth / 2.0f +
                           SpriteManager.Camera.OrthogonalWidth * (i + 1) / (float)divisions;
                HealthBarList[i].RelativeY = HealthBarY;
            }
        }

		private void CustomInitialize()
		{


		}

		private void CustomActivity()
		{


		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
	}
}
