using System;
using UnityEngine;


public class StatTracker: MonoBehaviour
{
    
    [SerializeField] private int rangeAttackPerformed = 0;
    [SerializeField] private int meleeAttackPerformed = 0;

    public void RangeAttackPerformed()
    {
        rangeAttackPerformed++;
    }
    
    public void MeleeAttackPerformed()
    {
        meleeAttackPerformed++;
    }
}