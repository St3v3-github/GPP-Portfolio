using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    InputManager inputManager;
    PlayerController playerController;
    EnemyController EnemyController;
    //BossVisualManager bossVisualManager;

    public Rigidbody enemyRB;
/*    public Rigidbody BossRB;*/


    [SerializeField] private LayerMask Ground, Enemy /*,Boss*/;

    [SerializeField] private float attackRange;
    [SerializeField] private bool InAttackRange;
/*    [SerializeField] private bool InBossAttackRange;*/

    [SerializeField] private float bonkUp;
    [SerializeField] private float bonkBack;

    [SerializeField] private float enemyHealth = 10;
    public GameObject enemyObject;

/*    [SerializeField] private float bossHealth = 10;
    public GameObject BossObject;*/

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerController = GetComponent<PlayerController>();
        EnemyController = FindObjectOfType<EnemyController>();
/*        bossVisualManager = FindObjectOfType<BossVisualManager>();*/
    }

    private void Update()
    {
        if (enemyHealth < 0)
        {
            enemyObject.SetActive(false);
        }
/*
        if (bossHealth < 0)
        {
            BossObject.SetActive(false);
        }*/
    }

    private void FixedUpdate()
    {
        InAttackRange = Physics.CheckSphere(transform.position, attackRange, Enemy);

        if (InAttackRange && inputManager.attackInput)
        {
            HandleBonk();
        }

/*        InBossAttackRange = Physics.CheckSphere(transform.position, attackRange, Boss);

        if (InBossAttackRange && inputManager.FightingInput)
        {
            HandleBossAttack();
        }*/
    }

    private void HandleBonk()
    {
        enemyRB.AddForce(transform.up * bonkUp, ForceMode.Impulse);
        enemyRB.AddForce(transform.forward * -bonkBack, ForceMode.Impulse);

        enemyHealth--;
        EnemyController.Damage();

    }

/*    private void HandleBossAttack()
    {
        BossRB.AddForce(transform.up * bonkUp, ForceMode.Impulse);
        BossRB.AddForce(transform.forward * -bonkBack, ForceMode.Impulse);

        bossHealth--;
        bossVisualManager.Damage();

    }*/
}
