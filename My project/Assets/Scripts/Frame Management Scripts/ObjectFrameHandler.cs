using System.Collections.Generic;
using UnityEngine;

public class ObjectFrameHandler : MonoBehaviour
{
    [SerializeField] private List<FrameData> frames = new();

    [SerializeField] private List<Transform> currentTransforms = new();

    private FrameManager frameManager;

    private void OnDisable()
    {
        if (frameManager == null) { return; }
        frameManager.OnGoToFrame -= GoToFrameIndex;
    }

    private void Start()
    {
        currentTransforms.Clear();
        frameManager = FindObjectOfType<FrameManager>();
        frameManager.OnGoToFrame += GoToFrameIndex;

        AddTransformsToList();

        if (frameManager.AmountOfFrames < frames.Count)
        {
            frameManager.SetAmountOfFrames(frames.Count);
        }
    }

    private void AddTransformsToList()
    {
        if (!currentTransforms.Contains(transform))
        {
            currentTransforms.Add(transform);
        }

        Transform[] childrenTransforms = transform.GetComponentsInChildren<Transform>(true);
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

        Debug.Log(string.Join(",", currentTransforms));

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

            frameData.setActive.Add(currentTransforms[i].gameObject.activeInHierarchy);
        }

        frames.Add(frameData);
    }

    private void ApplyPositions(int index)
    {
        if (frames.Count <= index) { return; }

        for (int i = 0; i < currentTransforms.Count; i++)
        {
            if (frames[index].positions.Count <= i) { continue; }

            if (frames[index].isChild[i])
            {
                currentTransforms[i].SetLocalPositionAndRotation(frames[index].positions[i], frames[index].rotations[i]);
            }
            else
            {
                currentTransforms[i].SetPositionAndRotation(frames[index].positions[i], frames[index].rotations[i]);
            }

            currentTransforms[i].gameObject.SetActive(frames[index].setActive[i]);
        }
    }
}