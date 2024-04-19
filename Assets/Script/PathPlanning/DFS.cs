using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;

namespace PathPlanning
{
    public class DFS
    { 
        public Cell startPosition;
        public Cell endPosition;
        public List<Vector3> path = new List<Vector3>();

        public static List<Vector3> DepthFirstSearch(Cell startPosition, Cell endPosition)
        {  
            Stack<Cell> stack = new Stack<Cell>();
            HashSet<Cell> visited = new HashSet<Cell>();
            Dictionary<Cell, Cell> parent = new Dictionary<Cell, Cell>();
         
            stack.Push(startPosition);
            visited.Add(startPosition);

            while (stack.Count > 0)
            {
                Cell current = stack.Pop();
                if (current == endPosition)
                {
                    return GetPath(parent, endPosition, startPosition);
                }
                foreach (Cell neighbor in current.Neighbors)
                {
                    if (!visited.Contains(neighbor) && !neighbor.IsTaken)
                    {
                        visited.Add(neighbor);
                        stack.Push(neighbor);
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

