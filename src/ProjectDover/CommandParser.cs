using System;
using System.Collections;
using System.Collections.Specialized;

namespace ProjectDover
{
    public class CommandParser
    {
        private Hashtable gameCommands;
        //Load dictionary associate String to enum

        public CommandParser() { 
            gameCommands = LoadCommand();
            //var test 
        
        }



        public Command ProcessCommandText(string commandText)
        {
            Command currentCommand = (Command)gameCommands[commandText.TrimEnd().Split(' ')[0]];
            bool hasParameter = (commandText.TrimEnd().Split(' ').Length > 1);

            switch(currentCommand){

                case Command.COMMAND_QUIT: 

                case Command.COMMAND_NORTH:
                case Command.COMMAND_EAST:
                case Command.COMMAND_SOUTH:
                case Command.COMMAND_WEST:

                case Command.COMMAND_INVENTORY:
                case Command.COMMAND_SUMMARY:
                case Command.COMMAND_SAVE:
                    // TODO initialize Command class
                    break;
                case Command.COMMAND_EXIT:
                    Console.WriteLine("Did you mean `quit`?");
                    currentCommand = Command.COMMAND_HANDLED;
                    break;
                case Command.COMMAND_TAKE:
                    if(!hasParameter){
                        Console.WriteLine("Please specify what you want to take?");
                        currentCommand = Command.COMMAND_HANDLED;
                    }
                    break;
                default:
                    currentCommand = Command.UNKNOWN;
                    break;

            }
            return currentCommand;


            // if (commandText.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
            // {
                
            //     return Command.COMMAND_HANDLED;
            // }

            // if (commandText.Equals("QUIT", StringComparison.OrdinalIgnoreCase))
            // {
            //     //No shortcut to avoid quitting by accident.
            //     return Command.COMMAND_QUIT;
            // }

            // if (commandText.Equals("LOOK", StringComparison.OrdinalIgnoreCase)
            //     || commandText.Equals("L", StringComparison.OrdinalIgnoreCase))
            // {
            //     return Command.COMMAND_LOOK;
            // }

            // if (commandText.Equals("NORTH", StringComparison.OrdinalIgnoreCase) 
            //     || commandText.Equals("N", StringComparison.OrdinalIgnoreCase))
            // {
            //     return Command.COMMAND_NORTH;
            // }
            // if (commandText.Equals("SOUTH", StringComparison.OrdinalIgnoreCase)
            //     || commandText.Equals("S", StringComparison.OrdinalIgnoreCase))
            // {
            //     return Command.COMMAND_SOUTH;
            // }
            // if (commandText.Equals("EAST", StringComparison.OrdinalIgnoreCase)
            //     || commandText.Equals("E", StringComparison.OrdinalIgnoreCase))
            // {
            //     return Command.COMMAND_EAST;
            // }
            // if (commandText.Equals("WEST", StringComparison.OrdinalIgnoreCase)
            //     || commandText.Equals("W", StringComparison.OrdinalIgnoreCase))
            // {
            //     return Command.COMMAND_WEST;
            // }
            // if (commandText.Equals("INVENTORY", StringComparison.OrdinalIgnoreCase)
            //     || commandText.Equals("I", StringComparison.OrdinalIgnoreCase))
            // {
            //     return Command.COMMAND_INVENTORY;
            // }

            // if (commandText.Equals("SUMMARY", StringComparison.OrdinalIgnoreCase)
            //     || commandText.Equals("X", StringComparison.OrdinalIgnoreCase))
            // {
            //     return Command.COMMAND_SUMMARY;
            // }

            // if (commandText.Equals("SAVE", StringComparison.OrdinalIgnoreCase)
            //     || commandText.Equals("V", StringComparison.OrdinalIgnoreCase))
            // {
            //     return Command.COMMAND_SAVE;
            // }

            // == Interaction commands ====
            // if(commandText.StartsWith("TAKE", StringComparison.OrdinalIgnoreCase)
            //     || commandText.Equals("T", StringComparison.OrdinalIgnoreCase)
            //     || commandText.StartsWith("T ", StringComparison.OrdinalIgnoreCase)){

            //     if(commandText.TrimEnd().Split(' ').Length > 1){
            //         return Command.COMMAND_TAKE;
            //     }
            //     Console.WriteLine("What do you want to take?");
            //     return Command.COMMAND_HANDLED;
            // }


            //return Command.UNKNOWN;
        }
    
        private Hashtable LoadCommand(){
            var txtCommands = new Hashtable{

                {"N" , Command.COMMAND_NORTH},
                {"NORTH" , Command.COMMAND_NORTH},

                {"E" , Command.COMMAND_EAST},
                {"EAST" , Command.COMMAND_EAST},

                {"S" , Command.COMMAND_SOUTH},
                {"SOUTH" , Command.COMMAND_SOUTH},

                {"W" , Command.COMMAND_WEST},
                {"WEST" , Command.COMMAND_WEST},

                {"I" , Command.COMMAND_INVENTORY},
                {"INVENTORY" , Command.COMMAND_INVENTORY},

                {"X" , Command.COMMAND_SUMMARY},
                {"SUMMARY" , Command.COMMAND_SUMMARY},

                {"L" , Command.COMMAND_LOOK},
                {"LOOK" , Command.COMMAND_LOOK},

                {"T" , Command.COMMAND_TAKE},
                {"TAKE" , Command.COMMAND_TAKE},


                {"Q" , Command.COMMAND_QUIT},
                {"QUIT" , Command.COMMAND_QUIT},
                {"EXIT" , Command.COMMAND_EXIT},
            };


            return txtCommands;
        }
    }

}
