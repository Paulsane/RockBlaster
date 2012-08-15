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
using RockBlaster.Data;

namespace RockBlaster.Screens
{
	public partial class GameScreen
	{

		void CustomInitialize()
		{
            AddAdditionalShips();
            this.HudInstance.MainShipList = MainShipList;
		}

        private void AddAdditionalShips()
        {
            for (int i = 1; i < InputManager.Xbox360GamePads.Length; i++)
            {
                if (InputManager.Xbox360GamePads[i].IsConnected)
                {
                    MainShip mainShip = new MainShip(ContentManagerName);
                    mainShip.PlayerIndex = i;
                    MainShipList.Add(mainShip);
                }
            }

            const float spacingBetweenPlayers = 60;
            const float startingX = -90;
            // Reposition all players
            for (int i = 0; i < MainShipList.Count; i++)
            {
                MainShipList[i].X = -startingX + i * spacingBetweenPlayers;
            }
        }

		void CustomActivity(bool firstTimeCalled)
		{
            CollisionActivity();
            RemovalActivity();
            EndGameActivity();
		}

        void EndGameActivity()
        {
            if (EndGameUiInstance.Visible == false && this.MainShipList.Count == 0)
            {
                EndGameUiInstance.Visible = true;
            }
        }

        void RemovalActivity()
        {
            for (int i = BulletList.Count - 1; i > -1; i--)
            {
                float absoluteX = Math.Abs(BulletList[i].X);
                float absoluteY = Math.Abs(BulletList[i].Y);

                const float removeBeyond = 600;
                if (absoluteX > removeBeyond || absoluteY > removeBeyond)
                {
                    BulletList[i].Destroy();
                }
            }

            for (int i = RockList.Count - 1; i > -1; i--)
            {
                float absoluteX = Math.Abs(RockList[i].X);
                float absoluteY = Math.Abs(RockList[i].Y);

                const float removeBeyond = 600;
                if (absoluteX > removeBeyond || absoluteY > removeBeyond)
                {
                    RockList[i].Destroy();
                }
            }
        }

        private void CollisionActivity()
        {
            BulletVsRockCollisionActivity();
            MainShipVsRockCollisionActivity();
            RockVsRockCollisionActivity();
        }

        private void BulletVsRockCollisionActivity()
        {
            for (int i = BulletList.Count - 1; i > -1; i--)
            {
                Bullet bullet = BulletList[i];
                for (int j = RockList.Count - 1; j > -1; j--)
                {
                    Rock rock = RockList[j];

                    if (rock.Collision.CollideAgainst(bullet.Collision))
                    {
                        GlobalData.PlayerData.Score += rock.PointsWorth;
                        this.HudInstance.Score = GlobalData.PlayerData.Score;
                        rock.TakeHit();
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
                        mainShip.Health--;
                        rock.TakeHit();
                        break;
                    }
                }
            }
        }

        private void RockVsRockCollisionActivity()
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
