using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FrameData
{
    public List<Transform> transforms = new();
    public List<bool> isChild = new();
}