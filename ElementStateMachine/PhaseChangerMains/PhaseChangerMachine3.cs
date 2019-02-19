using System;
using System.Collections.Generic;
using System.Text;

namespace ElementStateMachine
{
    /// <summary>
    /// A DSL script for building a state machine
    /// </summary>
    public class PhaseChangerMachine3 : FluentMachine
    {
        /// <summary>
        /// Builds the machine based on the DSL script
        /// This machine is not built on a real life system/phenomenon
        /// </summary>
        protected override void Build()
        {
            State("CRYSTAL").
                Transition("HEAT").To("LIQUID CRYSTAL").
            State("LIQUID CRYSTAL").
                Transition("COOL").To("CRYSTAL").
                Transition("HEAT").To("FERMIONIC CONDENSATE").
            State("FERMIONIC CONDENSATE").
                Transition("COOL").To("LIQUID CRYSTAL").
                Transition("SUPERCOOL").To("CRYSTAL").
                Transition("SUPERHEAT").To("QUANTUM HALL STATE").
                Transition("HEAT").To("RYDBERG MOLECULE").
            State("RYDBERG MOLECULE").
                Transition("COOL").To("FERMIONIC CONDENSATE").
                Transition("SUPERCOOL").To("CRYSTAL").
                Transition("SUPERHEAT").To("PHOTONIC MATTER").
                Transition("HEAT").To("QUANTUM HALL STATE").
            State("QUANTUM HALL STATE").
                Transition("COOL").To("RYDBERG MOLECULE").
                Transition("SUPERCOOL").To("LIQUID CRYSTAL").
                Transition("SUPERHEAT").To("DEGENERATE MATTER").
                Transition("HEAT").To("DROPLETON").
            State("PHOTONIC MATTER").
                Transition("COOL").To("RYDBERG MOLECULE").
                Transition("SUPERCOOL").To("FERMIONIC CONDENSATE").
                Transition("SUPERHEAT").To("DEGENERATE MATTER").
                Transition("HEAT").To("DROPLETON").
            State("DROPLETON").
                Transition("COOL").To("PHOTONIC MATTER").
                Transition("SUPERCOOL").To("QUANTUM HALL STATE").
                Transition("SUPERHEAT").To("QUARK MATTER").
                Transition("HEAT").To("DEGENERATE MATTER").
            State("DEGENERATE MATTER").
                Transition("COOL").To("DROPLETON").
                Transition("SUPERCOOL").To("PHOTONIC MATTER").
                Transition("SUPERHEAT").To("SUPERSOLID").
                Transition("HEAT").To("QUARK MATTER").
            State("QUARK MATTER").
                Transition("COOL").To("DEGENERATE MATTER").
                Transition("SUPERCOOL").To("DROPLETON").
                Transition("SUPERHEAT").To("SUPERSOLID").
                Transition("HEAT").To("COLOR-GASS CONDENSATE").
            State("COLOR-GASS CONDENSATE").
                Transition("COOL").To("QUARK MATTER").
                Transition("SUPERCOOL").To("DROPLETON").
                Transition("SUPERHEAT").To("STRING-NET LIQUID").
                Transition("HEAT").To("SUPERSOLID").
            State("SUPERSOLID").
                Transition("COOL").To("COLOR-GASS CONDENSATE").
                Transition("SUPERCOOL").To("QUARK MATTER").
                Transition("SUPERHEAT").To("SUPERGLASS").
                Transition("HEAT").To("STRING-NET LIQUID").
            State("STRING-NET LIQUID").
                Transition("COOL").To("SUPERSOLID").
                Transition("SUPERCOOL").To("COLOR-GASS CONDENSATE").
                Transition("SUPERHEAT").To("DARK MATTER").
                Transition("HEAT").To("SUPERGLASS").
            State("SUPERGLASS").
                Transition("COOL").To("STRING-NET LIQUID").
                Transition("SUPERCOOL").To("SUPERSOLID").
                Transition("HEAT").To("DARK MATTER").
            State("DARK MATTER").
                Transition("COOL").To("SUPERGLASS").
                Transition("SUPERCOOL").To("STRING-NET LIQUID");
        }
    }
}
