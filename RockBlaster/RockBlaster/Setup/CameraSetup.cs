using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using Microsoft.Xna.Framework;

namespace RockBlaster
{
	internal static class CameraSetup
	{
		internal static void SetupCamera(Camera cameraToSetUp, GraphicsDeviceManager graphicsDeviceManager)
		{
			#if !WINDOWS_PHONE
			FlatRedBallServices.GraphicsOptions.SetResolution(800, 600);
			#else
			graphicsDeviceManager.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
			#endif
			cameraToSetUp.UsePixelCoordinates(false, 800, 600);



		}
	}
}
