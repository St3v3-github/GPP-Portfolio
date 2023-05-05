using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ButtonLogic : MonoBehaviour
{
    public bool btnPressable = false;

    private PlayableDirector director;
    public GameObject button;
    public GameObject door;
    public GameObject timeline;

    public Text myText;

    void Awake()
    { 
        door = GameObject.Find("Door");

        director = timeline.GetComponent<PlayableDirector>();
        director.Stop();
    }

    public void DoorOpen()
    {
        director.Play();
    }

private void OnTriggerEnter()
    {
        btnPressable = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myText.text = "Press X to Interact";
        }
    }

    private void OnTriggerExit()
    {
        btnPressable = false;
        myText.text = " ";
    }
}
