using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    public enum Turn {
        PlayerTurn,
        EnemyTurn,
        Wait
    }

    public Transform enemies;
    public Transform characterTransform;
    [HideInInspector]
    public Turn turn;
    [HideInInspector]
    public List<Demon> demons;
    [HideInInspector]
    public Character character;
    [HideInInspector]
    public int enemyCount;

    void Start()
    {
        turn = Turn.PlayerTurn;
        demons = RetrieveDemons(enemies);
        character = characterTransform.GetComponent<Character>();
        enemyCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (turn)
        {
            case Turn.PlayerTurn:
                Debug.Log("Player Turn");
                character.Attack();
                turn = Turn.Wait;
                break;
            case Turn.EnemyTurn:
                Debug.Log("Enemy Turn");
                if (enemyCount < demons.Count)
                {

                    demons[enemyCount].Attack();
                    enemyCount++;
                    turn = Turn.Wait;
                }
                else
                {
                    enemyCount = 0;
                    turn = Turn.PlayerTurn;
                }
                break;
            case Turn.Wait:
                break;
        }

    }
    
    private List<Demon> RetrieveDemons(Transform enemies)
    {
        List<Demon> demons = new List<Demon>();

        foreach (Transform enemy in enemies)
        {
            demons.Add(enemy.GetComponent<Demon>());
        }

        return demons;
    }
       
}
