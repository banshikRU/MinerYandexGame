using UnityEngine;

[SerializeField]
public abstract class IAmBlock : MonoBehaviour
{
    [SerializeField] private  float _myEndurance;
    [SerializeField] private GameObject _myEnduranceBarPrefab;
    [SerializeField] private LayerMask _floorLayer;
    private HealBar _myCurentEnduranceBar;
    private GameObject _canvas;
    private bool isHitMe;
    private float _maxHealth;
    private float _timeToDeleteHealBar = 1.2f;
    public virtual float MyEndurance { get { return _myEndurance; } set { _myEndurance = value; } }
    private void Start()
    {
        _maxHealth = _myEndurance;
        _canvas = GameObject.FindObjectOfType<Canvas>().gameObject;
        _myCurentEnduranceBar = null;
    }
    private void FixedUpdate()
    {
        if (isHitMe == true)
        {
            _timeToDeleteHealBar-= Time.deltaTime;

            if (_timeToDeleteHealBar <=0)
            {
                isHitMe = false;
                _timeToDeleteHealBar = 1.2f;
                Destroy(_myCurentEnduranceBar.gameObject);
            }
        }
    }
    public void HitMe(float hit, out bool isFloorDestroyed)
    {
        isHitMe = true;
        _timeToDeleteHealBar = 1.2f;
        if (_myCurentEnduranceBar == null)
        {
            _myCurentEnduranceBar = GenerateHillBar(gameObject).GetComponent<HealBar>();

        }
        MyEndurance -= hit;
        _myCurentEnduranceBar.UpdateHealBar((float)MyEndurance / _maxHealth);
        if (MyEndurance <= 0)
        {
            isFloorDestroyed = true;
            DestroyAll();

        }
        else
        {
            isFloorDestroyed = false;
        }
    }
    public virtual void DestroyMe()
    {
        if (_myCurentEnduranceBar != null)
        {
            Destroy(_myCurentEnduranceBar.gameObject);
        }
        Destroy(gameObject);
        ScoreManager._scoreEvent.Invoke(1);
        
    }
    public virtual void DestMe()
    {
        if (_myCurentEnduranceBar != null)
        {
            Destroy(_myCurentEnduranceBar.gameObject);
        }
        Destroy(gameObject);
    }
    public GameObject GenerateHillBar(GameObject gameObject)
    {
        GameObject healBar = Instantiate(_myEnduranceBarPrefab, _canvas.transform);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        healBar.transform.position = screenPos + new Vector3(0,-0.4f);
        return healBar;
    }
    public void DestroyAll()
    {
        LevelBuilder.BuildNewLevel.Invoke();
        RaycastHit2D[] allBlocks;
        DestroyMe();
        allBlocks = Physics2D.RaycastAll(gameObject.transform.position, new Vector2(1, 0), 4, _floorLayer);
        foreach (var block in allBlocks)
        {
            block.transform.gameObject.GetComponent<IAmBlock>().DestMe();

        }
        allBlocks = Physics2D.RaycastAll(gameObject.transform.position, new Vector2(-1, 0), 4, _floorLayer);
        foreach (var block in allBlocks)
        {
            block.transform.gameObject.GetComponent<IAmBlock>().DestMe();

        }
    }
    public void ExplosiveDestroy()
    {
        LevelBuilder.BuildNewLevel.Invoke();
        RaycastHit2D[] allBlocks;
        allBlocks = Physics2D.RaycastAll(gameObject.transform.position, new Vector2(1, 0), 4, _floorLayer);
        foreach (var block in allBlocks)
        {
            block.transform.gameObject.GetComponent<IAmBlock>().DestroyMe();

        }
        allBlocks = Physics2D.RaycastAll(gameObject.transform.position, new Vector2(-1, 0), 4, _floorLayer);
        foreach (var block in allBlocks)
        {
            block.transform.gameObject.GetComponent<IAmBlock>().DestroyMe();

        }
    }
}

