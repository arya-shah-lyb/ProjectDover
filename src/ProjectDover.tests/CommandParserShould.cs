using System;
using Xunit;

namespace ProjectDover.tests
{
    public class CommandParserShould
    {
        private readonly CommandParser _cmdParser;

        public CommandParserShould() {
             _cmdParser = new CommandParser();
        }

        [Fact]
        public void IsParser_InputQuit_ReturnQuit()
        {

            Assert.Equal(Command.COMMAND_QUIT, _cmdParser.ProcessCommandText("Quit"));

            Assert.Equal(Command.UNKNOWN, _cmdParser.ProcessCommandText("Q"));

            Assert.Equal(Command.UNKNOWN, _cmdParser.ProcessCommandText("q"));

        }

        [Fact]
        public void IsParser_InputLook_ReturLook()
        {
            Assert.Equal(Command.COMMAND_LOOK, _cmdParser.ProcessCommandText("Look"));

            Assert.Equal(Command.COMMAND_LOOK, _cmdParser.ProcessCommandText("L"));

            Assert.Equal(Command.COMMAND_LOOK, _cmdParser.ProcessCommandText("l"));

        }

        [Fact]
        public void IsParser_InputNorth_ReturnNorth()
        {
            Assert.Equal(Command.COMMAND_NORTH, _cmdParser.ProcessCommandText("North"));

            Assert.Equal(Command.COMMAND_NORTH, _cmdParser.ProcessCommandText("N"));

            Assert.Equal(Command.COMMAND_NORTH, _cmdParser.ProcessCommandText("n"));

        }

        [Fact]
        public void IsParser_InputSouth_ReturnSouth()
        {
            Assert.Equal(Command.COMMAND_SOUTH, _cmdParser.ProcessCommandText("South"));

            Assert.Equal(Command.COMMAND_SOUTH, _cmdParser.ProcessCommandText("S"));

            Assert.Equal(Command.COMMAND_SOUTH, _cmdParser.ProcessCommandText("s"));

        }

        [Fact]
        public void IsParser_InputWest_ReturnWest()
        {
            Assert.Equal(Command.COMMAND_WEST, _cmdParser.ProcessCommandText("West"));

            Assert.Equal(Command.COMMAND_WEST, _cmdParser.ProcessCommandText("W"));

            Assert.Equal(Command.COMMAND_WEST, _cmdParser.ProcessCommandText("w"));

        }

        [Fact]
        public void IsParser_InputEast_ReturnEast()
        {
            Assert.Equal(Command.COMMAND_EAST, _cmdParser.ProcessCommandText("East"));

            Assert.Equal(Command.COMMAND_EAST, _cmdParser.ProcessCommandText("E"));

            Assert.Equal(Command.COMMAND_EAST, _cmdParser.ProcessCommandText("e"));

        }

        [Fact]
        public void IsParser_InputExit_ReturnHandle()
        {
            Assert.Equal(Command.COMMAND_HANDLED, _cmdParser.ProcessCommandText("Exit"));

            Assert.Equal(Command.COMMAND_HANDLED, _cmdParser.ProcessCommandText("EXIT"));
        }

        [Fact]
        public void IsParser_InputUnknowChar_ReturnUnknow()
        {
            Assert.Equal(Command.UNKNOWN, _cmdParser.ProcessCommandText("Frank"));

            Assert.Equal(Command.UNKNOWN, _cmdParser.ProcessCommandText("test"));

            Assert.Equal(Command.UNKNOWN, _cmdParser.ProcessCommandText("r"));

            Assert.Equal(Command.UNKNOWN, _cmdParser.ProcessCommandText("u"));
        }

        [Fact]
        public void IsParser_InputTake_ReturnTake()
        {
            // Take without spesifing what

            Assert.Equal(Command.COMMAND_HANDLED, _cmdParser.ProcessCommandText("Take"));

            Assert.Equal(Command.COMMAND_HANDLED, _cmdParser.ProcessCommandText("t"));

            Assert.Equal(Command.COMMAND_HANDLED, _cmdParser.ProcessCommandText("T"));

            Assert.Equal(Command.COMMAND_HANDLED, _cmdParser.ProcessCommandText("t "));

            Assert.Equal(Command.COMMAND_HANDLED, _cmdParser.ProcessCommandText("T "));

            // Correct Take command
            Assert.Equal(Command.COMMAND_TAKE, _cmdParser.ProcessCommandText("Take phone"));

            Assert.Equal(Command.COMMAND_TAKE, _cmdParser.ProcessCommandText("t phone"));

            Assert.Equal(Command.COMMAND_TAKE, _cmdParser.ProcessCommandText("T phone"));
        }
    }
}
