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
	public partial class MainShip : PositionedObject, IDestroyable
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
		static object mLockObject = new object();
		static bool mHasRegisteredUnload = false;
		static bool IsStaticContentLoaded = false;
		private static Texture2D MainShip1;
		private static Texture2D MainShip2;
		private static Texture2D MainShip3;
		private static Texture2D MainShip4;
		
		private FlatRedBall.Sprite Sprite;
		private FlatRedBall.Math.Geometry.Circle mCollision;
		public FlatRedBall.Math.Geometry.Circle Collision
		{
			get
			{
				return mCollision;
			}
		}
		public float MovementSpeed = 100f;
		public float TurningSpeed = 3.14f;
		public int StartingHealth = 6;
		public int Index { get; set; }
		public bool Used { get; set; }
		protected Layer LayerProvidedByContainer = null;

        public MainShip(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public MainShip(string contentManagerName, bool addToManagers) :
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
			if (Sprite.Parent == null)
			{
				Sprite.X = 0f;
			}
			else
			{
				Sprite.RelativeX = 0f;
			}
			if (Sprite.Parent == null)
			{
				Sprite.Y = 0f;
			}
			else
			{
				Sprite.RelativeY = 0f;
			}
			Sprite.ScaleX = 16f;
			Sprite.ScaleY = 16f;
			Sprite.PixelSize = 0.5f;
			Sprite.Texture = MainShip1;
			if (mCollision!= null && mCollision.Parent == null)
			{
				mCollision.CopyAbsoluteToRelative();
				mCollision.AttachTo(this, false);
			}
			Collision.Visible = false;
			if (Collision.Parent == null)
			{
				Collision.X = 0f;
			}
			else
			{
				Collision.RelativeX = 0f;
			}
			if (Collision.Parent == null)
			{
				Collision.Y = 0f;
			}
			else
			{
				Collision.RelativeY = 0f;
			}
			Collision.Radius = 10f;
			MovementSpeed = 100f;
			TurningSpeed = 3.14f;
			StartingHealth = 6;
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
			if (Sprite.Parent == null)
			{
				Sprite.X = 0f;
			}
			else
			{
				Sprite.RelativeX = 0f;
			}
			if (Sprite.Parent == null)
			{
				Sprite.Y = 0f;
			}
			else
			{
				Sprite.RelativeY = 0f;
			}
			Sprite.ScaleX = 16f;
			Sprite.ScaleY = 16f;
			Sprite.PixelSize = 0.5f;
			Sprite.Texture = MainShip1;
			ShapeManager.AddToLayer(mCollision, layerToAddTo);
			mCollision.Visible = false;
			if (mCollision.Parent == null)
			{
				mCollision.X = 0f;
			}
			else
			{
				mCollision.RelativeX = 0f;
			}
			if (mCollision.Parent == null)
			{
				mCollision.Y = 0f;
			}
			else
			{
				mCollision.RelativeY = 0f;
			}
			mCollision.Radius = 10f;
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
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("MainShipStaticUnload", UnloadStaticContent);
						mHasRegisteredUnload = true;
					}
				}
				bool registerUnload = false;
				if (!FlatRedBallServices.IsLoaded<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/mainship/mainship1.png", ContentManagerName))
				{
					registerUnload = true;
				}
				MainShip1 = FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/mainship/mainship1.png", ContentManagerName);
				if (!FlatRedBallServices.IsLoaded<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/mainship/mainship2.png", ContentManagerName))
				{
					registerUnload = true;
				}
				MainShip2 = FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/mainship/mainship2.png", ContentManagerName);
				if (!FlatRedBallServices.IsLoaded<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/mainship/mainship3.png", ContentManagerName))
				{
					registerUnload = true;
				}
				MainShip3 = FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/mainship/mainship3.png", ContentManagerName);
				if (!FlatRedBallServices.IsLoaded<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/mainship/mainship4.png", ContentManagerName))
				{
					registerUnload = true;
				}
				MainShip4 = FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/mainship/mainship4.png", ContentManagerName);
				if (registerUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
				{
					lock (mLockObject)
					{
						if (!mHasRegisteredUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
						{
							FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("MainShipStaticUnload", UnloadStaticContent);
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
			if (MainShip1 != null)
			{
				MainShip1= null;
			}
			if (MainShip2 != null)
			{
				MainShip2= null;
			}
			if (MainShip3 != null)
			{
				MainShip3= null;
			}
			if (MainShip4 != null)
			{
				MainShip4= null;
			}
		}
		public static object GetStaticMember (string memberName)
		{
			switch(memberName)
			{
				case  "MainShip1":
					return MainShip1;
				case  "MainShip2":
					return MainShip2;
				case  "MainShip3":
					return MainShip3;
				case  "MainShip4":
					return MainShip4;
			}
			return null;
		}
		object GetMember (string memberName)
		{
			switch(memberName)
			{
				case  "MainShip1":
					return MainShip1;
				case  "MainShip2":
					return MainShip2;
				case  "MainShip3":
					return MainShip3;
				case  "MainShip4":
					return MainShip4;
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
	public static class MainShipExtensionMethods
	{
	}
	
}
