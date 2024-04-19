using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;

namespace PathPlanning
{
    public class Dijkstra
    {
        public Cell startPosition;
        public Cell endPosition;
        public MapGrid grid;
        public List<Vector3> path = new List<Vector3>();
        public static List<Vector3> DijkstraAlgorithm(MapGrid grid, Cell startPosition, Cell endPosition)
        {
            Dictionary<Cell, int> distance = new Dictionary<Cell, int>();
            Dictionary<Cell, Cell> previous = new Dictionary<Cell, Cell>();
            List<Cell> unvisited = new List<Cell>();

            foreach (Cell cell in grid.GetAllCells())
            {
                distance[cell] = int.MaxValue;
                previous[cell] = null;
                unvisited.Add(cell);
            }

            distance[startPosition] = 0;

            while (unvisited.Count != 0)
            {
                Cell current = unvisited[0];
                foreach (Cell cell in unvisited)
                {
                    if (distance[cell] < distance[current])
                    {
                        current = cell;
                    }
                }
                unvisited.Remove(current);

                if (current == endPosition)
                {
                    return GetPath(previous, startPosition, endPosition);
                }

                foreach (Cell neighbor in current.Neighbors)
                {
                    if (unvisited.Contains(neighbor) && !neighbor.IsTaken)
                    {
                        int alt = distance[current] + 1;

                        if (distance[neighbor] > alt)
                        {
                            distance[neighbor] = alt;
                            previous[neighbor] = current;
                        }
                    }
                }
            }

            Debug.Log("No path found");
            return new List<Vector3>();
        }

        public static List<Vector3> GetPath(Dictionary<Cell, Cell> previous, Cell startPosition, Cell endPosition)
        {
            List<Vector3> path = new List<Vector3>();

            Cell current = endPosition;
            while (current != null && current != startPosition)
            {
                path.Add(current.Position);
                current = previous[current];
            }
            path.Reverse();
            return path;
        }
    }
}