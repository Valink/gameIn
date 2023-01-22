using TMPro;
using UnityEngine;

class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject startVue;
    [SerializeField] private GameObject gameOverVue;
    [SerializeField] private TMP_InputField input;
    
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void GameOver()    
    {
        startVue.SetActive(false);
        gameOverVue.SetActive(true);
        input.text = string.Empty;
    }
    
    public void Start()    
    {
        gameOverVue.SetActive(false);
        startVue.SetActive(false);
    }

    public void Home()    
    {
        gameOverVue.SetActive(false);
        startVue.SetActive(true);
    }
}