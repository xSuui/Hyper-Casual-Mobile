using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using DG.Tweening;

public class CoinsAnimationManager : Singleton<CoinsAnimationManager>
{
    public List<ItemCollectableCoin> itens;


    [Header("Animation")]
    public float scaleDuration = .2f;
    public float scaleTimeBetweenPieces = .1f;
    public Ease ease = Ease.OutBack;


    private void Start()
    {
        itens = new List<ItemCollectableCoin>();

       

    }

    public void RegisterCoin(ItemCollectableCoin i)
    {
        if (!itens.Contains(i))
        { 
            itens.Add(i);
            i.transform.localScale = Vector3.zero;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartAnimations();
        }
    }

    public void StartAnimations()
    {
        StartCoroutine(ScalePiecesByTime());
    }

    IEnumerator ScalePiecesByTime()
    {
        foreach (var p in itens)
        {
            p.transform.localScale = Vector3.zero;
        }

        Sort();

        yield return null;

        for (int i = 0; i <itens.Count; i++)
        {
            itens[i].transform.DOScale(1, scaleDuration).SetEase(ease);
            yield return new WaitForSeconds(scaleTimeBetweenPieces);
        }
    }

    private void Sort()
    {
        itens = itens.OrderBy(
            x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();
    }
    
}



#region test de resolução
/*public class CoinsAnimationManager : Singleton<CoinsAnimationManager>
{
    public List<ItemCollectableCoin> itens;

    [Header("Animation")]
    public float scaleDuration = .2f;
    public float scaleTimeBetweenPieces = .1f;
    public Ease ease = Ease.OutBack;

    private void Start()
    {
        itens = new List<ItemCollectableCoin>();
    }

    public void RegisterCoin(ItemCollectableCoin i)
    {
        if (!itens.Contains(i))
        {
            itens.Add(i);
            i.transform.localScale = Vector3.zero;
        }
    }

    public void ClearCoinsList()
    {
        foreach (ItemCollectableCoin coin in itens)
        {
            // Destrua ou desative as moedas antigas na lista
            Destroy(coin.gameObject); // ou coin.gameObject.SetActive(false);
        }
        itens.Clear(); // Limpe a lista
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartAnimations();
        }
    }

    public void StartAnimations()
    {
        ClearCoinsList(); // Limpe a lista antes de iniciar as novas animações
        StartCoroutine(ScalePiecesByTime());
    }

    IEnumerator ScalePiecesByTime()
    {
        foreach (var p in itens)
        {
            p.transform.localScale = Vector3.zero;
        }

        Sort();

        yield return null;

        for (int i = 0; i < itens.Count; i++)
        {
            itens[i].transform.DOScale(1, scaleDuration).SetEase(ease);
            yield return new WaitForSeconds(scaleTimeBetweenPieces);
        }
    }

    private void Sort()
    {
        itens = itens.OrderBy(
            x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();
    }
}*/
#endregion