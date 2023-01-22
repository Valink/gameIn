using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TypedWordDetector : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;

    [SerializeField] public List<string> UnitBehaviourNames;

    [SerializeField] public UnityEvent<string> typedWordMatchAUnitBehaviourNameEvent;

    [SerializeField] private string[] actions;
    [SerializeField] public UnityEvent<string> typedWordMatchAnActionEvent;

    void Awake()
    {
        input.onValueChanged.AddListener(OnValueChanged);
        input.onDeselect.AddListener(Select);
    }

    void Start()
    {
        Select(String.Empty);
    }

    private void Select(string _)
    {
        input.ActivateInputField();
    }

    private void OnValueChanged(string typedWord)
    {
        TypedWordMatchAnActionEvent(typedWord);
        TypedWordMatchAUnitBehaviourNameEvent(typedWord);
    }

    private void TypedWordMatchAnActionEvent(string typedWord)
    {
        if (IsInListCasseInsensitive(typedWord, actions))
        {
            TypedWordMatchATargetWord();
            typedWordMatchAnActionEvent.Invoke(typedWord);
        }
    }

    private void TypedWordMatchAUnitBehaviourNameEvent(string typedWord)
    {
        if (IsInListCasseInsensitive(typedWord, UnitBehaviourNames.ToArray()))
        {
            TypedWordMatchATargetWord();
            typedWordMatchAUnitBehaviourNameEvent.Invoke(typedWord);
        }
    }

    private void TypedWordMatchATargetWord()
    {
        ResetTextInput();
    }

    private void ResetTextInput()
    {
        input.text = "";
    }

    private bool IsInListCasseInsensitive(string typedWord, string[] strings)
    {
        foreach (var s in strings)
        {
            if (typedWord.ToUpper() == s.ToUpper())
            {
                return true;
            }
        }
        return false;
    }
}
