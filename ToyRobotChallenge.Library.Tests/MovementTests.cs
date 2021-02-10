using System;
using Xunit;

namespace ToyRobotChallenge.Library.Tests
{
    public class MovementTests : BaseUnitTest
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
@"echo Test 1 - Simple PLACE, MOVE & TURN commands
echo Expected output: 2,2,WEST
PLACE 2,2,NORTH
RIGHT
MOVE
MOVE
LEFT
LEFT
MOVE
MOVE
VALIDATE 2,2,WEST
REPORT
echo"  , false},

                {
@"echo Test 2 - Travel clockwise around the edge of the table
echo Expected output: 0,3,NORTH
PLACE 0,4,NORTH
RIGHT
MOVE MOVE MOVE MOVE
RIGHT
MOVE MOVE MOVE MOVE
RIGHT
MOVE MOVE MOVE MOVE
RIGHT
MOVE MOVE MOVE
VALIDATE 0,3,NORTH
REPORT
echo" , false },

                {
@"echo Test 3 - Travel clockwise around the edge of the table
echo Expected output: 3,0,EAST
PLACE 4,0,SOUTH
LEFT LEFT
MOVE MOVE MOVE MOVE
LEFT
MOVE MOVE MOVE MOVE
LEFT
MOVE MOVE MOVE MOVE
LEFT
MOVE MOVE MOVE
VALIDATE 3,0,EAST
REPORT
echo" , false },

                {
@"echo Test 4 - Try to travel off the edge of the table
echo Expected output: 4,1,EAST & 4,4,NORTH
PLACE 1,1,EAST
MOVE MOVE MOVE
VALIDATE 4,1,EAST
# really try hard to go off the east edge!
MOVE MOVE MOVE MOVE MOVE MOVE MOVE MOVE MOVE
VALIDATE 4,1,EAST
REPORT
LEFT
MOVE MOVE MOVE
VALIDATE 4,4,NORTH
# really try hard to go off the north edge!
MOVE MOVE MOVE MOVE MOVE MOVE MOVE MOVE MOVE
VALIDATE 4,4,NORTH
REPORT
echo"  , false},

                {
@"echo expected output: 0,1,NORTH
PLACE 0,0,NORTH
MOVE
REPORT
VALIDATE 0,1,NORTH"  , false},

                {
@"echo expected output: 0,0,WEST
PLACE 0,0,NORTH
LEFT
REPORT
VALIDATE 0,0,WEST"  , false},


                {
@"echo expected output: 3,3,NORTH
PLACE 1,2,EAST
MOVE
MOVE
LEFT
MOVE
REPORT
VALIDATE 3,3,NORTH
"  , false},
        };
    }
}
