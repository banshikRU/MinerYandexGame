using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    private Transform _player;
    [SerializeField]private GameObject _targetPrefab;
    private Transform _targetTransform;
    public float speed = 5f;
    private bool _reachedTarget = false;
    private bool _isFirstBlock;
    private void Start()
    {
        _player = GameObject.Find("Player").transform;
        _isFirstBlock = false;
        RotateToPlayer();
        _targetTransform =  Instantiate(_targetPrefab,_player.transform.position ,Quaternion.identity).transform;
    }
    void Update()
    {

        if (_targetTransform != null)
        {
            Vector3 direction = _targetTransform.position - transform.position;
            direction.Normalize();
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
            if (Vector3.Distance(transform.position, _targetTransform.position) < 0.1f && !_reachedTarget)
            {
                _reachedTarget = true;
                _targetTransform.position += direction * 10f; 
            }
            else if(Vector3.Distance(transform.position, _targetTransform.position) < 0.1f)
            {
                Destroy(gameObject);
            }
        }
       
    }
    private void RotateToPlayer()
    {
        Vector3 directionn = _player.position - gameObject.transform.position;
        gameObject.transform.rotation= Quaternion.LookRotation(Vector3.forward, directionn);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall" && _isFirstBlock == false)
        {
            //CameraControl.instance.ShakeCamera(3, 3f);
            _isFirstBlock = true;
            collision.GetComponent<Wall>().RevertSprite();
        }
    }

}
