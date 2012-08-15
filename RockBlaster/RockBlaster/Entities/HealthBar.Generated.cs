using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Model;

using FlatRedBall.Input;
using FlatRedBall.Utilities;

using FlatRedBall.Instructions;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
// Generated Usings
using RockBlaster.Screens;
using Matrix = Microsoft.Xna.Framework.Matrix;
using FlatRedBall.Graphics;
using FlatRedBall.Math;
using FlatRedBall.Broadcasting;
using RockBlaster.Entities;
using RockBlaster.Factories;
using FlatRedBall;

#if XNA4
using Color = Microsoft.Xna.Framework.Color;
#else
using Color = Microsoft.Xna.Framework.Graphics.Color;
#endif

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
#endif

#if FRB_XNA && !MONODROID
using Model = Microsoft.Xna.Framework.Graphics.Model;
#endif

namespace RockBlaster.Entities
{
	public partial class HealthBar : PositionedObject, IDestroyable
	{
        // This is made global so that static lazy-loaded content can access it.
        public static string ContentManagerName
        {
            get;
            set;
        }

		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		public enum VariableState
		{
			Uninitialized, //This exists so that the first set call actually does something
			Full, 
			Depleted
		}
		VariableState mCurrentState = VariableState.Uninitialized;
		public VariableState CurrentState
		{
			get
			{
				return mCurrentState;
			}
			set
			{
				mCurrentState = value;
				switch(mCurrentState)
				{
					case  VariableState.Full:
						BarSpriteScaleX = 22f;
						if (BarSprite.Parent == null)
						{
							BarSpriteX = 0f;
						}
						else
						{
							BarSprite.RelativeX = 0f;
						}
						break;
					case  VariableState.Depleted:
						BarSpriteScaleX = 0f;
						if (BarSprite.Parent == null)
						{
							BarSpriteX = -22f;
						}
						else
						{
							BarSprite.RelativeX = -22f;
						}
						break;
				}
			}
		}
		static object mLockObject = new object();
		static bool mHasRegisteredUnload = false;
		static bool IsStaticContentLoaded = false;
		
		private FlatRedBall.Sprite FrameSprite;
		private FlatRedBall.Sprite BarSprite;
		public float FrameSpriteRed
		{
			get
			{
				return FrameSprite.Red;
			}
			set
			{
				FrameSprite.Red = value;
			}
		}
		public float FrameSpriteGreen
		{
			get
			{
				return FrameSprite.Green;
			}
			set
			{
				FrameSprite.Green = value;
			}
		}
		public float FrameSpriteBlue
		{
			get
			{
				return FrameSprite.Blue;
			}
			set
			{
				FrameSprite.Blue = value;
			}
		}
		public float BarSpriteRed
		{
			get
			{
				return BarSprite.Red;
			}
			set
			{
				BarSprite.Red = value;
			}
		}
		public float BarSpriteGreen
		{
			get
			{
				return BarSprite.Green;
			}
			set
			{
				BarSprite.Green = value;
			}
		}
		public float BarSpriteBlue
		{
			get
			{
				return BarSprite.Blue;
			}
			set
			{
				BarSprite.Blue = value;
			}
		}
		public float BarSpriteScaleX
		{
			get
			{
				return BarSprite.ScaleX;
			}
			set
			{
				BarSprite.ScaleX = value;
			}
		}
		public float BarSpriteX
		{
			get
			{
				if (BarSprite.Parent == null)
				{
					return BarSprite.X;
				}
				else
				{
					return BarSprite.RelativeX;
				}
			}
			set
			{
				if (BarSprite.Parent == null)
				{
					BarSprite.X = value;
				}
				else
				{
					BarSprite.RelativeX = value;
				}
			}
		}
		public int Index { get; set; }
		public bool Used { get; set; }
		protected Layer LayerProvidedByContainer = null;

