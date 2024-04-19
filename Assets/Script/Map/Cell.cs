using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Cell
    {
        private int x, z;
        private bool isTaken;
        private CellObjectType objectType;
        private Vector3 position;
        public List<Cell> Neighbors;
        public int X { get => x; }
        public int Z { get => z; }
        public bool IsTaken { get => isTaken; set => isTaken = value; }
        public CellObjectType ObjectType { get => objectType; set => objectType = value; }
        public Vector3 Position { get => position; set => position = value; }

        public Cell(int x, int z)
        {
            this.x = x;
            this.z = z;
            this.objectType = CellObjectType.Empty;
            Position = new Vector3(x, 0, z);
            Neighbors = new List<Cell>();
            isTaken = false;
        }
        
        public void AddNeighbor(Cell neighbor)
        {
            Neighbors.Add(neighbor);
        }
        public enum CellObjectType
        {
            Empty,
            Obstacle,
            Start,
            End
        }
    }
}

