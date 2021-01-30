using System.Linq;
using LDF.Systems.Pathfinding;
using NUnit.Framework;

namespace Tests
{
    using Grid = PathfindingGrid;

    public class AStarTests
    {
        // | | 0 1 2 3 4 |X|   
        // |0| . . . . .
        // |1| . . . X .
        // |2| X X . X .
        // |3| S . . X F
        // |Y|
        private Grid _possibleGrid;

        // |   0 1 2 3 4 |X|
        // |0| . . . X .
        // |1| . . . X .
        // |2| X X . X .
        // |3| S . . X F
        // |Y|
        private Grid _impossibleGrid;

        private static Node StartingNode(Grid grid) => grid[0, 3];
        private static Node FinishNode(Grid grid) => grid[4, 3];

        private readonly (int x, int y)[] _possibleGridPath =
        {
            (1, 3), (2, 3), (2, 2), (2, 1), (2, 0), (3, 0), (4, 0), (4, 1), (4, 2), (4, 3)
        };

        private readonly (int x, int y)[] _possibleWalls =
        {
            (0, 2), (1, 2), (3, 1), (3, 2), (3, 3)
        };

        private readonly (int x, int y)[] _impossibleWalls =
        {
            (0, 2), (1, 2), (3, 0), (3, 1), (3, 2), (3, 3)
        };


        [SetUp]
        public void CreatePossibleGrid()
        {
            _possibleGrid = new Grid(5, 4, IsCellWalkable);

            bool IsCellWalkable(int x, int y)
            {
                return !_possibleWalls.Contains((x, y));
            }

            _impossibleGrid = new Grid(5, 4, IsCellWalkable2);

            bool IsCellWalkable2(int x, int y)
            {
                return !_impossibleWalls.Contains((x, y));
            }
        }

        [Test]
        public void Received_Correct_Path()
        {
            var path = _possibleGrid.FindPathAStar(
                StartingNode(_possibleGrid),
                FinishNode(_possibleGrid));

            (int x, int y)[] coords = path.Select(n => (n.X, n.Y)).ToArray();

            Assert.NotNull(path);
            Assert.AreEqual(_possibleGridPath, coords);
        }

        [Test]
        public void Trying_To_Get_Impossible_Path()
        {
            var path = _impossibleGrid.FindPathAStar(
                StartingNode(_impossibleGrid),
                FinishNode(_impossibleGrid));

            Assert.IsNull(path);
        }

        [Test]
        public void Trying_To_Get_To_Wall()
        {
            var wall = _possibleWalls[4];
            var path = _possibleGrid.FindPathAStar(
                StartingNode(_impossibleGrid),
                _possibleGrid[wall.x, wall.y]);

            var expectedPath = new[] {(1, 3), (2, 3), (3, 3)};
            var pathPoints = path.Select(n => ((int, int)) n);

            Assert.AreEqual(expectedPath, pathPoints);
        }
    }
}