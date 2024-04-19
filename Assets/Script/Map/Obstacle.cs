using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Obstacle
    { 
        private Vector3 position;

        public Vector3 Position { get => position; set => position = value; }
        
        public Obstacle(Vector3 position)
        {
            this.Position = position;
        }
    }
}

