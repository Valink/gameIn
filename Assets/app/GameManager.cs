using TMPro;
using UnityEngine;

class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverVue;
    public static GameManager Instance;

    [SerializeField] private TMP_InputField input;   
    private TMP_Text placeholder;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        gameOverVue.SetActive(false);

        placeholder = input.placeholder.GetComponent<TMP_Text>();
        placeholder.text = "Type 'start'";
    }

    public void GameOver()    
    {
        gameOverVue.SetActive(true);
        placeholder.text = "Type 'restart'";
        input.text = string.Empty;
    }

    public void StartGame()
    {
        gameOverVue.SetActive(false);
        placeholder.text = "Mash keyboard";
    }
}