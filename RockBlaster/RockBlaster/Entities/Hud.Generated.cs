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
	public partial class Hud : PositionedObject, IDestroyable
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
		
		private PositionedObjectList<HealthBar> HealthBarList;
		private FlatRedBall.Graphics.Text TextObject;
		public int Score
		{
			get
			{
				return int.Parse(TextObject.DisplayText);
			}
			set
			{
				TextObject.DisplayText = value.ToString();
			}
		}
		public float HealthBarY = 250f;
		public int Index { get; set; }
		public bool Used { get; set; }
		protected Layer LayerProvidedByContainer = null;

        public Hud(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public Hud(string contentManagerName, bool addToManagers) :
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
			HealthBarList = new PositionedObjectList<HealthBar>();
			TextObject = new FlatRedBall.Graphics.Text();
			
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
			
			for (int i = HealthBarList.Count - 1; i > -1; i--)
			{
				if (i < HealthBarList.Count)
				{
					// We do the extra if-check because activity could destroy any number of entities
					HealthBarList[i].Activity();
				}
			}
			CustomActivity();
			
			// After Custom Activity
		}

		public virtual void Destroy()
		{
			// Generated Destroy
			SpriteManager.RemovePositionedObject(this);
			
			for (int i = HealthBarList.Count - 1; i > -1; i--)
			{
				HealthBarList[i].Destroy();
			}
			if (TextObject != null)
			{
				TextObject.Detach(); TextManager.RemoveText(TextObject);
			}


			CustomDestroy();
		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			if (TextObject!= null && TextObject.Parent == null)
			{
				TextObject.CopyAbsoluteToRelative();
				TextObject.AttachTo(this, false);
			}
			if (TextObject.Parent == null)
			{
				TextObject.X = -375f;
			}
			else
			{
				TextObject.RelativeX = -375f;
			}
			if (TextObject.Parent == null)
			{
				TextObject.Y = 275f;
			}
			else
			{
				TextObject.RelativeY = 275f;
			}
			Score = 0;
			HealthBarY = 250f;
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
			TextManager.AddToLayer(TextObject, layerToAddTo);
			TextObject.SetPixelPerfectScale(layerToAddTo);
			if (TextObject.Parent == null)
			{
				TextObject.X = -375f;
			}
			else
			{
				TextObject.RelativeX = -375f;
			}
			if (TextObject.Parent == null)
			{
				TextObject.Y = 275f;
			}
			else
			{
				TextObject.RelativeY = 275f;
			}
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
			for (int i = 0; i < HealthBarList.Count; i++)
			{
				HealthBarList[i].ConvertToManuallyUpdated();
			}
			TextManager.ConvertToManuallyUpdated(TextObject);
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
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("HudStaticUnload", UnloadStaticContent);
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
							FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("HudStaticUnload", UnloadStaticContent);
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
			InstructionManager.IgnorePausingFor(TextObject);
		}

    }
	
	
	// Extra classes
	public static class HudExtensionMethods
	{
	}
	
}
