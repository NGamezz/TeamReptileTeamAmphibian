using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FrameData : IFrameData
{
    public List<Vector3> positions = new();

    public List<Transform> transforms = new();
}