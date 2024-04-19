using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public struct MapData{
        public bool[] obstacleArray;
        public List<Obstacle> obstacleList;
        public List<Vector3> path;
        public Vector3 startPosition;
        public Vector3 endPosition;
    }
}

