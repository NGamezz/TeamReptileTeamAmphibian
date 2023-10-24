using System;
using Unity.VisualScripting;
using UnityEngine;

public class FrameManager : MonoBehaviour
{
    private int frameIndex = 0;

    public int AmountOfFrames { get; private set; } = 0;

    private bool gamePaused = false;

    public event Action<int> OnGoToFrame;

    [SerializeField] private int amountOfAvailableFrames = 3;

    public void SetAmountOfFrames(int value)
    {
        if (value > 0)
        {
            AmountOfFrames = value;
        }
    }

    public void NextFrame()
    {
        if (frameIndex >= AmountOfFrames - 1 || frameIndex >= amountOfAvailableFrames - 1 || gamePaused) { return; }

        frameIndex++;
        OnGoToFrame?.Invoke(frameIndex);
    }

    public void PreviousFrame()
    {
        if (frameIndex <= 0 || gamePaused) { return; }

        frameIndex--;
        OnGoToFrame?.Invoke(frameIndex);
    }

    private void OnEnable()
    {
        EventManager.AddListener(EventType.Pause, () => gamePaused = true);
        EventManager.AddListener(EventType.UnPause, () => gamePaused = false);
    }

    private void OnDisable()
    {
        OnGoToFrame = null;
    }

    private void Start()
    {
        OnGoToFrame?.Invoke(0);
    }
}