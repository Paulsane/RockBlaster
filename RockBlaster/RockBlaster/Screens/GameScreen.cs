using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Graphics.Model;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
using FlatRedBall.Localization;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
#endif

using RockBlaster.Entities;

namespace RockBlaster.Screens
{
	public partial class GameScreen
	{

		void CustomInitialize()
		{


		}

		void CustomActivity(bool firstTimeCalled)
		{
            CollisionActivity();

		}

        private void CollisionActivity()
        {
            BulletVsRockCollisionActivity();
            MainShipVsRockCollisionActivity();
            RockVsRockCollisionActivity();
        }

        private void RockVsRockCollisionActivity()
        {
            for (int i = BulletList.Count - 1; i > -1; i--)
            {
                Bullet bullet = BulletList[i];
                for (int j = RockList.Count - 1; j > -1; j--)
                {
                    Rock rock = RockList[j];

                    if (rock.Collision.CollideAgainst(bullet.Collision))
                    {
                        rock.Destroy();
                        bullet.Destroy();
                        break;
                    }
                }
            }
        }

        private void MainShipVsRockCollisionActivity()
        {
            for (int i = RockList.Count - 1; i > -1; i--)
            {
                Rock rock = RockList[i];
                for (int j = MainShipList.Count - 1; j > -1; j--)
                {
                    MainShip mainShip = MainShipList[j];

                    if (mainShip.Collision.CollideAgainst(rock.Collision))
                    {
                        mainShip.Destroy();
                        rock.Destroy();
                        break;
                    }
                }
            }
        }

        private void BulletVsRockCollisionActivity()
        {
            for (int i = 0; i < RockList.Count; i++)
            {
                Rock firstRock = RockList[i];
                for (int j = i + 1; j < RockList.Count; j++)
                {
                    Rock secondRock = RockList[j];

                    float firstRockMass = 1;
                    float secondRockMass = 1;
                    float elasticity = .8f;

                    firstRock.Collision.CollideAgainstBounce(
                        secondRock.Collision, firstRockMass, secondRockMass, elasticity);
                }
            }
        }

		void CustomDestroy()
		{


		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

	}
}
