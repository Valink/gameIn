using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TypingController : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;

    [SerializeField] public UnityEvent<string> typedWordMatchATargetWordEvent;

    [SerializeField] private string[] targetWords;

    void Awake()
    {
        input.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(string typedWord)
    {
        if (TypedWordMatchATargetWord(typedWord))
        {
            input.text = "";
            typedWordMatchATargetWordEvent.Invoke(typedWord);
        }
    }

    private bool TypedWordMatchATargetWord(string typedWord)
    {
        foreach (var targetWord in targetWords)
        {
            if (typedWord.ToLower() == targetWord.ToLower())
            {
                return true;
            }
        }

        return false;
    }

    void Start()
    {
        input.Select();
    }

    void Update()
    {

    }
}
