using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{

    [Header("weapon base stats")]
    [SerializeField] protected float timeBetweenAttacks;
    [SerializeField] protected float chargeUpTime;
    [SerializeField, Range(0, 1)] protected float minChargePercent;
    [SerializeField] private bool isFullyAutomatic;

    private Coroutine _currentFireTimer;
    private bool _isOnCooldown;
    protected float _currentChargeTime;

    private WaitForSeconds _coolDownWait;
    private WaitUntil _coolDownEnforce;

    private void Start()
    {
        _coolDownWait = new WaitForSeconds(timeBetweenAttacks);
        _coolDownEnforce = new WaitUntil(() => !_isOnCooldown);
    }

    public void StartShooting()
    {
       _currentFireTimer = StartCoroutine(ReFireTimer());
    }

    public void StopShooting()
    {
        StopCoroutine(_currentFireTimer);

        float percent = _currentChargeTime / chargeUpTime;
        if(percent != 0) TryAttack(percent);
    }

    

    private IEnumerator CoolDownTimer()
    {

        _isOnCooldown = true;
        yield return _coolDownWait;
        _isOnCooldown = false;
    }

    private IEnumerator ReFireTimer()
    {
        print("waiting for cooldown");
        yield return _coolDownEnforce;
        print("post cooldown");

        while (_currentChargeTime < chargeUpTime)
        {
            _currentChargeTime += Time.deltaTime;
            yield return null;
        }

        TryAttack(1);
        yield return null;
    }

    private void TryAttack(float percent)
    {
        _currentChargeTime = 0;
        if(!CanAttack(percent)) return;
        
        Attack(percent);

        StartCoroutine(CoolDownTimer());

        if (isFullyAutomatic && percent >= 1)
        {
            _currentFireTimer = StartCoroutine(ReFireTimer());
        }
    }

    protected virtual bool CanAttack(float percent)
    {
        return !_isOnCooldown && percent >= minChargePercent;
    }
    
    protected abstract void Attack(float percent);

}
