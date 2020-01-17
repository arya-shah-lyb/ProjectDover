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
            string[] strInputs = commandText.ToUpper().TrimEnd().Split(' ');
            Command currentCommand = (Command)gameCommands[strInputs[0]];
            bool hasParameter = (strInputs.Length > 1);

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
