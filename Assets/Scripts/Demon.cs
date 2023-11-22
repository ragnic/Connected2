using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour
{
    public BattleHandler battleHandler;
    [HideInInspector]
    public FightStatus demonStatus;
    private HealthSystem healthSystem;
    private Missile missile;

    void Start()
    {
        demonStatus = FightStatus.Idle;
        missile = GetComponent<Missile>();
        healthSystem = GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (demonStatus)
        {
            case FightStatus.Idle:
                Idle();
                break;
            case FightStatus.AttackStart:
                AttackStart();
                break;
            case FightStatus.Wait:
                break;
            case FightStatus.AttackEnd:
                GoBackIdle();
                break;
        }
    }
    
    public void Damaged(Damage damage)
    {
        healthSystem.OnDamage(damage.Amount);
    }

    public void Attack()
    {
        demonStatus = FightStatus.AttackStart;
    }

    void AttackStart()
    {
        missile.missileStatus = Missile.MissileStatus.Thrown;
        missile.speedDir = (battleHandler.character.transform.position - transform.position).normalized;
        demonStatus = FightStatus.Wait;
    }

    void GoBackIdle()
    {
        battleHandler.character.Damaged(missile.damage);
        demonStatus = FightStatus.Idle;
        battleHandler.turn = BattleHandler.Turn.EnemyTurn;
    }

    void Idle()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Missile")
        {
            col.gameObject.transform.parent.GetComponent<Missile>().missileStatus = Missile.MissileStatus.Explode;
        }
    }
}
