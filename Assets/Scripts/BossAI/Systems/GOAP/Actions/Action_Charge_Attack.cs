using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Charge_Attack : Action_Base
{
    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] { typeof(Goal_Charge_Attack) });

    Goal_Attack _attackGoal;
    [SerializeField] private DamageTrigger damageTrigger;
    [SerializeField] 
    private float firerate = 1.2f;
    [SerializeField] private float nexTimeToFire = 0f;
    [SerializeField] private float ChargeSpeed = 55f;
    [SerializeField] private float chargeDistance = 3f;
    [SerializeField] private float ChargeAcceleration = 30f;

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

        //spawn collider here
        if (Time.time >= nexTimeToFire)
        {
            nexTimeToFire = Time.time + firerate;
            
            //AudioSource audio = GetComponent<AudioSource>();
            //audio.PlayOneShot(MeleeAudio);
            

            //get direction of player
            //get point to travel to
            //Agent.MoveTo(loc);
            // modify moveto to make faster movement
            //play animation plus activate trigger for damage

            Vector3 location =  damageTrigger.getPositionToChargeTo(chargeDistance);

            Agent.MoveTo(location, ChargeSpeed, ChargeAcceleration);
            
            anim.Play("BossArmature_meele_hit");
            damageTrigger.CreateDamageThingForSeconds();
        }
    }

    public override void OnDeactivated()
    {
        base.OnDeactivated();
        Animator anim = GetComponent<Animator>();
        _attackGoal = null;
    }

    public override void OnTick()
    {
        //var agentPos = Agent.transform.position;
        //var distanceBetween = Vector3.Distance(_attackGoal.MoveTarget, agentPos);
        //if enemy is in range repeat is possible here
        //if (distanceBetween <= _attackGoal.attackRange)
             OnActivated(LinkedGoal);
        
    }
}