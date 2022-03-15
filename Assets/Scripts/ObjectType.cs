using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectType : MonoBehaviour
{
    public ObjectShape objectShape;
}

public enum ObjectShape
{
    sphere = 0,
    cube,
    capsule,
}
