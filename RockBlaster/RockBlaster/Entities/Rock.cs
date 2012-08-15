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
	public partial class Rock
	{
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

        public void TakeHit()
        {
            switch (this.CurrentState)
            {
                case VariableState.Size4:
                    BreakIntoPieces(VariableState.Size3);
                    break;
                case VariableState.Size3:
                    BreakIntoPieces(VariableState.Size2);
                    break;
                case VariableState.Size2:
                    BreakIntoPieces(VariableState.Size1);
                    break;
                case VariableState.Size1:
                    // do nothing
                    break;
            }
            this.Destroy();
        }

        private void BreakIntoPieces(VariableState newRockState)
        {
            for (int i = 0; i < NumberOfRocksToBreakInto; i++)
            {
                Rock newRock = RockBlaster.Factories.RockFactory.CreateNew();
                newRock.Position = this.Position;
                newRock.Position.X += -1 + 2 * (float)(FlatRedBallServices.Random.NextDouble());
                newRock.Position.Y += -1 + 2 * (float)(FlatRedBallServices.Random.NextDouble());

                float randomAngle =
                    (float)(FlatRedBallServices.Random.NextDouble() * System.Math.PI * 2);

                float speed = 0 + (float)(FlatRedBallServices.Random.NextDouble() * RandomSpeedOnBreak);
                newRock.Velocity = FlatRedBall.Math.MathFunctions.AngleToVector(randomAngle) * speed;
                newRock.CurrentState = newRockState;
            }
        }
    }
}
