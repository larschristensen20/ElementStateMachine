##  First assignment for the MDSD course at University of Southern Denmark, 2nd semester of the Master in Software Engineering.
This repository features the first assignment for the Model-Driven Software Development course at University of Southern Denmark.
### How to run it
Unzip the project folder, and run the application using the ElementStateMachine.exe found in <yourUnzipLocation>\ElementStateMachine\ElementStateMachine\bin\Release\netcoreapp2.1\win10-x64\publish
  or
Clone the repository from [here](https://github.com/larschristensen20/ElementStateMachine), and build the project using Visual Studio 2017.


### Description
This project implements a fluent interface for making an internal DSL for a state machine within the context of matter state changes.
The project features a metamodel of states, transitions and events, as well as a builder (the FluentMachine), and a MachineExecutor (interpreter).
The project itself is based heavily on Ulrik Pagh Schultz work in his [repository](https://github.com/ulrikpaghschultz/MDSD)

### Evaluation
The project itself is quite simple, as I have not opted to use the extended states, also featured in the project. I hope that this version is adequate enough to be able to pass the assignment. In the future, it could be quite interesting to try and evolve the project to include the extended states, and add transitions based on temperature changes, where different temperatures could results in different transitions. 
