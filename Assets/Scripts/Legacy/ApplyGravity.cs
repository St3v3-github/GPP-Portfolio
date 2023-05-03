using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyGravity : MonoBehaviour
{
    [SerializeField] private float gravityConstant = 9.81f;
    
    void Start() {
        Physics.gravity = new Vector3(0f, gravityConstant, 0f); }
}
