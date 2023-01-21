using UnityEngine;

public class Dummy : MonoBehaviour
{
    public void LogWhenTypedWordMatchATargetWord(string typedWord)
    {
        Debug.Log("Typed word match a target word: " + typedWord);
    }
}
