using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.Math.Geometry;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Input;
using FlatRedBall.IO;
using FlatRedBall.Instructions;
using FlatRedBall.Math.Splines;
using FlatRedBall.Utilities;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if XNA4
using Color = Microsoft.Xna.Framework.Color;
#else
using Color = Microsoft.Xna.Framework.Graphics.Color;
#endif

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Microsoft.Xna.Framework.Media;
#endif

// Generated Usings
using FlatRedBall.Broadcasting;
using RockBlaster.Entities;
using RockBlaster.Factories;
using FlatRedBall;
using FlatRedBall.Math;

namespace RockBlaster.Screens
{
	public partial class GameScreen : Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		
		private PositionedObjectList<Bullet> BulletList;
		private PositionedObjectList<Rock> RockList;
		private PositionedObjectList<MainShip> MainShipList;
		private RockBlaster.Entities.MainShip Player1Ship;
		private RockBlaster.Entities.EndGameUi EndGameUiInstance;
		private RockBlaster.Entities.Hud HudInstance;

		public GameScreen()
			: base("GameScreen")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			BulletList = new PositionedObjectList<Bullet>();
			RockList = new PositionedObjectList<Rock>();
			MainShipList = new PositionedObjectList<MainShip>();
			Player1Ship = new RockBlaster.Entities.MainShip(ContentManagerName, false);
			Player1Ship.Name = "Player1Ship";
			EndGameUiInstance = new RockBlaster.Entities.EndGameUi(ContentManagerName, false);
			EndGameUiInstance.Name = "EndGameUiInstance";
			HudInstance = new RockBlaster.Entities.Hud(ContentManagerName, false);
			HudInstance.Name = "HudInstance";
			MainShipList.Add(Player1Ship);
			
			
			PostInitialize();
			base.Initialize(addToManagers);
			if (addToManagers)
			{
				AddToManagers();
			}

        }
        
// Generated AddToManagers
		public override void AddToManagers ()
		{
			BulletFactory.Initialize(BulletList, ContentManagerName);
			base.AddToManagers();
			AddToManagersBottomUp();
			CustomInitialize();
		}


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if (!IsPaused)
			{
				
				for (int i = BulletList.Count - 1; i > -1; i--)
				{
					if (i < BulletList.Count)
					{
						// We do the extra if-check because activity could destroy any number of entities
						BulletList[i].Activity();
					}
				}
				for (int i = RockList.Count - 1; i > -1; i--)
				{
					if (i < RockList.Count)
					{
						// We do the extra if-check because activity could destroy any number of entities
						RockList[i].Activity();
					}
				}
				for (int i = MainShipList.Count - 1; i > -1; i--)
				{
					if (i < MainShipList.Count)
					{
						// We do the extra if-check because activity could destroy any number of entities
						MainShipList[i].Activity();
					}
				}
				EndGameUiInstance.Activity();
				HudInstance.Activity();
			}
			else
			{
			}
			base.Activity(firstTimeCalled);
			if (!IsActivityFinished)
			{
				CustomActivity(firstTimeCalled);
			}


				// After Custom Activity
				
            
		}

		public override void Destroy()
		{
			// Generated Destroy
			BulletFactory.Destroy();
			
			for (int i = BulletList.Count - 1; i > -1; i--)
			{
				BulletList[i].Destroy();
			}
			for (int i = RockList.Count - 1; i > -1; i--)
			{
				RockList[i].Destroy();
			}
			for (int i = MainShipList.Count - 1; i > -1; i--)
			{
				MainShipList[i].Destroy();
			}
			if (EndGameUiInstance != null)
			{
				EndGameUiInstance.Destroy();
				EndGameUiInstance.Detach();
			}
			if (HudInstance != null)
			{
				HudInstance.Destroy();
				HudInstance.Detach();
			}

			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp ()
		{
			Player1Ship.AddToManagers(mLayer);
			EndGameUiInstance.AddToManagers(mLayer);
			HudInstance.AddToManagers(mLayer);
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			for (int i = 0; i < BulletList.Count; i++)
			{
				BulletList[i].ConvertToManuallyUpdated();
			}
			for (int i = 0; i < RockList.Count; i++)
			{
				RockList[i].ConvertToManuallyUpdated();
			}
			for (int i = 0; i < MainShipList.Count; i++)
			{
				MainShipList[i].ConvertToManuallyUpdated();
			}
			EndGameUiInstance.ConvertToManuallyUpdated();
			HudInstance.ConvertToManuallyUpdated();
		}
		public static void LoadStaticContent (string contentManagerName)
		{
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
			RockBlaster.Entities.EndGameUi.LoadStaticContent(contentManagerName);
			RockBlaster.Entities.Hud.LoadStaticContent(contentManagerName);
			CustomLoadStaticContent(contentManagerName);
		}
		object GetMember (string memberName)
		{
			return null;
		}


	}
}
