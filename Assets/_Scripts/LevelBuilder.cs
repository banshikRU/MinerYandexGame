using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class BuildNewLevel : UnityEvent { }
public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private List<GameObject> _wallTiles;
    private Queue<GameObject> _generatedWalls = new Queue<GameObject>();   
    [SerializeField] private List<GameObject> _floorTiles;
    [SerializeField] private List<GameObject> _oresTiles;
    [SerializeField] private List<IAmBlock> _buffsTiles;
    private int _currentGeneratingLevel;
    private float _strongBlockMin;
    private float _strongBlockMax;
    private float _minOresSpawn;
    private float _minRareOresSpawn;
    private float _maxRareOresSpawn;
    public static BuildNewLevel BuildNewLevel = new BuildNewLevel();

    public int CurentLevel { get => _currentGeneratingLevel; set => _currentGeneratingLevel = value; }

    public void Initialize()
    {
        BuildNewLevel.AddListener(BuildNewWorldLevel);
        for (int i = 0; i < 10; i++)
        {
            BuildNewWorldLevel();
        }
        CurentLevel = 0;
    }
    public void BuildNewWorldLevel()
    {
        CurentLevel += 1;
        _strongBlockMin = CurentLevel * 0.05f;
        _strongBlockMax = CurentLevel * 0.03f;
        _minOresSpawn = CurentLevel * 0.01f;
        _minRareOresSpawn = CurentLevel * 0.05f;
        _maxRareOresSpawn = CurentLevel * 0.03f;
        if (_strongBlockMin >= 2)
        {
            _strongBlockMin = 2;
        }
        if (_strongBlockMax >= _floorTiles.Count)
        {
            _strongBlockMax = _floorTiles.Count;
        }
        if (_minRareOresSpawn >= 1)
        {
            _minRareOresSpawn = 1;
        }
        if (_maxRareOresSpawn >= _oresTiles.Count)
        {
            _maxRareOresSpawn = _oresTiles.Count;
        }
        if (_minOresSpawn >= 40)
        {
            _minOresSpawn = 40;
        }
        for (float x = transform.position.x; x <= 4; x++) 
        {
            if ( x == transform.position.x || x == 4)
            {
                GameObject wall =  Instantiate(_wallTiles[Random.Range(0, _wallTiles.Count )],new Vector3(x, transform.position.y,0),Quaternion.identity);
                CheckGeneratedWallCount(wall);
            }
            else
            {
                if (Random.Range(_minOresSpawn,100)>80)
                {
                    Instantiate(_oresTiles[Random.Range(0 + (int)Random.Range(0, _minRareOresSpawn), (int)_maxRareOresSpawn)], new Vector3(x, transform.position.y, 0), Quaternion.identity);
                }
                else
                {
                    Instantiate(_floorTiles[Random.Range(0 + (int)Random.Range(0, _strongBlockMin), (int)_strongBlockMax)], new Vector3(x, transform.position.y, 0), Quaternion.identity);
                }
            }
        }
        GenerateBuffTile();
        ChangeGeneratorPosition();

    }
    private void CheckGeneratedWallCount(GameObject wall)
    {
        _generatedWalls.Enqueue(wall);
        if (_generatedWalls.Count>= 60)
        {
            Destroy(_generatedWalls.Dequeue());
        }
    }
    private void GenerateBuffTile()
    {
        if (Random.Range(0, 100) > 95)
        {
            int x = Random.Range(-1, 4);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(x, transform.position.y), Vector2.zero, Mathf.Infinity);
            Destroy(hit.transform.gameObject);
            IAmBlock _spawnedBlock = Instantiate(_buffsTiles[Random.Range(0, _buffsTiles.Count)], new Vector3(x, transform.position.y, 0), Quaternion.identity);
            _spawnedBlock.Init();
        }
    }
    private void ChangeGeneratorPosition()
    {
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - 1, 0);
    }
}
