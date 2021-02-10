using System;
using Xunit;

namespace ToyRobotChallenge.Library.Tests
{
    public class PlacementTests : BaseUnitTest
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
VALIDATE 0,1,NORTH
REPORT
echo"  , false},

                 {
@"# Should report an error
echo Test 2 - Validation should fail.  Robot is at 1,1,WEST, but validation of 2,2,WEST happens
echo Expected output: Position validation should fail
PLACE 1,1,WEST
VALIDATE 2,2,WEST
REPORT
echo"  , true},

                 {
@"# Lots of the same placement
echo Test 3 - PLACE command executed for the same position multiple times
echo Expected output: 2,2,WEST
PLACE 2,2,WEST
PLACE 2,2,WEST
PLACE 2,2,WEST
PLACE 2,2,WEST
PLACE 2,2,WEST
PLACE 2,2,WEST
PLACE 2,2,WEST
PLACE 2,2,WEST
PLACE 2,2,WEST
PLACE 2,2,WEST
PLACE 2,2,WEST
VALIDATE 2,2,WEST
REPORT
echo"  , false},

                 {
@"# Lots of varying placement
echo Test 4 - PLACE command executed for all positions on a 5x5 board
echo Expected output: 4,4,SOUTH
PLACE 0,0,WEST
PLACE 1,0,WEST
PLACE 2,0,WEST
PLACE 3,0,WEST
PLACE 4,0,WEST
PLACE 0,1,WEST
PLACE 1,1,WEST
PLACE 2,1,WEST
PLACE 3,1,WEST
PLACE 4,1,WEST
PLACE 0,2,WEST
PLACE 1,2,WEST
PLACE 2,2,WEST
PLACE 3,2,WEST
PLACE 4,2,WEST
PLACE 0,3,WEST
PLACE 1,3,WEST
PLACE 2,3,WEST
PLACE 3,3,WEST
PLACE 4,3,WEST
PLACE 0,4,WEST
PLACE 1,4,WEST
PLACE 2,4,WEST
PLACE 3,4,WEST
PLACE 4,4,WEST
#REPORT

PLACE 0,0,NORTH
PLACE 1,0,NORTH
PLACE 2,0,NORTH
PLACE 3,0,NORTH
PLACE 4,0,NORTH
PLACE 0,1,NORTH
PLACE 1,1,NORTH
PLACE 2,1,NORTH
PLACE 3,1,NORTH
PLACE 4,1,NORTH
PLACE 0,2,NORTH
PLACE 1,2,NORTH
PLACE 2,2,NORTH
PLACE 3,2,NORTH
PLACE 4,2,NORTH
PLACE 0,3,NORTH
PLACE 1,3,NORTH
PLACE 2,3,NORTH
PLACE 3,3,NORTH
PLACE 4,3,NORTH
PLACE 0,4,NORTH
PLACE 1,4,NORTH
PLACE 2,4,NORTH
PLACE 3,4,NORTH
PLACE 4,4,NORTH
#REPORT

PLACE 0,0,EAST
PLACE 1,0,EAST
PLACE 2,0,EAST
PLACE 3,0,EAST
PLACE 4,0,EAST
PLACE 0,1,EAST
PLACE 1,1,EAST
PLACE 2,1,EAST
PLACE 3,1,EAST
PLACE 4,1,EAST
PLACE 0,2,EAST
PLACE 1,2,EAST
PLACE 2,2,EAST
PLACE 3,2,EAST
PLACE 4,2,EAST
PLACE 0,3,EAST
PLACE 1,3,EAST
PLACE 2,3,EAST
PLACE 3,3,EAST
PLACE 4,3,EAST
PLACE 0,4,EAST
PLACE 1,4,EAST
PLACE 2,4,EAST
PLACE 3,4,EAST
PLACE 4,4,EAST
#REPORT

PLACE 0,0,SOUTH
PLACE 1,0,SOUTH
PLACE 2,0,SOUTH
PLACE 3,0,SOUTH
PLACE 4,0,SOUTH
PLACE 0,1,SOUTH
PLACE 1,1,SOUTH
PLACE 2,1,SOUTH
PLACE 3,1,SOUTH
PLACE 4,1,SOUTH
PLACE 0,2,SOUTH
PLACE 1,2,SOUTH
PLACE 2,2,SOUTH
PLACE 3,2,SOUTH
PLACE 4,2,SOUTH
PLACE 0,3,SOUTH
PLACE 1,3,SOUTH
PLACE 2,3,SOUTH
PLACE 3,3,SOUTH
PLACE 4,3,SOUTH
PLACE 0,4,SOUTH
PLACE 1,4,SOUTH
PLACE 2,4,SOUTH
PLACE 3,4,SOUTH
PLACE 4,4,SOUTH
REPORT
VALIDATE 4,4,SOUTH
echo"  , false},

                 {
@"# Lots of varying placement, with -ve xy position values
echo Test 5 - PLACE command should be skipped because -ve values can't be parsed
echo Expected output: 4,4,SOUTH
PLACE -5,0,WEST
PLACE -4,-1,WEST
PLACE -3,-2,WEST
PLACE -2,-3,WEST
PLACE -1,-4,WEST
REPORT
VALIDATE 4,4,SOUTH
echo"  , false},

                 {
@"# Lots of varying placement, with out of bounds x/y position values
echo Test 6 - X/Y values of place commands to big for 5x5 board
echo Expected output: 4,4,SOUTH
PLACE 4,4,SOUTH
PLACE 5,10,EAST
PLACE 6,11,EAST
PLACE 7,12,EAST
PLACE 8,13,EAST
PLACE 9,14,EAST
REPORT
VALIDATE 4,4,SOUTH
echo"  , false},

                 {
@"# test for over flow
echo Test 7 - X/Y values cause overflow
echo Expected output: 4,4,SOUTH
PLACE 4,4,WEST
PLACE 18446744073709551616,18446744073709551616,EAST
PLACE 18446744073709551616,18446744073709551616,EAST
PLACE 18446744073709551616,18446744073709551616,EAST
PLACE 18446744073709551616,18446744073709551616,EAST
PLACE 18446744073709551616,18446744073709551616,EAST
REPORT
VALIDATE 4,4,WEST
echo"  , false},
        };
    }
}
