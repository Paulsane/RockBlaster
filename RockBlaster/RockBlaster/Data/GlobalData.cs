using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RockBlaster.Data
{
    public static class GlobalData
    {
        public static PlayerData PlayerData
        {
            get;
            private set;
        }

        public static void Initialize()
        {
            PlayerData = new PlayerData();
        }
    }
}
