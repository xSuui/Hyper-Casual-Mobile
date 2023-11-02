using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{

    public Transform container;

    public List<GameObject> levels;

    /*[Header("Pieces")]
    public List<LevelPieceBase> levelPiecesStart;
    public List<LevelPieceBase> levelPieces;
    public List<LevelPieceBase> levelPiecesEnd;


    public int piecesStartNumber = 3;
    public int piecesNumber = 5;
    public int piecesEndNumber = 1;*/

    public List<LevelPieceBasedSetup> levelPieceBasedSetups;


    public float timeBetweemPieces = .3f;
    
    
    [SerializeField] private int _index;
    private GameObject _currentLevel;

    private List<LevelPieceBase> _spawnedPieces = new List<LevelPieceBase>();
    private LevelPieceBasedSetup _currSetup;

    [Header("Animation")]
    public float scaleDuration = .2f;
    public float scaleTimeBetweenPieces = .1f;
    public Ease ease = Ease.OutBack;

    private void Awake()
    {
        //SpawnNextLevel();
        CreateLevelPieces();
    }

    private void SpawnNextLevel()
    {
        if(_currentLevel != null)
        {
            Destroy(_currentLevel);
            _index++;
            if(_index >= levels.Count)
            {
                ResetLevelIndex();
            }
        }

        _currentLevel = Instantiate(levels[_index], container);
        _currentLevel.transform.localPosition = Vector3.zero;
    }

    private void ResetLevelIndex()
    {
        _index = 0;
    }

    #region
    private void CreateLevelPieces()
    {
        CleanSpawnedPieces();

            if (_currSetup != null)
            {
                _index++;

                if (_index >= levelPieceBasedSetups.Count)
                {
                    ResetLevelIndex();
                }
            }
                {
                    ResetLevelIndex();
                }


            _currSetup = levelPieceBasedSetups[_index];

        for (int i = 0; i < _currSetup.piecesStartNumber; i++)
        {
            CreateLevelPiece(_currSetup.levelPiecesStart);
        }

        for (int i = 0; i < _currSetup.piecesNumber; i++)
        {
            CreateLevelPiece(_currSetup.levelPieces);
        }

        for (int i = 0; i < _currSetup.piecesEndNumber; i++)
        {
            CreateLevelPiece(_currSetup.levelPiecesEnd);
        }

        //ColorManager.Instance.ChangeColorByType(_currSetup.artType);
        StartCoroutine(ScalePiecesByTime());

        //StartCoroutine(CreateLevelPiecesCoroutine());
    }

    #region tentativa de resolução do bug acima color manager
    /*private void CreateLevelPieces()
    {
        CleanSpawnedPieces();
        Debug.Log("Before _currSetup is accessed");
        _currSetup = levelPieceBasedSetups[_index];


        if (levelPieceBasedSetups != null && levelPieceBasedSetups.Count > 0)
        {
            if (_currSetup == null || _index >= levelPieceBasedSetups.Count)
            {
                ResetLevelIndex();
            }

            _currSetup = levelPieceBasedSetups[_index];

            for (int i = 0; i < _currSetup.piecesStartNumber; i++)
            {
                CreateLevelPiece(_currSetup.levelPiecesStart);
            }

            for (int i = 0; i < _currSetup.piecesNumber; i++)
            {
                CreateLevelPiece(_currSetup.levelPieces);
            }

            for (int i = 0; i < _currSetup.piecesEndNumber; i++)
            {
                CreateLevelPiece(_currSetup.levelPiecesEnd);
            }

            ColorManager.Instance.ChangeColorByType(_currSetup.artType);
            StartCoroutine(ScalePiecesByTime());
        }
        else
        {
            Debug.LogError("levelPieceBasedSetups is null or empty. Make sure it's properly initialized.");
        }
    }*/
    #endregion


    IEnumerator ScalePiecesByTime()
    {
        foreach(var p in _spawnedPieces)
        {
            p.transform.localScale = Vector3.zero;
        }

        yield return null;

        for(int i = 0; i < _spawnedPieces.Count; i++)
        {
            _spawnedPieces[i].transform.DOScale(1, scaleDuration).SetEase(ease);
            yield return new WaitForSeconds(scaleTimeBetweenPieces);
        }
    }

    private void CreateLevelPiece(List<LevelPieceBase> list)
    {
        var piece = list[Random.Range(0, list.Count)];
        var spanwedPiece = Instantiate(piece, container);

        if(_spawnedPieces.Count > 0)
        {
            var lastPiece = _spawnedPieces[_spawnedPieces.Count - 1];

            spanwedPiece.transform.position = lastPiece.endPiece.position;
        }
        else
        {
            spanwedPiece.transform.localPosition = Vector3.zero;
        }

        foreach(var p in spanwedPiece.GetComponentsInChildren<ArtPiece>())
        {
            p.ChangePiece(ArtManager.Instance.GetSetupByType(_currSetup.artType).gameObject);
        }

        _spawnedPieces.Add(spanwedPiece);
    }

    private void CleanSpawnedPieces()
    {
        for(int i = _spawnedPieces.Count - 1; i >= 0; i--)
        {
            Destroy(_spawnedPieces[i].gameObject);
        }

        _spawnedPieces.Clear();
    }

    IEnumerator CreateLevelPiecesCoroutine()
    {
        _spawnedPieces = new List<LevelPieceBase>();

        for (int i = 0; i < _currSetup.piecesNumber; i++)
        {
            CreateLevelPiece(_currSetup.levelPieces);
            yield return new WaitForSeconds(timeBetweemPieces);
        }
    }
    #endregion

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            CreateLevelPieces();
            //SpawnNextLevel();
        }
    }
}
