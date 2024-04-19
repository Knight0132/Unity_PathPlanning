using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;
using PathPlanning;

namespace Map
{
    public class MapGrid
    {
        private int width, length;
        public Cell[,] cellGrid;

        public int Width { get => width; }

        public int Length { get => length; }

        public MapGrid(int width, int length)
        {
            this.width = width;
            this.length = length;
            CreateGrid();
        }

        public void CreateGrid()
        {
            cellGrid = new Cell[length, width];
            for (int row = 0; row < length; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    cellGrid[row, col] = new Cell(col, row);
                }
            }

            GetNeighborsForCells();
        }
        
        private void GetNeighborsForCells()
        {
            for (int row = 0; row < length; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    Cell currentCell = cellGrid[row, col];

                    if (row > 0 && !cellGrid[row - 1, col].IsTaken)
                    {
                        currentCell.AddNeighbor(cellGrid[row - 1, col]);
                    }
                    
                    if (row < length - 1 && !cellGrid[row + 1, col].IsTaken)
                    {
                        currentCell.AddNeighbor(cellGrid[row + 1, col]);
                    }

                    if (col > 0 && !cellGrid[row, col - 1].IsTaken)
                    {
                        currentCell.AddNeighbor(cellGrid[row, col - 1]);
                    }

                    if (col < width - 1 && !cellGrid[row, col + 1].IsTaken)
                    {
                        currentCell.AddNeighbor(cellGrid[row, col + 1]);
                    }
                }
            }
        }

        public List<Cell> GetAllCells()
        {
            List<Cell> cells = new List<Cell>();
            
            for (int row = 0; row < length; row++)
            {
                for (int col = 0; col < length; col++)
                {
                    cells.Add(cellGrid[row, col]);
                }
            }
            return cells;
        }
        

        public void SetCell(int x, int z, Cell.CellObjectType objectType, bool isTaken = false)
        {
            cellGrid[z, x].ObjectType = objectType;
            cellGrid[z, x].IsTaken = isTaken;
        }

        public void SetCell(float x, float z, Cell.CellObjectType objectType, bool isTaken = false)
        {
            SetCell((int)x, (int)z, objectType, isTaken);
        }

        public void SetStartCell(int x, int z)
        {
            SetCell(x, z, Cell.CellObjectType.Start, false);
        }

        public void SetStartCell(float x, float z)
        {
            SetCell((int)x, (int)z, Cell.CellObjectType.Start, false);
        }

        public void SetEndCell(int x, int z)
        {
            SetCell(x, z, Cell.CellObjectType.End, false);
        }

        public void SetEndCell(float x, float z)
        {
            SetCell((int)x, (int)z, Cell.CellObjectType.End, false);
        }

        public bool IsCellTaken(int x, int z)
        {
            return cellGrid[z, x].IsTaken;
        }

        public bool IsCellTaken(float x, float z)
        {
            return cellGrid[(int)z, (int)x].IsTaken;
        }

        public int CalculateIndexFromCoordinates(int x, int z)
        {
            return z * width + x;
        }

        public int CalculateIndexFromCoordinates(float x, float z)
        {
            return CalculateIndexFromCoordinates((int)x, (int)z);
        }

        public Vector3 CalculateCoordinatesFromIndex(int index)
        {
            int x = index % width;
            int z = index / width;
            return new Vector3(x, 0, z);
        }
        
        
        public bool IsCellValid(float x, float z)
        {
            if (x < 0 || x >= width || z < 0 || z >= length)
            {
                return false;
            }

            return true;
        }

        public Cell GetCell(int x, int z)
        {
            if (!IsCellValid(x, z))
            {
                return null;
            }

            return cellGrid[z, x];
        }

        public Cell GetCell(float x, float z)
        {
            return GetCell((int)x, (int)z);
        }

        public void CheckCoordinates()
        {
            for (int i = 0; i < length; i++)
            {
                StringBuilder b = new StringBuilder();
                for (int j = 0; j < width; j++)
                {
                    b.Append(j + "," + i + " ");
                }

                Debug.Log(b.ToString());
            }
        }
    }
}


