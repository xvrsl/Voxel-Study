using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace XVRSL
{
    public class VoxelTest : MonoBehaviour
    {
        public Voxels.Voxel<Color> voxel;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Handles.matrix = transform.localToWorldMatrix;
            for (int i = 0; i < voxel.Count; i++)
            {
                var color = voxel[i];
                var coord = voxel.IndexToCoord(i);
                color.a = 0.25f;
                Gizmos.color = color;

                Handles.Label(coord, $"{i} ({coord.x},{coord.y},{coord.z})");
                Gizmos.DrawCube(coord, Vector3.one);

            }
        }
#endif
    }
}
