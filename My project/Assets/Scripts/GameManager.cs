using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void OnDisable()
    {
        EventManager.ClearEvents(true);
    }
}