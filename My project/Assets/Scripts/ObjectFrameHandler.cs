using System.Collections.Generic;
using UnityEngine;

public class ObjectFrameHandler : MonoBehaviour
{
    [SerializeField] private List<FrameData> frames = new();

    public List<Transform> currentTransforms = new();
    //private List<Vector3> currentPositions = new();

    [SerializeField] private int amountOfAvailableFrames = 3;
    private int frameIndex = 0;

    private void Start()
    {
        GoToFrameIndex(frameIndex);
    }

    public void GoToFrameIndex(int index)
    {
        if (frames.Count <= index) { return; }

        if (frames[index].positions == null) { return; }

        //currentPositions = frames[index].positions;

        ApplyPositions();
    }

    private void ApplyPositions()
    {
        for (int i = 0; i < currentTransforms.Count; i++)
        {
            if (frames[frameIndex].transforms[i].position == null) { continue; }

            currentTransforms[i].position = frames[frameIndex].transforms[i].position;
        }
    }

    public void NextFrame()
    {
        if (frameIndex >= frames.Count - 1 || frameIndex >= amountOfAvailableFrames) { return; }

        frameIndex++;
        GoToFrameIndex(frameIndex);
    }

    public void PreviousFrame()
    {
        if (frameIndex <= 0) { return; }

        frameIndex--;
        GoToFrameIndex(frameIndex);
    }

    //private void SavePositions()
    //{
    //    currentPositions.Clear();
    //    for (int i = 0; i < currentTransforms.Count; i++)
    //    {
    //        if (currentPositions.Contains(currentTransforms[i].position)) { continue; }

    //        currentPositions.Add(currentTransforms[i].position);
    //    }
    //}
}