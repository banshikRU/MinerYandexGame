using System;
using System.Collections.Generic;
using UnityEngine;
using AnimatorController = UnityEngine.RuntimeAnimatorController;
using Random = UnityEngine.Random;
public class SmothMovement : MonoBehaviour
{
    [SerializeField] private BackGroundPlacer _generator;
    private float _invokeTime;
    [SerializeField] private float _repeatRate;
    [NonSerialized]public float _damageMultiplier ;
    [SerializeField]private AnimatorController _withHelmet;
    [SerializeField]private AnimatorController _withoutHelmet;
    [SerializeField]private AnimatorController _pirate;
    [SerializeField]private AnimatorController _knight;
    [SerializeField]private BuffManager _buffManager;
    [SerializeField]private AudioClip _pickaxeHit;
    [SerializeField]private AudioClip _drillHit;
    [SerializeField]private AudioClip _bombExplosion;
    [SerializeField]private ContactFilter2D _contactFilter;
    [SerializeField]private GameObject _playerParticle;
    [SerializeField]private float _jumpHeight;
    [SerializeField]private Animator _playerAnimator;
    [SerializeField]private LayerMask _floorLayer;
    [SerializeField]private LayerMask _blockingLayer;
    [SerializeField]private float _timeToMove;
    [SerializeField]private float _timeToJump;
    [SerializeField]private float _superFallTime;
    [SerializeField]private float _smoothFallTime;
    [SerializeField]private int _blocksCanDestroyed;
    public bool _isJump;
    private bool _isMoving;
    public bool _inAir;
    public bool _isSuperFall;
    private float _moveTime;
    private bool _destroyAllBlocksMode;
    private BoxCollider2D _myBoxCollider;
    private Vector3 _startPosition; 
    private Vector3 _targetPosition; 
    private float elapsedTime = 0f; 
    void Start()
    {
        _damageMultiplier = 1f;
        _invokeTime = 0.3f;
        _myBoxCollider = GetComponent<BoxCollider2D>();
        _startPosition = transform.position; 
    }
    void FixedUpdate()
    {
        if (_destroyAllBlocksMode)
        {
            List<Collider2D> _results = new List<Collider2D>();
            Physics2D.OverlapCollider(_myBoxCollider, _contactFilter, _results);
            foreach (var block in _results)
            {
                block.GetComponent<IAmBlock>().DestroyAll();
            }
        }
        if (_targetPosition != Vector3.zero )
        {
            _inAir = true;
            CancelInvoke();
            elapsedTime += Time.fixedDeltaTime;
            float t = Mathf.Clamp01(elapsedTime / _moveTime);
            transform.position = Vector3.Lerp(_startPosition, _targetPosition, t);
            if (t >= 1.0f)
            {
                
                _inAir = false;
                if (_isJump)
                {
                    _isJump = false;
                    AttemptFall();
                }
                else 
                {
                    ResetMovement();
                }

            }
        }
    }
    public void SetAnimatorHelmet()
    {
        _playerAnimator.runtimeAnimatorController = _withHelmet;
    }
    public void SetAnimatorWithoutHelmet()
    {
        _playerAnimator.runtimeAnimatorController = _withoutHelmet;
    }
    public void SetAnimatorPirate()
    {
        _playerAnimator.runtimeAnimatorController = _pirate;
    }
    public void SetAnimatorKnight()
    {
        _playerAnimator.runtimeAnimatorController = _knight;
    }
    public void HitMe()
    {
        _playerAnimator.Play("PlayerDamaged");
    }
    public void AttemptFall()
    {
        float _fallTime;
        int _destroyBlocks;
        if (_isSuperFall)
        {
            _fallTime = _superFallTime;
            _playerParticle.SetActive(false);
            _playerAnimator.Play("SuperHit");
            _destroyAllBlocksMode = true;
            _destroyBlocks = _blocksCanDestroyed;
            _generator.SpawnLevel();
            SoundManager.instance.PlaySingle(_drillHit);
        }
        else
        {
            _fallTime = _smoothFallTime;
            _destroyBlocks = 0;
        }
        _myBoxCollider.enabled = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 10, _floorLayer);
        _myBoxCollider.enabled = true;
        if (hit.collider != null)
        {
            Fall(_fallTime,hit.collider.gameObject.transform.position.y + 0.55f - _destroyBlocks);
        }
    }
    public void SuperFall()
    {
        if (_isMoving && _playerParticle.activeSelf == true)
        {
            _targetPosition = Vector3.zero;
            _isSuperFall = true;
            _isJump = false;
            AttemptFall();
        }
    }
    private void Fall(float _fallTime,float y)
    {
        _isMoving = true;
        _moveTime = _fallTime;
        _startPosition = transform.position;
        _targetPosition = new Vector2(transform.position.x,y);
        elapsedTime = 0f;
    }
    public void StartSmoothMoveDown()
    {
        InvokeRepeating("MoveDown", _invokeTime, _repeatRate);
    }
    public void StartMoveDown()
    {
        InvokeRepeating("MoveDown", 0, _repeatRate);
    }
    private void MoveDown()
    {
        MoveTo(0, -1);
    }
    public void JumpTo()
    {
        if (!_inAir)
        {
            _isMoving = true;
            _isJump = true;
            _playerAnimator.Play("PlayerJump");
            _moveTime = _timeToJump;
            _startPosition = transform.position;
            _targetPosition = _startPosition + new Vector3(0, _jumpHeight);
            elapsedTime = 0f;
        }
    }
    public void MoveTo(float xDir,float yDir)
    {
        if (xDir!= 0)
        {
            if (_inAir)
            {
                return;
            }
            _invokeTime = 0.3f;
        }
        if (CheckForObstacles(xDir,yDir,out RaycastHit2D hit))
        {
            _moveTime = _timeToMove;
            _startPosition = transform.position;
            _targetPosition = _startPosition + new Vector3(xDir, yDir);
            elapsedTime = 0f;
        }
        else
        {
            if (hit.transform.gameObject.tag !="Wall")
            {
                HitBlock(hit.transform.gameObject.GetComponent<IAmBlock>());
            }
            //
            else
            {
                StartSmoothMoveDown();
            }
           //
        }
    }
    private void ResetMovement()
    {
        _generator.CheckForPlayerPosition();
        _destroyAllBlocksMode = false;
        _isJump = false;
        _isSuperFall = false;
        _isMoving = false;
        _targetPosition = Vector3.zero;
        _startPosition = Vector3.zero;
        elapsedTime = 0f;
        StartSmoothMoveDown();

    }
    private bool CheckForObstacles(float xDir, float yDir,out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);
        _myBoxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, _blockingLayer);
        _myBoxCollider.enabled = true;
        if (hit.transform == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void HitBlock(IAmBlock wall)
    {
        int _pickaxeDamage;
        SoundManager.instance.PlayPIckaxe(_pickaxeHit);
        _playerAnimator.Play("PlayerMine");
        int pickaxeDamageMax = Random.Range(2, 5);
        if (_buffManager.IsExtraDamageActive)
        {
            pickaxeDamageMax *= 2;
        }
        _pickaxeDamage = Random.Range(1, pickaxeDamageMax);
        wall.HitMe(_pickaxeDamage * _damageMultiplier, out bool isFloorDestroyed);
        //DamagePopup.Create(gameObject.transform.position + new Vector3(0, 0.5f), _pickaxeDamage*_damageMultiplier, _isCriticalHit);
        if (isFloorDestroyed)
        {
            if (wall.gameObject.TryGetComponent<ExplosionBlock>(out ExplosionBlock e))
            {
                SoundManager.instance.PlaySingle(_bombExplosion);
                AttemptFall();
            }
            else
            {
                CancelInvoke();
                _invokeTime = 0f;
                MoveTo(0, -1);
                
            }
        }
    }
}
