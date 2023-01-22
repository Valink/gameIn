using TMPro;
using UnityEngine;

class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject startVue;
    [SerializeField] private GameObject gameOverVue;
    public static GameManager Instance;

    [SerializeField] private TMP_InputField input;   
    private TMP_InputField placeholder;

    private void Awake()
    {
        Instance = this;
        placeholder = input.placeholder.GetComponent<TMP_InputField>();
    }

    public void GameOver()    
    {
        startVue.SetActive(false);
        gameOverVue.SetActive(true);
        input.text = string.Empty;
        placeholder.text = "Type 'restart'";
    }
    
    public void Start()    
    {
        gameOverVue.SetActive(false);
        startVue.SetActive(false);
        placeholder.text = "Type 'start'";
    }

    public void StartGame()
    {
        gameOverVue.SetActive(false);
        startVue.SetActive(false);
        placeholder.text = "Mash keyboard";
    }

    public void Home()    
    {
        gameOverVue.SetActive(false);
        startVue.SetActive(true);
    }
}