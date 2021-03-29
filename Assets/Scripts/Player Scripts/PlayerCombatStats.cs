using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombatStats : CombatStats
{

    public delegate void CurrencyUpdateManager();
    public CurrencyUpdateManager currencyUpdateManager;
    public float setIFrames;
    private float iFrames = -1.0f;
    public int currency
    {
        get { return pCurrency; }
        set { pCurrency = Math.Max(value, 0); currencyUpdateManager();}
    }
    private int pCurrency;


    private void Start()
    {
        base.Start();
        currency = 0;
        
    }
    public override void TakeDamage(int damage)
    {
        if (iFrames < 0)
        {
            base.TakeDamage(damage);
            iFrames = setIFrames;
            StartCoroutine(ReduceIFrames());
        }
    }

    IEnumerator ReduceIFrames()
    {
        while (iFrames > 0)
        {
            iFrames -= Time.deltaTime;
            yield return null;
        }
    }

    public void SetIFrames(float iframe)
    {
        iFrames = iframe;
        StartCoroutine(ReduceIFrames());
    }

    void OnDestroy()
    {
        if (currentHealth <= 0)
            Camera.main.GetComponent<CameraSceneTransitionHandler>().PlayerDeathSceneTransition();
    }
}