        public HealthBar(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public HealthBar(string contentManagerName, bool addToManagers) :
			base()
		{
			// Don't delete this:
            ContentManagerName = contentManagerName;
            InitializeEntity(addToManagers);

		}

		protected virtual void InitializeEntity(bool addToManagers)
		{
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			FrameSprite = new FlatRedBall.Sprite();
			BarSprite = new FlatRedBall.Sprite();
			
			PostInitialize();
			if (addToManagers)
			{
				AddToManagers(null);
			}


		}

// Generated AddToManagers
		public virtual void AddToManagers (Layer layerToAddTo)
		{
			LayerProvidedByContainer = layerToAddTo;
			SpriteManager.AddPositionedObject(this);
			AddToManagersBottomUp(layerToAddTo);
			CustomInitialize();
		}

		public virtual void Activity()
		{
			// Generated Activity
			
			CustomActivity();
			
			// After Custom Activity
		}

		public virtual void Destroy()
		{
			// Generated Destroy
			SpriteManager.RemovePositionedObject(this);
			
			if (FrameSprite != null)
			{
				FrameSprite.Detach(); SpriteManager.RemoveSprite(FrameSprite);
			}
			if (BarSprite != null)
			{
				BarSprite.Detach(); SpriteManager.RemoveSprite(BarSprite);
			}


			CustomDestroy();
		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			if (FrameSprite!= null && FrameSprite.Parent == null)
			{
				FrameSprite.CopyAbsoluteToRelative();
				FrameSprite.AttachTo(this, false);
			}
			FrameSprite.ScaleX = 24f;
			FrameSprite.ScaleY = 6f;
			if (BarSprite!= null && BarSprite.Parent == null)
			{
				BarSprite.CopyAbsoluteToRelative();
				BarSprite.AttachTo(this, false);
			}
			if (BarSprite.Parent == null)
			{
				BarSprite.X = 0f;
			}
			else
			{
				BarSprite.RelativeX = 0f;
			}
			if (BarSprite.Parent == null)
			{
				BarSprite.Y = 0f;
			}
			else
			{
				BarSprite.RelativeY = 0f;
			}
			if (BarSprite.Parent == null)
			{
				BarSprite.Z = 1f;
			}
			else
			{
				BarSprite.RelativeZ = 1f;
			}
			BarSprite.ScaleX = 22f;
			BarSprite.ScaleY = 4f;
			FrameSpriteRed = 1f;
			FrameSpriteGreen = 1f;
			FrameSpriteBlue = 0f;
			BarSpriteRed = 0f;
			BarSpriteGreen = 1f;
			BarSpriteBlue = 0f;
			BarSpriteScaleX = 22f;
			BarSpriteX = 0f;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp (Layer layerToAddTo)
		{
			// We move this back to the origin and unrotate it so that anything attached to it can just use its absolute position
			float oldRotationX = RotationX;
			float oldRotationY = RotationY;
			float oldRotationZ = RotationZ;
			
			float oldX = X;
			float oldY = Y;
			float oldZ = Z;
			
			X = 0;
			Y = 0;
			Z = 0;
			RotationX = 0;
			RotationY = 0;
			RotationZ = 0;
			SpriteManager.AddToLayer(FrameSprite, layerToAddTo);
			FrameSprite.ScaleX = 24f;
			FrameSprite.ScaleY = 6f;
			SpriteManager.AddToLayer(BarSprite, layerToAddTo);
			if (BarSprite.Parent == null)
			{
				BarSprite.X = 0f;
			}
			else
			{
				BarSprite.RelativeX = 0f;
			}
			if (BarSprite.Parent == null)
			{
				BarSprite.Y = 0f;
			}
			else
			{
				BarSprite.RelativeY = 0f;
			}
			if (BarSprite.Parent == null)
			{
				BarSprite.Z = 1f;
			}
			else
			{
				BarSprite.RelativeZ = 1f;
			}
			BarSprite.ScaleX = 22f;
			BarSprite.ScaleY = 4f;
			X = oldX;
			Y = oldY;
			Z = oldZ;
			RotationX = oldRotationX;
			RotationY = oldRotationY;
			RotationZ = oldRotationZ;
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			this.ForceUpdateDependenciesDeep();
			SpriteManager.ConvertToManuallyUpdated(this);
			SpriteManager.ConvertToManuallyUpdated(FrameSprite);
			SpriteManager.ConvertToManuallyUpdated(BarSprite);
		}
		public static void LoadStaticContent (string contentManagerName)
		{
			ContentManagerName = contentManagerName;
			#if DEBUG
			if (contentManagerName == FlatRedBallServices.GlobalContentManager)
			{
				HasBeenLoadedWithGlobalContentManager = true;
			}
			else if (HasBeenLoadedWithGlobalContentManager)
			{
				throw new Exception("This type has been loaded with a Global content manager, then loaded with a non-global.  This can lead to a lot of bugs");
			}
			#endif
			if (IsStaticContentLoaded == false)
			{
				IsStaticContentLoaded = true;
				lock (mLockObject)
				{
					if (!mHasRegisteredUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("HealthBarStaticUnload", UnloadStaticContent);
						mHasRegisteredUnload = true;
					}
				}
				bool registerUnload = false;
				if (registerUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
				{
					lock (mLockObject)
					{
						if (!mHasRegisteredUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
						{
							FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("HealthBarStaticUnload", UnloadStaticContent);
							mHasRegisteredUnload = true;
						}
					}
				}
				CustomLoadStaticContent(contentManagerName);
			}
		}
		public static void UnloadStaticContent ()
		{
			IsStaticContentLoaded = false;
			mHasRegisteredUnload = false;
		}
		static VariableState mLoadingState = VariableState.Uninitialized;
		public static VariableState LoadingState
		{
			get
			{
				return mLoadingState;
			}
			set
			{
				mLoadingState = value;
			}
		}
		public Instruction InterpolateToState (VariableState stateToInterpolateTo, double secondsToTake)
		{
			switch(stateToInterpolateTo)
			{
				case  VariableState.Full:
					BarSprite.ScaleXVelocity = (22f - BarSprite.ScaleX) / (float)secondsToTake;
					if (BarSprite.Parent != null)
					{
						BarSprite.RelativeXVelocity = (0f - BarSprite.RelativeX) / (float)secondsToTake;
					}
					else
					{
						BarSprite.XVelocity = (0f - BarSprite.X) / (float)secondsToTake;
					}
					break;
				case  VariableState.Depleted:
					BarSprite.ScaleXVelocity = (0f - BarSprite.ScaleX) / (float)secondsToTake;
					if (BarSprite.Parent != null)
					{
						BarSprite.RelativeXVelocity = (-22f - BarSprite.RelativeX) / (float)secondsToTake;
					}
					else
					{
						BarSprite.XVelocity = (-22f - BarSprite.X) / (float)secondsToTake;
					}
					break;
			}
			var instruction = new DelegateInstruction<VariableState>(StopStateInterpolation, stateToInterpolateTo);
			instruction.TimeToExecute = TimeManager.CurrentTime + secondsToTake;
			this.Instructions.Add(instruction);
			return instruction;
		}
		public void StopStateInterpolation (VariableState stateToStop)
		{
			switch(stateToStop)
			{
				case  VariableState.Full:
					BarSprite.ScaleXVelocity =  0;
					if (BarSprite.Parent != null)
					{
						BarSprite.RelativeXVelocity =  0;
					}
					else
					{
						BarSprite.XVelocity =  0;
					}
					break;
				case  VariableState.Depleted:
					BarSprite.ScaleXVelocity =  0;
					if (BarSprite.Parent != null)
					{
						BarSprite.RelativeXVelocity =  0;
					}
					else
					{
						BarSprite.XVelocity =  0;
					}
					break;
			}
			CurrentState = stateToStop;
		}
		public void InterpolateBetween (VariableState firstState, VariableState secondState, float interpolationValue)
		{
			#if DEBUG
			if (float.IsNaN(interpolationValue))
			{
				throw new Exception("interpolationValue cannot be NaN");
			}
			#endif
			bool setBarSpriteScaleX = true;
			Single BarSpriteScaleXFirstValue = 0;
			Single BarSpriteScaleXSecondValue = 0;
			bool setBarSpriteX = true;
			Single BarSpriteXFirstValue = 0;
			Single BarSpriteXSecondValue = 0;
			switch(firstState)
			{
				case  VariableState.Full:
					BarSpriteScaleXFirstValue = 22f;
					BarSpriteXFirstValue = 0f;
					break;
				case  VariableState.Depleted:
					BarSpriteScaleXFirstValue = 0f;
					BarSpriteXFirstValue = -22f;
					break;
			}
			switch(secondState)
			{
				case  VariableState.Full:
					BarSpriteScaleXSecondValue = 22f;
					BarSpriteXSecondValue = 0f;
					break;
				case  VariableState.Depleted:
					BarSpriteScaleXSecondValue = 0f;
					BarSpriteXSecondValue = -22f;
					break;
			}
			if (setBarSpriteScaleX)
			{
				BarSpriteScaleX = BarSpriteScaleXFirstValue * (1 - interpolationValue) + BarSpriteScaleXSecondValue * interpolationValue;
			}
			if (setBarSpriteX)
			{
				if (BarSprite.Parent != null)
				{
					BarSprite.RelativeX = BarSpriteXFirstValue * (1 - interpolationValue) + BarSpriteXSecondValue * interpolationValue;
				}
				else
				{
					BarSpriteX = BarSpriteXFirstValue * (1 - interpolationValue) + BarSpriteXSecondValue * interpolationValue;
				}
			}
		}
		object GetMember (string memberName)
		{
			return null;
		}
		protected bool mIsPaused;
		public override void Pause (InstructionList instructions)
		{
			base.Pause(instructions);
			mIsPaused = true;
		}
		public virtual void SetToIgnorePausing ()
		{
			InstructionManager.IgnorePausingFor(this);
			InstructionManager.IgnorePausingFor(FrameSprite);
			InstructionManager.IgnorePausingFor(BarSprite);
		}

    }
	
	
	// Extra classes
	public static class HealthBarExtensionMethods
	{
	}
	
}
