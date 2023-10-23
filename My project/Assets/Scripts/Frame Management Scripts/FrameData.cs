using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FrameData
{
    public List<bool> isChild = new();
    public List<Vector3> positions = new();
    public List<Quaternion> rotations = new();
}