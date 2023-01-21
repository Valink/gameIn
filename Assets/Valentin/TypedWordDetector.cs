using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TypedWordDetector : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;

    [SerializeField] public List<string> friendNames;

    [SerializeField] public UnityEvent<string> typedWordMatchAFriendNameEvent;

    [SerializeField] private string[] actions;
    [SerializeField] public UnityEvent<string> typedWordMatchAnActionEvent;

    void Awake()
    {
        input.onValueChanged.AddListener(OnValueChanged);
    }

    void Start()
    {
        input.Select();
    }

    private void OnValueChanged(string typedWord)
    {
        TypedWordMatchAnActionEvent(typedWord);
        TypedWordMatchAFriendNameEvent(typedWord);
    }

    private void TypedWordMatchAnActionEvent(string typedWord)
    {
        if (IsInListCasseInsensitive(typedWord, actions))
        {
            TypedWordMatchATargetWord();
            typedWordMatchAFriendNameEvent.Invoke(typedWord);
        }
    }

    private void TypedWordMatchAFriendNameEvent(string typedWord)
    {
        if (IsInListCasseInsensitive(typedWord, friendNames.ToArray()))
        {
            TypedWordMatchATargetWord();
            typedWordMatchAFriendNameEvent.Invoke(typedWord);
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
            if (typedWord.ToLower() == s.ToLower())
            {
                return true;
            }
        }
        return false;
    }
}
