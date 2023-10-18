using System;
using UnityEngine;

public class FrameManager : MonoBehaviour
{
    private int frameIndex = 0;

    [SerializeField] private int amountOfFrames = 3;

    public event Action<int> OnGoToFrame;

    [SerializeField] private int amountOfAvailableFrames = 3;

    public void NextFrame()
    {
        if (frameIndex >= amountOfFrames || frameIndex >= amountOfAvailableFrames) { return; }

        frameIndex++;
        OnGoToFrame?.Invoke(frameIndex);
    }

    public void PreviousFrame()
    {
        if (frameIndex <= 0) { return; }

        frameIndex--;
        OnGoToFrame?.Invoke(frameIndex);
    }

    private void OnDisable()
    {
        OnGoToFrame = null;
    }
}
