using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHiveBuisness
{
    static class HoneyVault
    {
        const float NECTAR_CONVERSION_RATIO = .19f;
        const float LOW_LEVEL_WARNING = 10f;

        public static string StatusReport { get 
            {
                string status = $"{honey} units of honey\n" + $"{nectar} units of nectar";

                string warnings = "";

                if(honey < LOW_LEVEL_WARNING)
                {
                    warnings += "\nLOW HONEY - ADD A HONEY MANUFACTURER";
                }

                if(nectar < LOW_LEVEL_WARNING)
                {
                    warnings += "\nLOW NECTAR - ADD A NECTAR COLLECTOR";
                }

                return status + warnings;
            } }
        static private float honey = 25f;
        static private float nectar = 100f;

        public static void CollectNectar(float amount)
        {
            if (amount > 0) nectar += amount;
        }

        public static void ConvertNectarToHoney(float amount)
        {
            if (amount > nectar) amount = nectar;
            nectar -= amount;
            honey += amount * NECTAR_CONVERSION_RATIO;
        }

        public static bool ConsumeHoney(float amount)
        {
            if(honey >= amount)
            {
                honey -= amount;
                return true;
            }

            return false;
        }
    }
}
