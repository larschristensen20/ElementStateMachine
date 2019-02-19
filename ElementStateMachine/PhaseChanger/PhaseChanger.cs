/*
Copyright (c) 2012, Ulrik Pagh Schultz, University of Southern Denmark
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met: 

1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer. 
2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution. 

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

The views and conclusions contained in the software and documentation are those
of the authors and should not be interpreted as representing official policies, 
either expressed or implied, of the University of Southern Denmark.
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ElementStateMachine
{
    /// <summary>
    /// The main class that instanciates the MachineExecutor class to interpret the MetaModel.
    /// The class also runs an experiment using a number of events, and prints diagnostics (run time) about the experiments.
    /// </summary>
    public class PhaseChanger
    {
        private static readonly int NEVENTS = 200000;

        private static List<Event> events_machine1 = MakeEventList("HEAT", "COOL", "SUPERCOOL", "SUPERHEAT");
        private static Event[] generatedEvents = GenerateEventList(events_machine1, NEVENTS, new Random());
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
            Console.WriteLine("\n" + "Test of Machine1 started with: "+NEVENTS+" random events");
            foreach (Event e in generatedEvents)
            {
                machine1.ProcessEvent(e);
            }
            stopwatch.Stop();
            TimeSpan ts1 = stopwatch.Elapsed;
            Console.WriteLine("Run time of Machine1: "+ts1);

            // Machine 2
            Console.WriteLine("\n" + "Test of Machine2 started with: " + NEVENTS + " random events");
            stopwatch = Stopwatch.StartNew();
            foreach (Event e in generatedEvents)
            {
                machine2.ProcessEvent(e);   
            }
            stopwatch.Stop();
            TimeSpan ts2 = stopwatch.Elapsed;
            Console.WriteLine("Run time of Machine2: " + ts2);
            Console.ReadKey();
        }
    }
}
