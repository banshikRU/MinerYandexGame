using UnityEngine;

public class PlayerProgress : MonoBehaviour
{
    [SerializeField] private bool isTrainingFinish;
    public static PlayerProgress instance;
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
       
    }
    public void Initialize()
    {
        if (!PlayerPrefs.HasKey("Coins"))
        {
            PlayerPrefs.SetInt("Coins", 0);
            Debug.Log("Coins Set!");
        }
        if (!PlayerPrefs.HasKey("Bombs"))
        {
            PlayerPrefs.SetInt("Bombs", 0);
            Debug.Log("Bombs Set!");
        }
        if (!PlayerPrefs.HasKey("Scores"))
        {
            PlayerPrefs.SetInt("Scores", 0);
            Debug.Log("Scores Set!");
        }
        if (!PlayerPrefs.HasKey("FirstScinBuy"))
        {
            PlayerPrefs.SetInt("FirstScinBuy", 0);
            Debug.Log("FirstScinBuy Set!");
        }
        if (!PlayerPrefs.HasKey("SecondScinBuy"))
        {
            PlayerPrefs.SetInt("SecondScinBuy", 0);
            Debug.Log("SecondScinBuy Set!");
        }
        if (!PlayerPrefs.HasKey("ThirdScinBuy"))
        {
            PlayerPrefs.SetInt("ThirdScinBuy", 0);
            Debug.Log("ThirdScinBuy Set!");
        }
        if (!PlayerPrefs.HasKey("FirstTraining"))
        {
            PlayerPrefs.SetInt("FirstTraining", isTrainingFinish == true ? 1 : 0);
            Debug.Log("FirstTraining Set!");
        }
        if (!PlayerPrefs.HasKey("LastTraining"))
        {
            PlayerPrefs.SetInt("LastTraining", isTrainingFinish == true ? 1 : 0);
            Debug.Log("LastTraining Set!");
        }
        if (!PlayerPrefs.HasKey("FirstSuperJump"))
        {
            PlayerPrefs.SetInt("FirstSuperJump", isTrainingFinish == true ? 1 : 0);
            Debug.Log("FirstSuperJump Set!");
        }
        if (!PlayerPrefs.HasKey("GameRated"))
        {
            PlayerPrefs.SetInt("GameRated", 0);
            Debug.Log("GameRated Set!");
        }
    }
}
