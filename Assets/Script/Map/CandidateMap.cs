using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Map
{
    public class CandidateMap
    {
        private MapGrid grid;
        private bool[] obstacleArray;
        private int numberOfObstacles = 0;
        private List<Obstacle> obstacleList;
        private List<Vector3> path;

        public MapGrid Grid { get => grid; }

        public bool[] ObstacleArray { get => obstacleArray; }

        public CandidateMap(MapGrid grid, int numberOfObstacles)
        {
            this.grid = grid;
            this.numberOfObstacles = numberOfObstacles;
        }

        public void CreateMap()
        {
            obstacleArray = new bool[grid.Width * grid.Length];
            obstacleList = new List<Obstacle>();
            PlaceObstacles(this.numberOfObstacles);
        }
    

        private bool CheckIfPositionCanBeObstacle(Vector3 position)
        {
            int index = grid.CalculateIndexFromCoordinates(position.x, position.z);
            return obstacleArray[index] == false;
        }

        private void PlaceObstacles(int numberOfObstacles)
        {
            var count = numberOfObstacles;
            var maxiter = 10 * numberOfObstacles;
            while (count > 0 && maxiter > 0)
            {
                var randomIndex = Random.Range(0, obstacleArray.Length);
                if (obstacleArray[randomIndex] == false)
                {
                    var coordinates = grid.CalculateCoordinatesFromIndex(randomIndex);
                    
                    grid.SetCell(coordinates.x, coordinates.z, Cell.CellObjectType.Obstacle, true);
                    obstacleArray[randomIndex] = true;
                    obstacleList.Add(new Obstacle(coordinates));
                    count--;
                }
                maxiter--;
            }
        }

        public MapData ReturnMapData()
        {
            return new MapData
            {
                obstacleArray = this.obstacleArray,
                obstacleList = obstacleList,
                path = this.path
            };
        }
    }
}

