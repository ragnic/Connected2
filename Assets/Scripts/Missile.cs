using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public enum MissileType
    {
        Normal
    }
    public enum MissileStatus
    {
        Idle,
        Thrown,
        InAir,
        Explode
    }

    public MissileType missleType;
    public float speed = 10;
    public Transform missileTransform;
    [HideInInspector]
    public Vector3 speedDir;
    [HideInInspector]
    public MissileStatus missileStatus;
    [HideInInspector]
    public Damage damage;

    private Transform instantiatedMissile;

    void Start()
    {
        switch (missleType)
        {
            case MissileType.Normal:
                damage = new Damage(DamageType.Physical, 20);
                break;
        }
        missileStatus = MissileStatus.Idle;
    }

    void Update()
    {
        switch (missileStatus)
        {
            case MissileStatus.Idle:
                break;
            case MissileStatus.Thrown:
                Vector3 position = transform.position;
                instantiatedMissile = Instantiate(missileTransform, position, Quaternion.identity);
                instantiatedMissile.parent = transform;
                missileStatus = MissileStatus.InAir;
                break;
            case MissileStatus.InAir:
                instantiatedMissile.position += speed * Time.deltaTime * speedDir;
                break;
            case MissileStatus.Explode:
                Destroy(instantiatedMissile.gameObject);
                if (GetComponent<Character>() != null)
                {
                    GetComponent<Character>().playerStatus = FightStatus.AttackEnd;
                }
                else if (GetComponent<Demon>() != null)
                {
                    GetComponent<Demon>().demonStatus = FightStatus.AttackEnd;
                }
                missileStatus = MissileStatus.Idle;
                break;
        }
    }
}
