using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class character_static
{
    public static string name;
    public static string s_class;
    public static int level;
    public static int experience_points;
    public static float strength;
    public static float dexterity;
    public static float intelligence;
}
public static class Classes
{
    public static Class Mage = new Class("Mage", 0, 0, 1, 3, 1, 2);
    public static Class Fighter = new Class("Fighter", 3, 1, 2, 0, 1, 0);
    public static Class Rogue = new Class("Rogue", 1, 3, 2, 1, 0, 0);
}
public static class Attacks
{
    public static Attack basic_melee = new Attack("Basic Melee-attack", 1, DamageSources.melee, DamageTypes.physical_slashing); 
}

public static class DamageTypes
{
    public static DamageType physical_slashing = new DamageType("Physical Slashing", false, true, false, false, false, false, false, false, false, false, false, false);
    public static DamageType physical_bludgeoning = new DamageType("Physical Bludgeoning", false, false, true, false, false, false, false, false, false, false, false, false);
    public static DamageType physical_piercing = new DamageType("Physical Piercing", false, false, false, true, false, false, false, false, false, false, false, false);
}
public static class DamageSources
{
    public static DamageSource melee = new DamageSource("Melee", false, true, false);
    public static DamageSource ranged = new DamageSource("Ranged", true, false, false);
    public static DamageSource areaOfEffect = new DamageSource("Ranged", false, false, true);
}

