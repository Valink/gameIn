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
            Debug.Log($"Action: {typedWord.ToUpper()}");
            typedWordMatchAnActionEvent.Invoke(typedWord.ToUpper());
        }
    }

    private void TypedWordMatchAFriendNameEvent(string typedWord)
    {
        if (IsInListCasseInsensitive(typedWord, friendNames.ToArray()))
        {
            TypedWordMatchATargetWord();
            Debug.Log($"Satellite: {typedWord.ToUpper()}");
            typedWordMatchAFriendNameEvent.Invoke(typedWord.ToUpper());
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
