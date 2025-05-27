using UnityEngine;

namespace Octrees {
    public class OctreeGenerator : MonoBehaviour {
        public GameObject[] objects;
        [Range(1,10000)]
        public float minNodeSize = 1f;
        public Octree ot;
        
        public readonly Graph waypoints = new();
        
        void Awake() => ot = new Octree(objects, minNodeSize, waypoints);

        void OnDrawGizmos() {
            if (!Application.isPlaying) return;
            
            Gizmos.color = Color.green;
            
            ot.root.DrawNode();
        }
    }
}
