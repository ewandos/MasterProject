using System;
using UnityEngine;


public class StatTracker: MonoBehaviour
{
    public static StatTracker Instance { get; private set; }
    [SerializeField] private int rangeAttackPerformed;
    [SerializeField] private int meleeAttackPerformed;

    private void Awake()
    {
        Instance = this;
    }

    public int getRangeAttackPerformed()
    {
        return rangeAttackPerformed;
    }
    
    public int getMeleeAttackPerformed()
    {
        return meleeAttackPerformed;
    }
    
    public void RangeAttackPerformed()
    {
        rangeAttackPerformed++;
    }
    
    public void MeleeAttackPerformed()
    {
        meleeAttackPerformed++;
    }

    public bool getMoreMeleeAttacksPerformed()
    {
        if (meleeAttackPerformed > rangeAttackPerformed)
        {
            return true;
        }
        return false;
    }
    
    public bool getMoreRangedAttacksPerformed()
    {
        if (rangeAttackPerformed > meleeAttackPerformed)
        {
            return true;
        }
        return false;
    }
}