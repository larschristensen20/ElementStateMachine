First assignment for the MDSD course at University of Southern Denmark, 2 semester of the Master in Software Engineering.

The description from Blackboard:
I want to create a state machine, that models the different states elements can be in; Gas, ice, liquid, plasma. Specifically i will use Water as the example for the assignment, but it should be able to handle any element. (Even though water strictly speaking is not a chemical element, but is actually a compound made of two elements (2 hydrogen atoms, and 1 oxygen). I will use C# as my host language, where i will, as in Ulrik's example, create a control program to switch between the described states. As discussed in class with Ulrik, i won't be spending time making a nice GUI or even command-line control for the program, at first it will simply feature hardcoded states it will transverse. An order of the states, following the amount of heat needed to switch between states, could be as follows:
SOLID (0)
LIQUID (1)
GAS (2)
PLASMA (3)
This way I can use the simple plus/minus (heat/cool/supercool) structure, moving between lower states (ice,liquid) to higher states (vapor, plasma), i will however try to use a hierarchical state machine to take care of the transition between vapor directly to ice, without first transitioning to a liquid state.


An example of the DSL for this using method chaining, following Ulrik's code, could be something like:

state("PLASMA").
transition("COOL").to("GAS").setState("gas",2).
 
state("GAS").
transition("HEAT").to("PLASMA").setState("plasma",3).otherwise().transition("SUPERCOOL").to("SOLID").setState("solid",0).otherwise().transition("COOL").to("LIQUID").setState("liquid",1)
