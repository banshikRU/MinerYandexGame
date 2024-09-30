using UnityEngine;
public class Bootstrap : MonoBehaviour
{
    [SerializeField] private ConsumablesBomb ConsumablesBomb;
    [SerializeField] private MarketMenu MarketMenu;
    [SerializeField] private LevelBuilder LevelBuilder;
    [SerializeField] private GameManager GameManager;
    [SerializeField] private CoinManager CoinManager;
    [SerializeField] private PlayerProgress PlayerProgress;
    [SerializeField] private Language Language;
    private void Start()
    {
        PlayerProgress.Initialize();//Load all extern info player
        Language.Initialize();
        ConsumablesBomb.Initialize();
        CoinManager.Initialize(); // Load coinsCount for MainMenu
        MarketMenu.Initialize();
        GameManager.Initialize(); // Load MainScene and Check for IsDesctop()
        LevelBuilder.Initialize(); // LoadScene
    }
}
