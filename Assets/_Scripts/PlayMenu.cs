using UnityEngine;

public class PlayMenu : MonoBehaviour
{
    [SerializeField]private GameObject _pauseButton;
    [SerializeField] private ConsumablesBomb consumablesBomb;
    private void OnEnable()
    {
        consumablesBomb.InitializeRunTimeBombs();
        if (PlayerPrefs.GetInt("FirstTraining")== 0)
        {
            _pauseButton.SetActive(false);
        }
        else
        {
            _pauseButton.SetActive(true);
        }
    }
    
}
