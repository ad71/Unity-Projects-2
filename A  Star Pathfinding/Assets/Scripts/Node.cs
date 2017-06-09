using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

    public bool walkable;
    public Vector3 worldposition;

    public Node(bool _walkable, Vector3 _worldPosition)
    {
        this.walkable = _walkable;
        this.worldposition = _worldPosition;
    }
}
