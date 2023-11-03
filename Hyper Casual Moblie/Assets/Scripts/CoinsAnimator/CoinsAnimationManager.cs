using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using DG.Tweening;

#region resolução
public class CoinsAnimationManager : Singleton<CoinsAnimationManager>
{
    public List<ItemCollectableCoin> itens;

    [Header("Animation")]
    public float scaleDuration = 0.2f;
    public float scaleTimeBetweenPieces = 0.1f;
    public Ease ease = Ease.OutBack;

    private void Start()
    {
        itens = new List<ItemCollectableCoin>();
    }

    public void RegisterCoin(ItemCollectableCoin i)
    {
        if (i != null && i.transform != null && !itens.Contains(i))
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
        // Filtrar objetos não nulos na lista
        var validCoins = itens.Where(coin => coin != null).ToList();
        StartCoroutine(ScalePiecesByTime(validCoins));
    }

    IEnumerator ScalePiecesByTime(List<ItemCollectableCoin> validCoins)
    {
        foreach (var p in validCoins)
        {
            if (p != null && p.transform != null)
            {
                p.transform.localScale = Vector3.zero;
            }
        }

        Sort(validCoins);

        yield return null;

        for (int i = 0; i < validCoins.Count; i++)
        {
            if (validCoins[i] != null && validCoins[i].transform != null)
            {
                validCoins[i].transform.DOScale(1, scaleDuration).SetEase(ease);
                yield return new WaitForSeconds(scaleTimeBetweenPieces);
            }
        }
    }

    
    private void Sort(List<ItemCollectableCoin> validCoins)
    {
        validCoins.Sort((a, b) =>
        {
            float distanceA = Vector3.Distance(transform.position, a.transform.position);
            float distanceB = Vector3.Distance(transform.position, b.transform.position);
            return distanceA.CompareTo(distanceB);
        });
    }



}
#endregion


#region codes antigos
    /*if (!itens.Contains(i))
        {
            itens.Add(i);
            i.transform.localScale = Vector3.zero;
        }
    /*IEnumerator ScalePiecesByTime(List<ItemCollectableCoin> validCoins)
    {
    foreach (var p in validCoins)
    {
        p.transform.localScale = Vector3.zero;
    }

    Sort(validCoins);

    yield return null;

    for (int i = 0; i < validCoins.Count; i++)
    {
        validCoins[i].transform.DOScale(1, scaleDuration).SetEase(ease);
        yield return new WaitForSeconds(scaleTimeBetweenPieces);
    }
}
     private void Sort(List<ItemCollectableCoin> validCoins)
{
    validCoins = validCoins.OrderBy(
        x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();
}*/
#endregion

#region code da aula
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
    
}*/
#endregion