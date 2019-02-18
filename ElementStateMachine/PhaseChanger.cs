using System;
using System.Collections.Generic;
using System.Diagnostics;
using static ElementStateMachine.FluentMachine;

namespace ElementStateMachine
{
    class PhaseChanger
    {
        private static List<Event> events_machine1 = MakeEventList("HEAT", "COOL", "SUPERCOOL", "SUPERHEAT");
        private static Event[] generatedEvents = GenerateEventList(events_machine1, 20, new Random());
        private static Event[] GenerateEventList(List<Event> events, int nevents, Random rand)
        {
            Event[] result = new Event[nevents];
            for (int i = 0; i < nevents; i++)
                result[i] = events[rand.Next(events.Count)];
            return result;
        }

        private static List<Event> MakeEventList(params string[] names)
        {
            List<Event> events = new List<Event>();
            for (int i = 0; i < names.Length; i++) events.Add(new Event(names[i]));
            return events;
        }

        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            
            MachineExecutor<GenericRuntimeState> machine1 = new MachineExecutor<GenericRuntimeState>(new PhaseChangerMachine1().GetMetaModel());
            MachineExecutor<GenericRuntimeState> machine2  = new MachineExecutor<GenericRuntimeState>(new PhaseChangerMachine2().GetMetaModel());
            machine1.Initialize();
            machine2.Initialize();

            // Machine 1
            stopwatch = Stopwatch.StartNew();
            Console.WriteLine("Machine1");
            foreach (Event e in generatedEvents)
            {
                machine1.ProcessEvent(e);
                
                Console.WriteLine(machine1.GetStateName());
            }
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            Console.WriteLine("Time for machine 1: "+ts + "\n");

            // Machine 2
            Console.WriteLine("Machine2");
            stopwatch = Stopwatch.StartNew();
            foreach (Event e in generatedEvents)
            {
                Console.WriteLine(machine2.GetStateName());
                machine2.ProcessEvent(e);
                
            }
            stopwatch.Stop();
            TimeSpan ts1 = stopwatch.Elapsed;
            Console.WriteLine("Time for machine 1: " + ts1 +"\n");
            Console.ReadKey();
        }
    }
}
