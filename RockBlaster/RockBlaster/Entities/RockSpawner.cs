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
	public partial class RockSpawner
	{
        double mLastSpawnTime;
        bool IsTimeToSpawn
        {
            get
            {
                float spawnFrequency = 1 / RocksPerSecond;
                return Screens.ScreenManager.CurrentScreen.PauseAdjustedSecondsSince(mLastSpawnTime) > spawnFrequency;
            }
        }

		private void CustomInitialize()
		{


		}

		private void CustomActivity()
		{
            if (IsTimeToSpawn)
            {
                PerformSpawn();
            }
            this.RocksPerSecond += TimeManager.SecondDifference * this.SpawnRateIncrease;

		}

        private void PerformSpawn()
        {
            Vector3 position = GetRandomRockPosition();
            Vector3 velocity = GetRandomRockVelocity(position);

            Rock rock = RockBlaster.Factories.RockFactory.CreateNew();
            rock.Position = position;
            rock.Velocity = velocity;

            mLastSpawnTime = Screens.ScreenManager.CurrentScreen.PauseAdjustedCurrentTime;
        }

        private Vector3 GetRandomRockVelocity(Vector3 position)
        {
            Vector3 centerOfGameScreen = new Vector3(SpriteManager.Camera.X, SpriteManager.Camera.Y, 0);

            Vector3 directionToCenter = centerOfGameScreen - position;

            directionToCenter.Normalize();

            float speed = MinVelocity +
                (float)(FlatRedBallServices.Random.NextDouble() * (MaxVelocity - MinVelocity));

            return speed * directionToCenter;
        }

        private Vector3 GetRandomRockPosition()
        {
            int randomSide = FlatRedBallServices.Random.Next(4);

            float topEdge = SpriteManager.Camera.AbsoluteTopYEdgeAt(0);
            float bottomEdge = SpriteManager.Camera.AbsoluteBottomYEdgeAt(0);
            float leftEdge = SpriteManager.Camera.AbsoluteLeftXEdgeAt(0);
            float rightEdge = SpriteManager.Camera.AbsoluteRightXEdgeAt(0);

            float minX = 0;
            float maxX = 0;
            float minY = 0;
            float maxY = 0;
            switch (randomSide)
            {
                case 0: // top
                    minX = leftEdge;
                    maxX = rightEdge;
                    minY = topEdge;
                    maxY = topEdge;
                    break;
                case 1: // right
                    minX = rightEdge;
                    maxX = rightEdge;
                    minY = bottomEdge;
                    maxY = topEdge;
                    break;
                case 2: // bottom
                    minX = leftEdge;
                    maxX = rightEdge;
                    minY = bottomEdge;
                    maxY = bottomEdge;
                    break;
                case 3: // left
                    minX = leftEdge;
                    maxX = leftEdge;
                    minY = bottomEdge;
                    maxY = topEdge;
                    break;
            }

            float offScreenX = minX + (float)(FlatRedBallServices.Random.NextDouble() * (maxX - minX));
            float offScreenY = minY + (float)(FlatRedBallServices.Random.NextDouble() * (maxY - minY));

            float amountToMoveBy = 64;
            switch (randomSide)
            {
                case 0: // top
                    offScreenY += amountToMoveBy;
                    break;
                case 1: // right
                    offScreenX += amountToMoveBy;
                    break;
                case 2: // bottom
                    offScreenY -= amountToMoveBy;
                    break;
                case 3: // left
                    offScreenX -= amountToMoveBy;
                    break;
            }

            return new Vector3(offScreenX, offScreenY, 0);
        }

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
	}
}
