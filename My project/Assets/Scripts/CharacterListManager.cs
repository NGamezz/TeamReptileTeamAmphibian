using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class FacePlate
{
}

/// <summary>
/// Still gotta make that.
/// </summary>
public class CharacterListManager : MonoBehaviour
{
    [SerializeField] private List<Character> characters = new List<Character>();
    [SerializeField] private Button button;

    [SerializeField] private GameObject selectionHolder;

    private Character currentCharacter;

    public void SelectCharacter(Character character)
    {
        currentCharacter = character;
        selectionHolder.SetActive(true);
    }

    public void DisplayCharacters()
    {
        foreach (Character character in characters)
        {
            character.SelectionObject.gameObject.SetActive(true);
        }
    }

    public bool CheckForCharacter(Character character, string name = "", string profession = "", FacePlate facePlate = null)
    {
        if (character.Profession == profession || character.Name == name || character.FacePlate == facePlate)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Start()
    {
        selectionHolder.SetActive(false);
    }
}
