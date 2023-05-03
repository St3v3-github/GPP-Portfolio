using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public float multiplier = 2f;
    public float waitTime = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }

    IEnumerator Pickup(Collider player) 
    {
       PlayerController playerController =  player.GetComponent<PlayerController>();
        playerController.runSpeed *= multiplier;
        //playerController.sprintSpeed *= multiplier;

        gameObject.transform.position = new Vector3(0, -50, 0);

        yield return new WaitForSeconds(waitTime);

        playerController.runSpeed /= multiplier;
        //playerController.sprintSpeed /= multiplier;

        Destroy(gameObject);
    }

}
