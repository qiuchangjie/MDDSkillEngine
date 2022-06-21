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
                col = GetComponent<Collider>();
            }

            //Debug.LogError(col.bounds.size);

            if (col is BoxCollider)
            {
                BoxCollider col2 = (BoxCollider)col;
                Gizmos.DrawWireCube(col2.bounds.center, col2.bounds.size);
            }

            if (col is SphereCollider)
            {
                SphereCollider col3 = (SphereCollider)col;
                Gizmos.DrawWireSphere(col.bounds.center,col3.radius);
            }

            if (col is CapsuleCollider)
            {
                CapsuleCollider col4 = (CapsuleCollider)col;
            }

           
            //Gizmos.DrawLine(transform.position, transform.position + Vector3.one * 10);
        }
    }
}



#endif
