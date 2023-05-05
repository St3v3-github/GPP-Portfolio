using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Powerup : MonoBehaviour
{
    public float waitTime = 2f;
    public bool aquired = false;

    public Text myText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myText.text = "Double Jump Active";
            StartCoroutine(Pickup(other));
        }
    }

    IEnumerator Pickup(Collider player)
    {
        //PhysicsManager physicsManager = player.GetComponent<PhysicsManager>();

        //powerup goes here
        //playerMotion.runSpeed *= multiplier;

        aquired = true;
       
        gameObject.transform.position = new Vector3(0, -500, 0);

        yield return new WaitForSeconds(waitTime);

        //powerup ends here
        //playerMotion.runSpeed /= multiplier;

        Destroy(gameObject);
    }

}
