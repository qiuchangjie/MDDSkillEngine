#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    public class EditorOutLine : MonoBehaviour
    {
        Collider col;

        private void OnDrawGizmos()
        {
            if (col == null)
            {
                col = GetComponent<BoxCollider>();
            }

            //Debug.LogError(col.bounds.size);

            Gizmos.DrawWireCube(col.bounds.center, col.bounds.size);

           
            //Gizmos.DrawLine(transform.position, transform.position + Vector3.one * 10);
        }
    }
}



#endif
