using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    public float speed = 6;
    public float jumpSpeed = 8;
    private float gravity = 9.8f;
    private float currentVerticalSpeed = 0;
    private int score = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private bool playerHasControl = true;
    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("Score");
        SetCountText();
        winTextObject.SetActive(false);
        characterController = GetComponent<CharacterController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move(){
        if(!playerHasControl){
            return;
        }
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if(characterController.isGrounded){
            currentVerticalSpeed = 0;
            if(Input.GetAxis("Jump") > 0){
                currentVerticalSpeed = jumpSpeed;
            }
        }
        currentVerticalSpeed -= gravity * Time.deltaTime;
        Vector3 move = transform.forward * verticalInput + transform.right * horizontalInput;
        move.y = currentVerticalSpeed;
        characterController.Move(speed*Time.deltaTime * move);
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PickUp")) {
            UnityEngine.Debug.Log("not boom");
            other.gameObject.SetActive(false);
            score++;
            SetCountText();
        } else 
        if (other.gameObject.CompareTag("Win Level")) {
            other.gameObject.SetActive(false);
            GameObject[] gameObjects;
            gameObjects = GameObject.FindGameObjectsWithTag("PickUp");
            score += gameObjects.Length;//ui element, offset by one is good
            foreach(GameObject bit in gameObjects){
                bit.SetActive(false);
            }
            SetCountText();
        }
    }
    void SetCountText() {
        PlayerPrefs.SetInt("Score", score); // update stored value
        countText.text =  "Score: " + score.ToString();
        GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("PickUp");

        if (gameObjects.Length == 0)
        {
            winTextObject.SetActive(true);
            playerHasControl = false;
            PlayerPrefs.SetInt("Score", 0);
        }
    }
}
