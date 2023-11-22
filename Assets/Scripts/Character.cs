using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public float MovementSpeed;
    float movement;
    public Button AttackButton;
    public BattleHandler battleHandler;
    [HideInInspector]
    public FightStatus playerStatus;
    private HealthSystem healthSystem;
    private Missile missile;

    public Animator animator;

    void Start()
    {
        playerStatus = FightStatus.Idle;
        AttackButton.interactable = false;
        AttackButton.onClick.AddListener(AttackButtpnPressed);
        healthSystem = GetComponent<HealthSystem>();
        missile = GetComponent<Missile>();
    }

    void Update()
    {
        // Moving character
        movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;
        if(movement > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movement < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (Mathf.Abs(movement) < 0.01)
        {
            animator.SetBool("IsRun", false);
        }
        else
        {
            animator.SetBool("IsRun", true);
        }

        switch (playerStatus)
        {
            case FightStatus.Idle:
                Idle();
                break;
            case FightStatus.Decision:
                AppearAttackOptions();
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
        playerStatus = FightStatus.Decision;
    }

    private void AppearAttackOptions()
    {
        //UI will pop up with Attack options
        AttackButton.interactable = true;
    }

    private void AttackStart()
    {
        // Attack will create based on options
        missile.missileStatus = Missile.MissileStatus.Thrown;
        missile.speedDir = (battleHandler.demons[0].transform.position - transform.position).normalized;
        playerStatus = FightStatus.Wait;
    }

    private void GoBackIdle()
    {
        // Player will go back to idle after damging the enemy and send signal to battle handler that its turn has been end.
        battleHandler.demons[battleHandler.enemyCount].Damaged(missile.damage);
        battleHandler.turn = BattleHandler.Turn.EnemyTurn;
        playerStatus = FightStatus.Idle;
    }

    private void Idle()
    {
        //Atack option will disappear
        AttackButton.interactable = false;
    }

    void AttackButtpnPressed()
    {
        playerStatus = FightStatus.AttackStart;
        AttackButton.interactable = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Missile")
        {
            col.gameObject.transform.parent.GetComponent<Missile>().missileStatus = Missile.MissileStatus.Explode;
        }
    }
}
