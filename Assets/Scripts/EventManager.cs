using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class EventManager
    {
        public delegate void PlaneReachedCity();

        public static event PlaneReachedCity OnPlaneReachedCity;

        public static void RaiseOnPlaneReachedCity()
        {
            if (OnPlaneReachedCity != null)
                OnPlaneReachedCity();
        }
    }
}
