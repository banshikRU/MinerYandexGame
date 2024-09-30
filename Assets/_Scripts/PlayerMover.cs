//using System.Collections;
//using System.Collections.Generic;
//using System.Runtime.CompilerServices;
//using Unity.VisualScripting;
//using UnityEditor.Animations;
//using UnityEngine;
//using Random = UnityEngine.Random;

//public class PlayerMover : MonoBehaviour
//{
//    [SerializeField]private float _repeatRate;
//    public GameObject _playerParticle;
//    [SerializeField] private AnimatorController _withHelmet;
//    [SerializeField] private AnimatorController _withoutHelmet;
//    [SerializeField] private AudioClip _pickaxeHit;
//    [SerializeField] private ParticleSystem _playerParticleSystem;
//    [SerializeField] private Animator _playerAnimator;
//    [SerializeField] private BuffManager _buffManager;
//    [SerializeField] private LayerMask _floorLayer;
//    [SerializeField] private LayerMask _blockingLayer;
//    [SerializeField] private float _moveTime;
//    [SerializeField] private float _smoothFallTime;
//    [NonSerialized] public float _damageMultiplier;
//    private float _inverseSmoothFall;
//    [NonSerialized]public  float _inverseSuperFall;
//    [SerializeField] private float _superFallTime;
//    [SerializeField]private ContactFilter2D _contactFilter;
//    private float _pickaxeDamage;
//    [SerializeField] private int _blockCanDestroyed;
//    private bool _destroyAllBlockMode;
//    private BoxCollider2D _myBoxCollider;
//    private Rigidbody2D _myRigidBody;
//    [NonSerialized] public bool _isMoving;
//    [NonSerialized] public bool _isFly;
//    [NonSerialized] public float _inverseMoveTime;
//    public Vector2 _curentVector2;
//    public void SetAnimatorHelmet()
//    {
//        _playerAnimator.runtimeAnimatorController = _withHelmet;
//    }
//    public void SetAnimatorWithoutHelmet()
//    {
//        _playerAnimator.runtimeAnimatorController = _withHelmet;
//    }
//    private void Start()
//    {
//        _damageMultiplier = 1;
//        _isFly = false;
//        _myBoxCollider = GetComponent<BoxCollider2D>();
//        _myRigidBody = GetComponent<Rigidbody2D>();
//        _inverseMoveTime = 1f / _moveTime;
//        _inverseSmoothFall = 1f/_smoothFallTime;
//        _inverseSuperFall = 1f / _superFallTime;

//    }
//    private void Update()
//    {
//        if (_destroyAllBlockMode)
//        {
//            List<Collider2D> _results = new List<Collider2D>();
//            Physics2D.OverlapCollider(_myBoxCollider, _contactFilter, _results);
//            foreach (var block in _results)
//            {
//                block.GetComponent<IAmBlock>().DestroyAll();
//            }
//        }
//    }
//    private IEnumerator SmoothMovement(Vector3 end,bool isLeftOrRight)
//    {
//        _isMoving = true;
//        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
//        while (sqrRemainingDistance > float.Epsilon)
//        {
//            Vector3 newPostion = Vector3.MoveTowards(_myRigidBody.position, end, _inverseMoveTime * Time.fixedDeltaTime);
//            _myRigidBody.MovePosition(newPostion);
//            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
//            yield return null;
//        }
//        _myRigidBody.MovePosition(end);
//        if (_isFly == true)
//        {
//            AttemptFall(_inverseSmoothFall);
//        }
//        _isMoving = false;
//        if (isLeftOrRight)
//        {
//            yield return new WaitForSeconds(0.3F);
//            StartMoveDown();
//        }

//    }
//    public void AttemptFall(float _fallTime)
//    {
//        int _bD;
//        if (_fallTime == _inverseSuperFall)
//        {
//            _playerParticle.SetActive(false);
//            _playerAnimator.Play("SuperHit");
//            StopAllCoroutines();
//            _destroyAllBlockMode = true;
//            _bD = _blockCanDestroyed;
//        }
//        else
//        {
            
