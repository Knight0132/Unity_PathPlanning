using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathPlanning;

namespace Map
{
    public class MapGenerator : MonoBehaviour
    {
        public Camera cam;
        public GridVisualizer gridVisualizer;
        public MapVisualizer mapVisualizer;
    
        public int numberOfObstacles;

        private CandidateMap map;
    
        [Range(0, 100)]
        public int width, length = 20;
        private MapGrid grid;
        private Vector3 startPosition, endPosition;
        private Cell startCell, endCell;
        private bool isStartSet = false;
        private bool isEndSet = false;
        
        public SearchAlgorithm selectedAlgorithm = SearchAlgorithm.BFS;
        private List<Vector3> path = new List<Vector3>();
        
        
        private void Start()
        {
            grid = new MapGrid(width, length);
            map = new CandidateMap(grid, numberOfObstacles);
            map.CreateMap();
            gridVisualizer.VisualizeGrid(width, length);
            mapVisualizer.VisualizeMap(grid, map.ReturnMapData());
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 worldPoint = hit.point;
                    int x = Mathf.RoundToInt(worldPoint.x);
                    int z = Mathf.RoundToInt(worldPoint.z);

                    if (!isStartSet)
                    {
                        startPosition = new Vector3(x, 0, z);
                        grid.SetStartCell(x, z);
                        isStartSet = true;
                        Debug.Log("Start Position Set: " + startPosition);
                        mapVisualizer.PlaceStartPoint(startPosition);
                    }
                    else
                    {
                        endPosition = new Vector3(x, 0, z);
                        grid.SetEndCell(x, z);
                        isEndSet = true;
                        Debug.Log("End Position Set: " + endPosition);
                        mapVisualizer.PlaceEndPoint(endPosition);
                        
                        CalculateAndVisualizePath();
                    }
                }
            }
        }
        
        private void CalculateAndVisualizePath()
        {
            startCell = GetCellFromPosition(startPosition);
            endCell = GetCellFromPosition(endPosition);
            switch (selectedAlgorithm)
            {
                case SearchAlgorithm.BFS:
                    path = BFS.BreadthFirstSearch(startCell, endCell);
                    break;
                case SearchAlgorithm.DFS:
                    path = DFS.DepthFirstSearch(startCell, endCell);
                    break;
                case SearchAlgorithm.Dijkstra:
                    path = Dijkstra.DijkstraAlgorithm(grid, startCell, endCell);
                    break;
            }
            mapVisualizer.VisualizePath(path);
        }
        
        private Cell GetCellFromPosition(Vector3 position)
        {
            int x = Mathf.RoundToInt(position.x);
            int z = Mathf.RoundToInt(position.z);
            return grid.cellGrid[z, x];
        }
        
    }
}
