using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {

    public Color color;
    private List<Transform> nodes;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Transform[] pathTransforms = GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();
        for(int i = 0; i < pathTransforms.Length; ++i)
        {
            if(pathTransforms[i] != this.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }

        for(int i = 0; i < nodes.Count; ++i)
        {
            Vector3 current = nodes[i].position;
            Vector3 previous = Vector3.zero;
            if (i > 0) previous = nodes[i - 1].position;
            else if (i == 0 &&  nodes.Count > 1) previous = nodes[nodes.Count - 1].position;
            Gizmos.DrawLine(previous, current);
            Gizmos.DrawWireSphere(current, 1f);
        }
    }
}
