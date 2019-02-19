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
namespace ElementStateMachine
{
    /// <summary>
    /// A DSL script for building a state machine
    /// </summary>
    public class PhaseChangerMachine1 : FluentMachine
    {
        /// <summary>
        /// Builds the machine based on the DSL script
        /// </summary>
        protected override void Build()
        {
            State("SOLID").
                Transition("HEAT").To("LIQUID").
                Transition("SUPERHEAT").To("GAS").
                Transition("HEAT").To("GLASS").
            State("GLASS").
                Transition("COOL").To("GLASS").
                Transition("HEAT").To("LIQUID").
            State("LIQUID").
                Transition("COOL").To("SOLID").
                Transition("COOL").To("GLASS").
                Transition("SUPERCOOL").To("SUPERFLUID").
                Transition("HEAT").To("GAS").
            State("SUPERFLUID").
                Transition("SUPERHEAT").To("LIQUID").
            State("GAS").
                Transition("COOL").To("LIQUID").
                Transition("SUPERCOOL").To("SOLID").
                Transition("SUPERHEAT").To("PLASMA").
                Transition("SUPERCOOL").To("BOSE EINSTEIN CONDENSATE").
                Transition("HEAT").To("PLASMA").
            State("BOSE EINSTEIN CONDENSATE").
                Transition("SUPERHEAT").To("GAS").
            State("PLASMA").
                Transition("COOL").To("GAS").
                Transition("SUPERHEAT").To("QUARK-GLUON PLASMA").
            State("QUARK-GLUON PLASMA").
                Transition("SUPERCOOL").To("PLASMA");
                
        }
    }
}
