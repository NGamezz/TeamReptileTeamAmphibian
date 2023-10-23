using System;
using UnityEngine;

public class FrameManager : MonoBehaviour
{
    private int frameIndex = 0;

    public int amountOfFrames = 0;

    public event Action<int> OnGoToFrame;

    [SerializeField] private int amountOfAvailableFrames = 3;

    public void NextFrame()
    {
        if (frameIndex >= amountOfFrames - 1 || frameIndex >= amountOfAvailableFrames - 1) { return; }

        frameIndex++;
        OnGoToFrame?.Invoke(frameIndex);
    }

    public void PreviousFrame()
    {
        if (frameIndex <= 0) { return; }

        frameIndex--;
        OnGoToFrame?.Invoke(frameIndex);
    }

    private void Start()
    {
        OnGoToFrame?.Invoke(frameIndex);
    }

    private void OnDisable()
    {
        OnGoToFrame = null;
    }
}
