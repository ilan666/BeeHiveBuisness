﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHiveBuisness
{
    class Queen : Bee
    {
        public string StatusReport { get; private set; }
        public override float CostPerShift { get { return 2.15f; } }
        private float eggs = 0;
        private float unassignedWorkers = 3;
        const float EGGS_PER_SHIFT = 0.45f;
        const float HONEY_PER_UNASSIGNED_WORKER = 0.5f;

        private Bee[] workers = new Bee[0];

        public Queen() : base("Queen") 
        {
            AssignBee("Nectar Collector");
            AssignBee("Honey Manufacturer");
            AssignBee("Egg Care");
        }

        public void AssignBee(string job)
        {
            switch (job)
            {
                case "Egg Care":
                    AddWorker(new EggCare(this));
                    break;

                case "Nectar Collector":
                    AddWorker(new NectarCollector());
                    break;

                case "Honey Manufacturer":
                    AddWorker(new HoneyManufacturer());
                    break;

            }
        }

        public void CareForEggs(float eggsToConvert)
        {
            if (eggs >= eggsToConvert)
            {
                eggs -= eggsToConvert;
                unassignedWorkers += eggsToConvert;
            }
        }

        protected override void DoJob()
        {
            eggs += EGGS_PER_SHIFT;

            foreach (Bee worker in workers)
            {
                worker.WorkNextShift();
            }

            HoneyVault.ConsumeHoney(HONEY_PER_UNASSIGNED_WORKER * workers.Length);

            UpdateStatusReport();
        }

        private void UpdateStatusReport()
        {
            StatusReport = $"Vault report:\n {HoneyVault.StatusReport}\n" +
            $"Egg count: {eggs:0.0}\nUnassigned workers: {unassignedWorkers:0.0}\n" +
            $"{WorkerStatus("Nectar Collector")}\n {WorkerStatus("Honey Manufacturer")}" +
            $"\n{WorkerStatus("Egg care")}\n TOTAL WORKERS: {workers.Length}";
        }

        public void AddWorker(Bee worker)
        {
            if(unassignedWorkers >= 1)
            {
                unassignedWorkers--;
                Array.Resize(ref workers, workers.Length + 1);
                workers[workers.Length - 1] = worker;
            }
        }

        private string WorkerStatus(string job)
        {
            int count = 0;
            foreach (Bee bee in workers)
            {
                if (bee.Job == job) count++;
            }
            return count == 1 ? $"{count} {job} bee" : $"{count} {job} bees";
        }
    }
}
