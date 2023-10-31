using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableCoin : ItemCollectableBase
{
    //public Collider2D collider;
    public new Collider2D collider;

    protected override void OnCollect()
    {
        base.OnCollect();
        IitemManager.Instance.AddCoins();
        collider.enabled = false;
    }
}
