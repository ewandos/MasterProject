using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Attack : Action_Base
{
    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] { typeof(Goal_Attack) });

    Goal_Attack _attackGoal;
    [SerializeField] 
    private float firerate = 1.2f;
    [SerializeField] private float nexTimeToFire = 0f;

    public override List<System.Type> GetSupportedGoals()
    {
        return SupportedGoals;
    }

    public override float GetCost()
    {
        return 0;
    }

    public override void OnActivated(Goal_Base _linkedGoal)
    {
        base.OnActivated(_linkedGoal);

        //choose attack animation to play

        
        Animator anim = GetComponent<Animator>();
        Audio audio = GetComponentInChildren<Audio>();
        DamageTrigger damageTrigger = GetComponentInChildren<DamageTrigger>();

        //spawn collider here
        if (Time.time >= nexTimeToFire)
        {
            nexTimeToFire = Time.time + firerate;

            audio.playAttackBossAudio();
            anim.SetBool("isAttacking", true);
            damageTrigger.CreateDamageThingForSeconds();
        }
    }

    public override void OnDeactivated()
    {
        base.OnDeactivated();
        Animator anim = GetComponent<Animator>();
        anim.SetBool("isAttacking", false);
        _attackGoal = null;
    }

    public override void OnTick()
    {
        OnActivated(LinkedGoal);
    }
}