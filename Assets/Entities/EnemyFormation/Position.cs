using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour {

   

    private void OnDrawGizmos()
    {
        //make a Gizmo which is essentially a prefab visible in editor only but has no visual impact.
        Gizmos.DrawWireSphere(transform.position, 1);

        //now drop these gizmos where you need them and script them.
    }
}
