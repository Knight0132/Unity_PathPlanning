using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;

namespace PathPlanning
{
    public class BFS
    {
        public Cell startPosition;
        public Cell endPosition;
        public List<Vector3> path = new List<Vector3>();

        public static List<Vector3> BreadthFirstSearch(Cell startPosition, Cell endPosition)
        {
            Queue<Cell> queue = new Queue<Cell>();
            HashSet<Cell> visited = new HashSet<Cell>();
            Dictionary<Cell, Cell> parent = new Dictionary<Cell, Cell>();
            
            visited.Add(startPosition);
            queue.Enqueue(startPosition);

            while (queue.Count > 0)
            {
                Cell current = queue.Dequeue();
                if (current == endPosition)
                {
                    return GetPath(parent, endPosition, startPosition);
                }
                foreach (Cell neighbor in current.Neighbors)
                {
                    if (!visited.Contains(neighbor) && !neighbor.IsTaken)
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                        parent[neighbor] = current;
                    }
                }
            }
            
            Debug.Log("No path found");
            return new List<Vector3>();
        }
        
        public static List<Vector3> GetPath(Dictionary<Cell, Cell> parent, Cell endPosition, Cell startPosition)
        {
            List<Vector3> path = new List<Vector3>();
            
            Cell current = endPosition;
            while (current != null && current != startPosition)
            {
                path.Add(current.Position);
                current = parent[current];
            }
            path.Reverse();
            return path;
        }
    }
}

