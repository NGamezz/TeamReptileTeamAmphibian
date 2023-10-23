using System;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] private string characterName;
    public string Name { get { return characterName; } }

    [SerializeField] private string profession;
    public string Profession { get { return profession; } }

    [SerializeField] private FacePlate facePlate;
    public FacePlate FacePlate { get { return facePlate; } }

    [SerializeField] private Button selectionButton;

    public GameObject SelectionObject;

    private CharacterListManager listManager;

    public Character(CharacterListManager listManager)
    {
        this.listManager = listManager;
    }

    public void Initialize()
    {
        selectionButton.onClick.RemoveAllListeners();
        selectionButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        listManager.SelectCharacter(this);
    }
}
