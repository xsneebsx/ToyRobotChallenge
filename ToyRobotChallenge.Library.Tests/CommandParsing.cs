using System;
using Xunit;

namespace ToyRobotChallenge.Library.Tests
{
    public class CommandParsing : BaseUnitTest
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void TestMethod1(string commands, bool throws)
        {
            if (throws)
            {
                Assert.ThrowsAny<ApplicationException>(() => CommandParser.Parse(commands));
            }
            else
            {
                CommandParser.Parse(commands);
            }
        }

        public static TheoryData<string, bool> Data =>
        new TheoryData<string, bool>
        {
            {
@"# Something simple to begin with...
echo Test 1 - Simple PLACE command
echo Expected output: 0,1,NORTH
PLACE 0,1,NORTH

# should be back to the beginning in the end.
VALIDATE 0,1,NORTH
REPORT
echo" , false},{
@"# Something simple to begin with...
echo Test 1 - Simple PLACE command
echo Expected output: 0,1,NORTH
PLACE 0,1,NORTH

# should be back to the beginning in the end.
VALIDATE 0,1,NORTH
REPORT
echo" , false },

                {
@"# single line
echo Test 2 - Simple PLACE, VALIDATION & REPORT on a single line
echo Expected output: 0,1,NORTH
PLACE 0,1,NORTH VALIDATE 0,1,NORTH REPORT
echo" , false },

                {
@"# single line, new line & tab command delimiters
echo Test 3 - Commands on multiple & single lines
echo Expected output: 0,1,NORTH
PLACE 0,1,NORTH				
MOVE
RIGHT RIGHT
MOVE
LEFT		LEFT
# should be back to the beginning in the end.
VALIDATE 0,1,NORTH
REPORT
echo" , false },

                {
@"# Simple failure due to bad case, command keywords or format
echo Test 4 - PLACE 0,1,SoUth command failure due to incorrect case
echo Expected output: 0,1,NORTH
PLACE 0,1,SoUth

# bad place, robot should should still be at 0,1,NORTH
VALIDATE 0,1,NORTH
REPORT
echo" , false },

                {
@"# Simple failure due to bad case, command keywords or format
echo Test 5 - Command failure due to base case, command or format
echo Expected output: 0,1,NORTH
PLACE 0,1,NORTH		
PLACE 0,two,NORTH	
PLACE 0,0,S		
MOVE
RIGHT RIGHT Right
M0VE Move move MOVE @!#$$
LEFT		LEFT
# should be back to the beginning in the end.
VALIDATE 0,1,NORTH
REPORT
echo" , false },

                {
@"# Lots of bad place commands
echo Test 6 - Lots of bad commands that shouldn't parse
echo Expected output: 2,2,EAST
PLACE 0,1,Norph
PLACE sdf,324,South
PlAcE 0,1,NORTH	
;asdlkdjf 
kdkdi9ek
# 1 good place at the end of some garbage
Pldldkkd dkfjaf ff fooalke PLACE 2,3 PLACE 2,2,EAST
VALIDATE 2,2,EAST
REPORT
echo" , false },

                {
@"# single line command
echo Test 7 - All commands on a single line
echo expected output: 0,1,NORTH
PLACE 0,1,NORTH MOVE RIGHT RIGHT MOVE LEFT LEFT VALIDATE 0,1,NORTH REPORT
echo" , false },
        };
    }
}
