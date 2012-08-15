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
	public partial class MainShip
	{
        int mHealth;
        public int Health
        {
            get
            {
                return mHealth;
            }
            set
            {
                mHealth = value;
                if (mHealth <= 0)
                {
                    Destroy();
                }
            }
        }

        int mPlayerIndex = 0;
        public int PlayerIndex
        {
            get { return mPlayerIndex; }
            set
            {
                mPlayerIndex = value;
                mGamePad = InputManager.Xbox360GamePads[mPlayerIndex];

                switch (mPlayerIndex)
                {
                    case 0:
                        this.Sprite.Texture = MainShip1;
                        break;
                    case 1:
                        this.Sprite.Texture = MainShip2;
                        break;
                    case 2:
                        this.Sprite.Texture = MainShip3;
                        break;
                    case 3:
                        this.Sprite.Texture = MainShip4;
                        break;
                }
            }
        }

        Xbox360GamePad mGamePad;

		private void CustomInitialize()
		{
            mGamePad = InputManager.Xbox360GamePads[0];
            Health = StartingHealth;
		}

		private void CustomActivity()
		{
            MovementActivity();
            TurningActivity();
            ShootingActivity();
		}

        private void ShootingActivity()
        {
            if (mGamePad.ButtonPushed(Xbox360GamePad.Button.A))
            {
                Bullet firstBullet = RockBlaster.Factories.BulletFactory.CreateNew();
                firstBullet.Position = this.Position;
                firstBullet.Position += this.RotationMatrix.Up * 12;
                firstBullet.Position += this.RotationMatrix.Right * 6;
                firstBullet.RotationZ = this.RotationZ;
                firstBullet.Velocity = this.RotationMatrix.Up * firstBullet.MovementSpeed;

                Bullet secondBullet = RockBlaster.Factories.BulletFactory.CreateNew();
                secondBullet.Position = this.Position;
                secondBullet.Position += this.RotationMatrix.Up * 12;
                secondBullet.Position -= this.RotationMatrix.Right * 6;
                secondBullet.RotationZ = this.RotationZ;
                secondBullet.Velocity = this.RotationMatrix.Up * secondBullet.MovementSpeed;
            }
        }

        private void TurningActivity()
        {
            this.RotationZVelocity = -mGamePad.LeftStick.Position.X * TurningSpeed;
        }

        void MovementActivity()
        {
            this.Velocity = this.RotationMatrix.Up * this.MovementSpeed;
        }

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
	}
}
