using System.Collections.Generic;
using UnityEngine;

public class ObjectFrameHandler : MonoBehaviour
{
    [SerializeField] private List<FrameData> frames = new();

    public List<Transform> currentTransforms = new();

    private void Awake()
    {
        FrameManager frameManager = FindObjectOfType<FrameManager>();
        frameManager.OnGoToFrame += GoToFrameIndex;
    }

    private void Start()
    {
        GoToFrameIndex(0);
    }

    public void GoToFrameIndex(int index)
    {
        if (frames.Count <= index) { return; }

        ApplyPositions(index);
    }

    private void ApplyPositions(int index)
    {
        for (int i = 0; i < currentTransforms.Count; i++)
        {
            if (frames[index].transforms[i] == null) { continue; }

            if (frames[index].isChild[i])
            {
                currentTransforms[i].SetLocalPositionAndRotation(frames[index].transforms[i].position, frames[index].transforms[i].rotation);
            }
            else
            {
                currentTransforms[i].SetPositionAndRotation(frames[index].transforms[i].position, frames[index].transforms[i].rotation);
            }
        }
    }
}