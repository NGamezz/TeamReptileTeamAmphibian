using System.Collections.Generic;
using UnityEngine;

public class ObjectFrameHandler : MonoBehaviour
{
    [SerializeField] private List<FrameData> frames = new();

    private readonly List<Transform> currentTransforms = new();

    private FrameManager FrameManager => FindObjectOfType<FrameManager>();

    private void OnEnable()
    {
        FrameManager.OnGoToFrame += GoToFrameIndex;
    }

    private void OnDisable()
    {
        if (FrameManager == null) { return; }
        FrameManager.OnGoToFrame -= GoToFrameIndex;
    }

    private void Start()
    {
        AddTransformsToList();
        GoToFrameIndex(0);

        if (FrameManager.amountOfFrames < frames.Count)
        {
            FrameManager.amountOfFrames = frames.Count;
        }
    }

    private void AddTransformsToList()
    {
        if (!currentTransforms.Contains(transform))
        {
            currentTransforms.Add(transform);
        }

        Transform[] childrenTransforms = transform.GetComponentsInChildren<Transform>();
        foreach (Transform transform in childrenTransforms)
        {
            if (currentTransforms.Contains(transform)) { continue; }
            currentTransforms.Add(transform);
        }
    }

    public void GoToFrameIndex(int index)
    {
        if (frames.Count <= index) { return; }

        ApplyPositions(index);
    }

    public void SaveState()
    {
        FrameData frameData = new();

        AddTransformsToList();

        foreach (Transform transform in currentTransforms)
        {
            if (transform.IsChildOf(this.transform) && transform != this.transform)
            {
                frameData.isChild.Add(true);
            }
            else
            {
                frameData.isChild.Add(false);
            }
        }

        for (int i = 0; i < currentTransforms.Count; i++)
        {
            if (currentTransforms[i] == null) { continue; }

            if (frameData.isChild[i])
            {
                frameData.positions.Add(currentTransforms[i].localPosition);
                frameData.rotations.Add(currentTransforms[i].localRotation);
            }
            else
            {
                frameData.positions.Add(currentTransforms[i].position);
                frameData.rotations.Add(currentTransforms[i].rotation);
            }
        }

        frames.Add(frameData);

        if (FrameManager.amountOfFrames < frames.Count)
        {
            FrameManager.amountOfFrames++;
        }
    }

    private void ApplyPositions(int index)
    {
        for (int i = 0; i < currentTransforms.Count; i++)
        {
            if (frames[index].positions[i] == null) { continue; }

            if (frames[index].isChild[i])
            {
                currentTransforms[i].SetLocalPositionAndRotation(frames[index].positions[i], frames[index].rotations[i]);
            }
            else
            {
                currentTransforms[i].SetPositionAndRotation(frames[index].positions[i], frames[index].rotations[i]);
            }
        }
    }
}