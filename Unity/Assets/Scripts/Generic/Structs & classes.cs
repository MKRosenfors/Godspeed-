using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    public string name;
    public float damageValue;
    public DamageSource damageSource;
    public DamageType damageType;
    public Attack(string _name, float _damageValue, DamageSource _damageSource, DamageType _damageType)
    {
        name = _name;
        damageValue = _damageValue;
        damageSource = _damageSource;
        damageType = _damageType;
    }
}

public class TurnAction
{
    public string name;
    public TurnAction(string _name)
    {
        name = _name;
    }
}

public class Class
{
    public string name;
    public float strengthStartMod;
    public float dexterityStartMod;
    public float constitutionStartMod;
    public float intelligenceStartMod;
    public float wisdomStartMod;
    public float charismaStartMod;

    public Class(string _name, float _str, float _dex, float _con, float _int, float _wis, float _cha)
    {
        name = _name;
        strengthStartMod = _str;
        dexterityStartMod = _dex;
        constitutionStartMod = _con;
        intelligenceStartMod = _int;
        wisdomStartMod = _wis;
        charismaStartMod = _cha;
    }
}

public class DamageType
{
    public string name;
    public bool magical;
    public bool slashing;
    public bool bludgeoning;
    public bool piercing;
    public bool fire;
    public bool cold;
    public bool psychic;
    public bool lightning;
    public bool thunder;
    public bool acid;
    public bool poison;
    public bool force;

    public DamageType(string _name, bool _magical, bool _slashing, bool _bludgeoning, bool _piercing, bool _fire, bool _cold, bool _psychic, bool _lightning, bool _thunder, bool _acid, bool _poison, bool _force)
    {
        //Should reconsider amount of damage-types
        name = _name;
        magical = _magical;
        slashing = _slashing;
        bludgeoning = _bludgeoning;
        piercing = _piercing;
        fire = _fire;
        cold = _cold;
        psychic = _psychic;
        lightning = _lightning;
        thunder = _thunder;
        acid = _acid;
        poison = _poison;
        force = _force;
    }
}

public class DamageSource
{
    public string name;
    public bool ranged;
    public bool melee;
    public bool areaOfEffect;

    public DamageSource(string _name, bool _ranged, bool _melee, bool _AoE)
    {
        name = _name;
        ranged = _ranged;
        melee = _melee;
        areaOfEffect = _AoE;
    }
}
