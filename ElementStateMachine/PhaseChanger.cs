using System;
using System.Collections.Generic;
using static ElementStateMachine.FluentMachine;

namespace ElementStateMachine
{
    class PhaseChanger
    {
        private Event[] events;

        private static List<Event> events_machine1 = makeEventList("HEAT", "COOL", "SUPERCOOL", "SUPERHEAT");
        private List<Event> events_machine2 = makeEventList("HEAT", "COOL", "SUPERHEAT", "SUPERCOOL");

        private static Event[] generatedEvents = generateEventList(events_machine1, 20, new Random(8));

        private static Event[] generateEventList(List<Event> events, int nevents, Random rand)
        {
            Event[] result = new Event[nevents];
            for (int i = 0; i < nevents; i++)
                result[i] = events[rand.Next(events.Count)];
            return result;
        }

        private static List<Event> makeEventList(params string[] names)
        {
            List<Event> events = new List<Event>();
            for (int i = 0; i < names.Length; i++) events.Add(new Event(names[i]));
            return events;
        }

        static void Main(string[] args)
        {
           MachineExecutor<GenericRuntimeState> machine1 = new MachineExecutor<GenericRuntimeState>(new PhaseChangerMachine2().GetMetaModel());
           MachineExecutor<GenericRuntimeState> machine2 = new MachineExecutor<GenericRuntimeState>(new PhaseChangerMachine1().GetMetaModel());

            machine1.Initialize();
            machine2.Initialize();
            

            foreach (Event e in generatedEvents)
            {
                machine1.ProcessEvent(e);
            }
        }
    }
}
