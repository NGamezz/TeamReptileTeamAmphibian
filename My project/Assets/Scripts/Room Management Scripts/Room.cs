using System.Collections.Generic;
using UnityEngine;

public interface IRoom
{
    public void OnRoomSelection();
    public void OnRoomDeselection();
}

public class Room : MonoBehaviour, IRoom
{
    [Tooltip("Which dialogue to show if room is selected, it is dependant on the scene index. If you wish to skip a frame, just make an empty one.")]
    [SerializeField] private List<Dialogue> dialoguePerFrame;
    private Dialogue currentDialogue;

    private bool roomSelected = false;

    private FrameManager frameManager;

    private void Awake()
    {
        frameManager = FindObjectOfType<FrameManager>();
    }

    private void Start()
    {
        frameManager.OnGoToFrame += SelectDialogueIndex;
        SelectDialogueIndex(0);
    }

    private void OnDisable()
    {
        frameManager.OnGoToFrame += SelectDialogueIndex;
    }

    private void SelectDialogueIndex(int index)
    {
        if (dialoguePerFrame.Count <= index || dialoguePerFrame[index] == null)
        {
            currentDialogue = new()
            {
                Text = string.Empty
            };
        }
        else
        {
            currentDialogue = dialoguePerFrame[index];
        }

        if (roomSelected)
        {
            DialogueHandler.Instance.PlayDialogue(currentDialogue);
        }
    }

    public void OnRoomSelection()
    {
        if (currentDialogue == null) { return; }
        roomSelected = true;
        DialogueHandler.Instance.PlayDialogue(currentDialogue);
    }

    public void OnRoomDeselection()
    {
        DialogueHandler.Instance.PlayDialogue(new Dialogue() { Text = string.Empty });
        roomSelected = false;
    }
}