//            _bD = 0;
//        }
//        _isMoving = true;
//        _myBoxCollider.enabled = false;
//        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down,10,_floorLayer);
//        _myBoxCollider.enabled = true;
//        if (hit.collider != null)
//        {
//            StartCoroutine(Fall(_fallTime,_curentVector2 = new Vector3(transform.position.x, (hit.collider.gameObject.transform.position.y + 0.55f- _bD), 0)));
//        }
//    }
//    public void AttemptFallich(float _fallTime, float x)
//    {
//        int _bD;
//        if (_fallTime == _inverseSuperFall)
//        {
//            _playerParticle.SetActive(false);
//            _playerAnimator.Play("SuperHit");
//            StopAllCoroutines();
//            _destroyAllBlockMode = true;
//            _bD = _blockCanDestroyed;
//        }
//        else
//        {

//            _bD = 0;
//        }
//        _isMoving = true;
//        _myBoxCollider.enabled = false;
//        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 10, _floorLayer);
//        _myBoxCollider.enabled = true;
//        if (hit.collider != null)
//        {
//            StartCoroutine(Fall(_fallTime, _curentVector2 = new Vector3(x, (hit.collider.gameObject.transform.position.y + 0.55f - _bD), 0)));
//        }
//    }
//    public IEnumerator Fall(float _fallTime, Vector3 end)
//    {
//        yield return new WaitForSeconds(0.2f);
//        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
//        while (sqrRemainingDistance > float.Epsilon)
//        {
//            Vector3 newPostion = Vector3.MoveTowards(_myRigidBody.position, end, _fallTime * Time.fixedDeltaTime);
//            _myRigidBody.MovePosition(newPostion);
//            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
//            yield return null;
//        }

//        _myRigidBody.MovePosition(end);
//        _destroyAllBlockMode = false;
//        _isFly = false;
//        _isMoving = false;
//        StopAllCoroutines();
//    }
//    private bool Move(float xDir, float yDir, out RaycastHit2D hit)
//    {
//        bool isLeftOrRight;
//        Vector2 start = transform.position;
//        Vector2 end = start + new Vector2(xDir, yDir);
//        _myBoxCollider.enabled = false;
//        hit = Physics2D.Linecast(start, end, _blockingLayer);
//        _myBoxCollider.enabled = true;
//        if (hit.transform == null)
//        {
//            if (yDir > 0)
//            {
//                _playerAnimator.Play("PlayerJump");
//                _isFly = true;
//            }
//            if (xDir!= 0)
//            {
//                isLeftOrRight = true;
//            }
//            else
//            {
//                isLeftOrRight = false;
//            }
//            StartCoroutine(SmoothMovement(end,isLeftOrRight));
//            return true;
//        }
//        return false;
//    }
//    public void StartMoveDown()
//    {
//        InvokeRepeating("MoveDown", 0.3f, _repeatRate);
//    }
//    private void MoveDown()
//    {
//        AttemptMove<Wall>(0, -1);
//    }
//    public void AttemptMove<T>(float xDir, float yDir) where T : Component
//    {
//        RaycastHit2D hit;
//        Move(xDir, yDir, out hit);
//        if (hit.transform == null)
//            return;
//        if (hit.transform.gameObject.TryGetComponent<IAmBlock>(out IAmBlock wall))
//        {
//            HitBlock(xDir, yDir,hit,wall);
//        }
//    }
//    private void HitBlock(float xDir, float yDir, RaycastHit2D hit, IAmBlock wall)
//    {
//        SoundManager.instance.PlaySingle(_pickaxeHit);
//        _playerAnimator.Play("PlayerMine");
//       // _playerParticleSystem.Play();
//        int pickaxeDamageMax = Random.Range(2, 5);
//        if (_buffManager.IsExtraDamageActive)
//        {
//            pickaxeDamageMax *= 2;
//        }
//        _pickaxeDamage = Random.Range(1,pickaxeDamageMax);
//        wall.HitMe(_pickaxeDamage * _damageMultiplier, out bool isFloorDestroyed);
//        //DamagePopup.Create(gameObject.transform.position + new Vector3(0, 0.5f), _pickaxeDamage*_damageMultiplier, _isCriticalHit);
//        if (isFloorDestroyed)
//        {
//            if (wall.gameObject.TryGetComponent<ExplosionBlock>(out ExplosionBlock e))
//            {
//                BackGroundGenerator.instance.Invoke();
//                AttemptFall(_inverseSmoothFall);
//            }
//            else
//            {
//                BackGroundGenerator.instance.Invoke();
//                Move(xDir, yDir, out hit);
//            }
//        }
//    }
//}
