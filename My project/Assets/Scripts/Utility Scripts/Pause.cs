using UnityEngine;

public class Pause : MonoBehaviour
{
    public void PauseGame()
    {
        EventManager.InvokeEvent(EventType.Pause);
    }

    public void UnPause()
    {
        EventManager.InvokeEvent(EventType.UnPause);
    }
}