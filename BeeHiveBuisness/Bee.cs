using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHiveBuisness
{
    abstract class Bee
    {
        public string Job { get; private set; }

        public abstract float CostPerShift { get; }

        public Bee(string job)
        {
            Job = job;
        }

        public void WorkNextShift() 
        {
            if (HoneyVault.ConsumeHoney(CostPerShift))
            {
                DoJob();
            }
        }

        protected abstract void DoJob();
    }
}
