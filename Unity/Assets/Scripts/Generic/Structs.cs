using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Attack
{
    public float damageValue;
    public string damageSource;
    public string damageType;
    public Attack(float _damageValue, string _damageSource, string _damageType)
    {
        damageValue = _damageValue;
        damageSource = _damageSource;
        damageType = _damageType;
    }
}
