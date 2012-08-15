using RockBlaster.Entities;
using System;
using FlatRedBall.Math;
using FlatRedBall.Graphics;
using RockBlaster.Performance;

namespace RockBlaster.Factories
{
	public static class RockFactory
	{
		public static string mContentManagerName;
		static PositionedObjectList<Rock> mScreenListReference;
		static PoolList<Rock> mPool = new PoolList<Rock>();
		public static Action<Rock> EntitySpawned;
		public static Rock CreateNew ()
		{
			return CreateNew(null);
		}
		public static Rock CreateNew (Layer layer)
		{
			if (string.IsNullOrEmpty(mContentManagerName))
			{
				throw new System.Exception("You must first initialize the factory to use it.");
			}
			Rock instance = null;
			instance = new Rock(mContentManagerName, false);
			instance.AddToManagers(layer);
			if (mScreenListReference != null)
			{
				mScreenListReference.Add(instance);
			}
			if (EntitySpawned != null)
			{
				EntitySpawned(instance);
			}
			return instance;
		}
		
		public static void Initialize (PositionedObjectList<Rock> listFromScreen, string contentManager)
		{
			mContentManagerName = contentManager;
			mScreenListReference = listFromScreen;
		}
		
		public static void Destroy ()
		{
			mContentManagerName = null;
			mScreenListReference = null;
			mPool.Clear();
			EntitySpawned = null;
		}
		
		private static void FactoryInitialize ()
		{
			const int numberToPreAllocate = 20;
			for (int i = 0; i < numberToPreAllocate; i++)
			{
				Rock instance = new Rock(mContentManagerName, false);
				mPool.AddToPool(instance);
			}
		}
		
		public static void MakeUnused (Rock objectToMakeUnused)
		{
			MakeUnused(objectToMakeUnused, true);
		}
		
		public static void MakeUnused (Rock objectToMakeUnused, bool callDestroy)
		{
			objectToMakeUnused.Destroy();
		}
		
		
	}
}
