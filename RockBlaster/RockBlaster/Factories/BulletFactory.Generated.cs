using RockBlaster.Entities;
using System;
using FlatRedBall.Math;
using FlatRedBall.Graphics;
using RockBlaster.Performance;

namespace RockBlaster.Factories
{
	public static class BulletFactory
	{
		public static string mContentManagerName;
		static PositionedObjectList<Bullet> mScreenListReference;
		static PoolList<Bullet> mPool = new PoolList<Bullet>();
		public static Action<Bullet> EntitySpawned;
		public static Bullet CreateNew ()
		{
			return CreateNew(null);
		}
		public static Bullet CreateNew (Layer layer)
		{
			if (string.IsNullOrEmpty(mContentManagerName))
			{
				throw new System.Exception("You must first initialize the factory to use it.");
			}
			Bullet instance = null;
			instance = new Bullet(mContentManagerName, false);
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
		
		public static void Initialize (PositionedObjectList<Bullet> listFromScreen, string contentManager)
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
				Bullet instance = new Bullet(mContentManagerName, false);
				mPool.AddToPool(instance);
			}
		}
		
		public static void MakeUnused (Bullet objectToMakeUnused)
		{
			MakeUnused(objectToMakeUnused, true);
		}
		
		public static void MakeUnused (Bullet objectToMakeUnused, bool callDestroy)
		{
			objectToMakeUnused.Destroy();
		}
		
		
	}
}
