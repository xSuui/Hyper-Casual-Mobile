using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using TMPro;
using DG.Tweening;

public class PlayerController : Singleton<PlayerController>
{
    //publics
    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 1f;

    public float speed = 1f;

    public Ease ease = Ease.OutBack;

    public string tagToCheckEnemy = "Enemy";
    public string tagToCheckEndLine = "EndLine";

    public GameObject endScreen;
    public bool invencible = true;

    [Header("Text")]
    public TextMeshPro uiTextPowerUp;

    [Header("Coin Setup")]
    public GameObject coinCollector;

    [Header("Animation")]
    public AnimatorManager animatorManager;

    [SerializeField] private BounceHelper _bounceHelper;


    //privates
    private bool _canRun;
    private Vector3 _pos;
    private float _currentSpeed;
    private Vector3 _startPosition;
    private float _baseSpeedToAnimation = 7;

    public void ScaleCharacterSmoothly(float scaleValue, float duration, Ease easeType)
    {
        transform.localScale = Vector3.zero; // Comece com escala zero
        transform.DOScale(Vector3.one * scaleValue, duration).SetEase(easeType);
        ScaleCharacterSmoothly(1.0f, 1.0f, Ease.OutBack);

    }


    private void Start()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 1f).SetEase(ease);


        _startPosition = transform.position;
        ResetSpeed();

        //transform.DOScale(Vector3.one, 1f).SetEase(ease);
        //_currentSpeed = speed;
        //_canRun = true;
    }


    public void Bounce()
    {
        if(_bounceHelper != null)
        {
            transform.DOScale(Vector3.one * 1.2f, 1).SetEase(ease)
            .OnComplete(() => transform.DOScale(Vector3.one, 1).SetEase(ease));

            _bounceHelper.Bounce();
        }

    }

    void Update()
    {
        if (!_canRun) return;

        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * _currentSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == tagToCheckEnemy)
        {
            //_canRun = false;
            if(!invencible)
            {
                MoveBack(collision.transform);
                EndGame(AnimatorManager.AnimationType.DEAD);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == tagToCheckEndLine)
        {
            if(!invencible) EndGame();
        }
    }

    private void MoveBack(Transform t)
    {
        t.DOMoveZ(1f, .3f).SetRelative();
    }

    private void EndGame(AnimatorManager.AnimationType animationType = AnimatorManager.AnimationType.IDLE)
    {
        _canRun = false;
        endScreen.SetActive(true);
        animatorManager.Play(animationType);
    }

    public void StartToRun()
    {
        _canRun = true;
        animatorManager.Play(AnimatorManager.AnimationType.RUN, _currentSpeed / _baseSpeedToAnimation);
    }

    #region POWERUPS

    public void CollectPowerUp()
    {
        // Outras ações do Power Up, se necessário.
        Bounce();
    }


    public void SetPowerUpText(string s)
    {
        uiTextPowerUp.text = s;
    }

    public void PowerUpSpeedUp(float f)
    {
        _currentSpeed = f;
    }

    public void ScaleCharacter(float scaleValue, float duration, Ease easeType)
    {
        transform.DOScale(Vector3.one * scaleValue, duration).SetEase(easeType);

        // Para escalar de 1 para 1.2f e depois para 1.
        //ScaleCharacter(1.2f, 0.5f, Ease.OutBack); OnComplete(() => ScaleCharacter(1f, 0.5f, Ease.OutBack));

    }


    /*public void ScaleCharacter(float PowerUpSpeedUp, float duration, Ease easeType)
    {
        transform.DOScale(Vector3.one * PowerUpSpeedUp, duration).SetEase(easeType);
    }*/


    public void SetInvencible(bool b = true)
    {
        invencible = b;
    }

    public void ResetSpeed()
    {
        _currentSpeed = speed;
    }

    public void ChangeHeight(float amount, float duration, float animationDuration, Ease ease)
    {
        /*var p = transform.position;
        p.y = _startPosition.y + amount;
        transform.position = p;*/

        transform.DOMoveY(_startPosition.y + amount, animationDuration).SetEase(ease);//.OnComplete(ResetHeight);
        Invoke(nameof(ResetHeight), duration);
    }

    public void ResetHeight()
    {
        /*var p = transform.position;
        p.y = _startPosition.y;
        transform.position = p;*/

        transform.DOMoveY(_startPosition.y, .1f);
    }

    public void ChangeCoinCollectorSize(float amount)
    {
        coinCollector.transform.localScale = Vector3.one * amount;
    }
    #endregion
}
