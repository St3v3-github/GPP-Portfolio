using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    InputManager inputManager;

    public GameObject player;
    public GameObject enemy;
    public Rigidbody enemyRB;

    [SerializeField] Vector3 newPos, velocity;
    [SerializeField] private float speed, timer, detectionRad = 20;
    [SerializeField] private bool isMoving, playerDetected;

    [SerializeField] public float attackRange;
    [SerializeField] public bool InAttackRange;
    [SerializeField] private float bonkUp;
    [SerializeField] private float bonkBack;
    [SerializeField] private float enemyHealth = 10;

    MeshRenderer meshRenderer;
    Color originalColour, damageColor = Color.red;
    [SerializeField] private float flashTime = 0.35f;


    void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();

        newPos = transform.position;

        meshRenderer = GetComponent<MeshRenderer>();
        originalColour = meshRenderer.material.color;
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandlePlayerDetection();
        HandleCombat();
    }

    private void HandleMovement()
    {
        if (transform.position == newPos)
        {
            timer = timer + Time.deltaTime;
            if (timer >= 2)
            {
                newPos = new Vector3(transform.position.x + Random.Range(-20, 20), transform.position.y, transform.position.z + Random.Range(-20, 20));
            }
        }
        else
        {
            timer = 0;
            var lookPos = newPos - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);
            transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, Time.deltaTime, speed);
        }
    }

    private void HandlePlayerDetection()
    {
        // finds distance between player & enemy
        var dist = Vector3.Distance(player.transform.position, transform.position);

        //checks to see if player is near enemy
        if (dist <= detectionRad)
        {
            playerDetected = true;
            newPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        }
        else
        {
            playerDetected = false;
        }
    }

    private void HandleCombat()
    {
        if (enemyHealth < 0)
        {
            enemy.SetActive(false);
        }

        // finds distance between player & enemy
        var dist = Vector3.Distance(player.transform.position, transform.position);

        //checks to see if player is near enemy
        if (dist <= attackRange)
        {
            InAttackRange = true;
            if (InAttackRange && inputManager.attackInput)
            {
                enemyRB.AddForce(transform.up * bonkUp, ForceMode.Impulse);
                enemyRB.AddForce(transform.forward * -bonkBack, ForceMode.Impulse);

                enemyHealth--;
                StartCoroutine(EFlash());
            }

            else
            {
                InAttackRange = true;
            }
        }
    }

    public IEnumerator EFlash()
    {
        meshRenderer.material.color = damageColor;
        yield return new WaitForSeconds(flashTime);
        meshRenderer.material.color = originalColour;
    }
}
