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
using RockBlaster.Performance;
using FlatRedBall.Broadcasting;
using RockBlaster.Entities;
using RockBlaster.Factories;
using FlatRedBall;
using FlatRedBall.Math.Geometry;
using Microsoft.Xna.Framework.Graphics;

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
	public partial class Rock : PositionedObject, IDestroyable, IPoolable
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
			Size1, 
			Size2, 
			Size3, 
			Size4
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
					case  VariableState.Size1:
						SpriteTexture = Rock1;
						CollisionRadius = 6f;
						break;
					case  VariableState.Size2:
						SpriteTexture = Rock2;
						CollisionRadius = 12f;
						break;
					case  VariableState.Size3:
						SpriteTexture = Rock3;
						CollisionRadius = 20f;
						break;
					case  VariableState.Size4:
						SpriteTexture = Rock4;
						CollisionRadius = 30f;
						break;
				}
			}
		}
		static object mLockObject = new object();
		static bool mHasRegisteredUnload = false;
		static bool IsStaticContentLoaded = false;
		private static Texture2D Rock1;
		private static Texture2D Rock2;
		private static Texture2D Rock3;
		private static Texture2D Rock4;
		
		private FlatRedBall.Sprite Sprite;
		private FlatRedBall.Math.Geometry.Circle mCollision;
		public FlatRedBall.Math.Geometry.Circle Collision
		{
			get
			{
				return mCollision;
			}
		}
		public Microsoft.Xna.Framework.Graphics.Texture2D SpriteTexture
		{
			get
			{
				return Sprite.Texture;
			}
			set
			{
				Sprite.Texture = value;
			}
		}
		public float CollisionRadius
		{
			get
			{
				return Collision.Radius;
			}
			set
			{
				Collision.Radius = value;
			}
		}
		public int NumberOfRocksToBreakInto = 2;
		public float RandomSpeedOnBreak = 50f;
		public int PointsWorth = 10;
		public int Index { get; set; }
		public bool Used { get; set; }
		protected Layer LayerProvidedByContainer = null;

        public Rock(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public Rock(string contentManagerName, bool addToManagers) :
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
			Sprite = new FlatRedBall.Sprite();
			mCollision = new FlatRedBall.Math.Geometry.Circle();
			
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
			if (Used)
			{
				RockFactory.MakeUnused(this, false);
			}
			
			if (Sprite != null)
			{
				Sprite.Detach(); SpriteManager.RemoveSprite(Sprite);
			}
			if (Collision != null)
			{
				Collision.Detach(); ShapeManager.Remove(Collision);
			}


			CustomDestroy();
		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			if (Sprite!= null && Sprite.Parent == null)
			{
				Sprite.CopyAbsoluteToRelative();
				Sprite.AttachTo(this, false);
			}
			Sprite.PixelSize = 0.5f;
			Sprite.Texture = Rock1;
			if (mCollision!= null && mCollision.Parent == null)
			{
				mCollision.CopyAbsoluteToRelative();
				mCollision.AttachTo(this, false);
			}
			Collision.Visible = false;
			Collision.Radius = 6f;
			CollisionRadius = 6f;
			NumberOfRocksToBreakInto = 2;
			RandomSpeedOnBreak = 50f;
			PointsWorth = 10;
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
			SpriteManager.AddToLayer(Sprite, layerToAddTo);
			Sprite.PixelSize = 0.5f;
			Sprite.Texture = Rock1;
			ShapeManager.AddToLayer(mCollision, layerToAddTo);
			mCollision.Visible = false;
			mCollision.Radius = 6f;
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
			SpriteManager.ConvertToManuallyUpdated(Sprite);
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
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("RockStaticUnload", UnloadStaticContent);
						mHasRegisteredUnload = true;
					}
				}
				bool registerUnload = false;
				if (!FlatRedBallServices.IsLoaded<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/rock/rock1.png", ContentManagerName))
				{
					registerUnload = true;
				}
				Rock1 = FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/rock/rock1.png", ContentManagerName);
				if (!FlatRedBallServices.IsLoaded<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/rock/rock2.png", ContentManagerName))
				{
					registerUnload = true;
				}
				Rock2 = FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/rock/rock2.png", ContentManagerName);
				if (!FlatRedBallServices.IsLoaded<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/rock/rock3.png", ContentManagerName))
				{
					registerUnload = true;
				}
				Rock3 = FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/rock/rock3.png", ContentManagerName);
				if (!FlatRedBallServices.IsLoaded<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/rock/rock4.png", ContentManagerName))
				{
					registerUnload = true;
				}
				Rock4 = FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/rock/rock4.png", ContentManagerName);
				if (registerUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
				{
					lock (mLockObject)
					{
						if (!mHasRegisteredUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
						{
							FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("RockStaticUnload", UnloadStaticContent);
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
			if (Rock1 != null)
			{
				Rock1= null;
			}
			if (Rock2 != null)
			{
				Rock2= null;
			}
			if (Rock3 != null)
			{
				Rock3= null;
			}
			if (Rock4 != null)
			{
				Rock4= null;
			}
		}
		public static object GetStaticMember (string memberName)
		{
			switch(memberName)
			{
				case  "Rock1":
					return Rock1;
				case  "Rock2":
					return Rock2;
				case  "Rock3":
					return Rock3;
				case  "Rock4":
					return Rock4;
			}
			return null;
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
				case  VariableState.Size1:
					Collision.RadiusVelocity = (6f - Collision.Radius) / (float)secondsToTake;
					break;
				case  VariableState.Size2:
					Collision.RadiusVelocity = (12f - Collision.Radius) / (float)secondsToTake;
					break;
				case  VariableState.Size3:
					Collision.RadiusVelocity = (20f - Collision.Radius) / (float)secondsToTake;
					break;
				case  VariableState.Size4:
					Collision.RadiusVelocity = (30f - Collision.Radius) / (float)secondsToTake;
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
				case  VariableState.Size1:
					Collision.RadiusVelocity =  0;
					break;
				case  VariableState.Size2:
					Collision.RadiusVelocity =  0;
					break;
				case  VariableState.Size3:
					Collision.RadiusVelocity =  0;
					break;
				case  VariableState.Size4:
					Collision.RadiusVelocity =  0;
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
			bool setCollisionRadius = true;
			Single CollisionRadiusFirstValue = 0;
			Single CollisionRadiusSecondValue = 0;
			switch(firstState)
			{
				case  VariableState.Size1:
					CollisionRadiusFirstValue = 6f;
					break;
				case  VariableState.Size2:
					CollisionRadiusFirstValue = 12f;
					break;
				case  VariableState.Size3:
					CollisionRadiusFirstValue = 20f;
					break;
				case  VariableState.Size4:
					CollisionRadiusFirstValue = 30f;
					break;
			}
			switch(secondState)
			{
				case  VariableState.Size1:
					CollisionRadiusSecondValue = 6f;
					break;
				case  VariableState.Size2:
					CollisionRadiusSecondValue = 12f;
					break;
				case  VariableState.Size3:
					CollisionRadiusSecondValue = 20f;
					break;
				case  VariableState.Size4:
					CollisionRadiusSecondValue = 30f;
					break;
			}
			if (setCollisionRadius)
			{
				CollisionRadius = CollisionRadiusFirstValue * (1 - interpolationValue) + CollisionRadiusSecondValue * interpolationValue;
			}
		}
		object GetMember (string memberName)
		{
			switch(memberName)
			{
				case  "Rock1":
					return Rock1;
				case  "Rock2":
					return Rock2;
				case  "Rock3":
					return Rock3;
				case  "Rock4":
					return Rock4;
			}
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
			InstructionManager.IgnorePausingFor(Sprite);
			InstructionManager.IgnorePausingFor(Collision);
		}

    }
	
	
	// Extra classes
	public static class RockExtensionMethods
	{
	}
	
}
