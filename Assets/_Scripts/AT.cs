using UnityEngine;

public class AT : MonoBehaviour
{
    public void PageVisible()
    {
        YandexManager.ysdk.GameReadyApiStart();
        SoundManager.instance.OnOffAllSfx(true);
        SoundManager.instance.OnOffAllSound(true);
        Debug.Log("���� � ������");
        Time.timeScale = 1F;
        ObstacleGenerator.Instance.isTimerStart =  true;
    }

    public void PageNotVisible()
    {
        YandexManager.ysdk.GameReadyApiStop();
        SoundManager.instance.OnOffAllSfx(false);
        SoundManager.instance.OnOffAllSound(false);
        Debug.Log("���� �� � ������");
        Time.timeScale = 0F;
        ObstacleGenerator.Instance.isTimerStart = false;
    }
}