using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapVisualizer : MonoBehaviour
    {
        public GameObject obstaclePrefab;
        public GameObject pathPrefab;
        public GameObject startPrefab;
        public GameObject endPrefab;

        private GameObject startPointInstance;
        private GameObject endPointInstance;
        private List<GameObject> pathInstances = new List<GameObject>();

        public void VisualizeMap(MapGrid grid, MapData data)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            for (int row = 0; row < grid.Length; row++)
            {
                for (int col = 0; col < grid.Width; col++)
                {
                    Cell cell = grid.GetCell(col, row);
                    Vector3 position = new Vector3(col, 0, row);
                    Vector3 adjustedPosition = position + new Vector3(.5f, .5f, .5f);

                    if (cell.IsTaken)
                    {
                        Instantiate(obstaclePrefab, adjustedPosition, Quaternion.identity, transform);
                    }
                }
            }
        }

        public void PlaceStartPoint(Vector3 Position)
        {
            if (startPointInstance != null)
            {
                Destroy(startPointInstance);
            }
            Vector3 adjustedPosition = Position + new Vector3(.5f, 0, .5f);
            startPointInstance = Instantiate(startPrefab, adjustedPosition, Quaternion.identity, transform);
        }

        public void PlaceEndPoint(Vector3 Position)
        {
            if (endPointInstance != null)
            {
                Destroy(endPointInstance);
            }
            Vector3 adjustedPosition = Position + new Vector3(.5f, 0, .5f);
            endPointInstance = Instantiate(endPrefab, adjustedPosition, Quaternion.identity, transform);
        }

        public void ClearPath()
        {
            foreach (var pathInstances in pathInstances)
            {
                Destroy(pathInstances);
            }
            pathInstances.Clear();
        }

        public void VisualizePath(List<Vector3> path)
        {
            ClearPath();
            foreach (Transform child in transform)
            {
                if (child.gameObject.name == "Path")
                {
                    Destroy(child.gameObject);
                }
            }

            foreach (Vector3 pos in path)
            {
                Vector3 Position = new Vector3(pos.x, 0.01f, pos.z);
                Vector3 adjustedPosition = Position + new Vector3(.5f, 0, .5f);
                Quaternion rotation = Quaternion.Euler(90, 0, 0);
                GameObject pathPrefabInstance = Instantiate(pathPrefab, adjustedPosition, rotation, transform);
                pathInstances.Add(pathPrefabInstance);
            }
        }
    }
}
