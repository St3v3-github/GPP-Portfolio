using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpeedBoost : MonoBehaviour
{
    public float multiplier = 2f;
    public float waitTime = 2f;

    public Text myText;

    public GameObject Player;
    public ParticleSystem particles;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myText.text = "Speedboost Active";
            StartCoroutine(Pickup(other));
        }
    }

    IEnumerator Pickup(Collider player) 
    {
       PlayerController playerController =  player.GetComponent<PlayerController>();
        playerController.runSpeed *= multiplier;
        //playerController.sprintSpeed *= multiplier;

        gameObject.transform.position = new Vector3(0, -50, 0);

        var particleInstance = Instantiate(particles, Player.transform.position, Quaternion.identity);
        particleInstance.transform.SetParent(Player.transform);

        yield return new WaitForSeconds(waitTime);

        myText.text = " ";

        particleInstance.Stop();
        Destroy(particleInstance);

        playerController.runSpeed /= multiplier;
        //playerController.sprintSpeed /= multiplier;

        Destroy(gameObject);
    }

}
