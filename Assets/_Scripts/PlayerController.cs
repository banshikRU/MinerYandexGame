using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SmothMovement _smoth;
    [SerializeField] private GameManager _gameManager;
    private bool isPlayerLeft;
    private void Start()
    {
        isPlayerLeft = false;
    }
    private void Update()
    {
        TakeInput();
    }
    private void TakeInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && _gameManager._isPlay)
        {
            _smoth.JumpTo();
        }
        if (Input.GetKeyDown(KeyCode.S) && _gameManager._isPlay)
        {
            _smoth.SuperFall();
        }

        if (Input.GetKeyDown(KeyCode.A) && _gameManager._isPlay)
        {
            if (isPlayerLeft == false)
            {
                isPlayerLeft = true;
                transform.localScale = new Vector3(-1, 1);
            }
            _smoth.CancelInvoke();
            _smoth.MoveTo(-1, 0);
        }

        if (Input.GetKeyDown(KeyCode.D) && _gameManager._isPlay)
        {
            if (isPlayerLeft == true)
            {
                isPlayerLeft = false;
                transform.localScale = new Vector3(1, 1);
            }
            _smoth.CancelInvoke();
            _smoth.MoveTo(1,0);
        }
        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerPrefs.DeleteAll();
        }
#endif
    }
    public void Up()
    {
            _smoth.JumpTo();
    }
    public void Down()
    {
            _smoth.SuperFall();
    }
    public void Left()
    {
        if (isPlayerLeft == false)
        {
            isPlayerLeft = true;
            transform.localScale = new Vector3(-1, 1);
        }
            _smoth.MoveTo(-1, 0);
    }
    public void Rigth()
    {
        if (isPlayerLeft == true)
        {
            isPlayerLeft = false;
            transform.localScale = new Vector3(1, 1);
        }
            _smoth.MoveTo(1, 0);
    }
}
