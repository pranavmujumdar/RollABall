using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{   
    private Rigidbody rigidbody;
    public int timeoutForRestart;
    public Text scoreText;
    public Text winText;
    public Text livesText;
    private int score; 
    public int lives;
    public float speed;
    private float minspeed;
    private float maxspeed;

    // Start is called before the first frame update
    void Start() 
    {
        rigidbody = GetComponent<Rigidbody>();
        score = 0;
        //lives = 3;
        UpdateScore();
        UpdateLives();
        winText.text = "";
        timeoutForRestart = 5;
        minspeed = speed;
        maxspeed = speed + 5;
    }

    /*
     * This function has the logic to restart the game with pressing r key on the keyboard
     * It is called once per frame
     */ 
    private void Update()
    {
        if(Input.GetKeyDown("r"))
        {
            
            if (CheckRetries())
            {
                lives -= 1;
                UpdateLives();
                transform.position = new Vector3(-9, 0.5f, -9);
            }
            else
            {
                winText.text = "You Lose!";
                Invoke("RestartGame", timeoutForRestart);
            }
        }
    }
    // fixedupdate is called just before performing physics calculations, physics code goes here
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rigidbody.AddForce (movement * speed);
    }
/**
 * This function contains all the triggerEnter actions
 * 1. When the player enters the PickUp box collider it will set increase the score by one and deactivate the trigger
 * 2. When the player enters a teleporter box collider it will check if the certain score is met and display the msg if it does not or it teleports to the other part of the maze
 */
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            score = score + 1;
            UpdateScore();
            // transform.localScale = new Vector3(2,2,2);
        }
        if (other.gameObject.CompareTag("Teleporter"))
        {
            if (score >= 12)
            {
                transform.position = new Vector3(8.63f, 0.5f, 5.54f);
            }
            else
            {
                Invoke("ShowTeleporter1Msg", 0.5f);
            }
        }
            
        if (other.gameObject.CompareTag("Teleporter 2"))
        {
            if (score >= 16)
            {
                transform.position = new Vector3(8.8f, 0.5f, 8.68f);
            }
            else
            {
                Invoke("ShowTeleporter2Msg",0.5f);
            }
        }
            
        if (other.gameObject.CompareTag("Teleporter 3"))
        {
            transform.position = new Vector3(-8.9f, 0.5f, 0.8f);
        }
    }
    /*
     * This function contains all the TriggerExit logic
     * When the player collides with the teleporter trigger,
     * And if the player does not have the predefined score to open the teleporter it displays the msg 
     * HENCE on trigger exit we need to disable the msg to empty
     */
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Teleporter"))
        {
            winText.text = "";
        }

        if (other.gameObject.CompareTag("Teleporter 2"))
        {
            winText.text = "";
        }
    }
    /* This function contains all the Collision enter logic
     * On collision with a size up tag the player size increases
     * On collision with a size down tag the player size decreases
     */ 
    void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("SizeUp"))
        {
            transform.localScale = new Vector3(1.5f,1.5f,1.5f);
            if(speed == maxspeed)
            {
                Debug.Log("Maxspeed");
            }
            else
            {
                speed = speed + 5;
            }
        }
        if(other.gameObject.CompareTag("SizeDown"))
        {
            transform.localScale = new Vector3(1,1,1);
            if (speed == minspeed)
            {
                Debug.Log("Already Minspeed");
            }
            else
            {
                speed = speed - 5;
            }
        }
        if(other.gameObject.CompareTag("Spike"))
        {
            
            if(CheckRetries())
            {
                lives -= 1;
                // SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
                UpdateLives();
                transform.position = new Vector3(-9,0.5f,-9);
            }
            else
            {
                winText.text = "You Lose!";
                Invoke("RestartGame", timeoutForRestart);
            }
        }
    }
    /*
     * This function updates the score and displays it in the left top corner
     * And when the score reaches 19 Win text is displayed
     */ 
    void UpdateScore()
    {
        scoreText.text = "Score: " + score.ToString() + " of 19";
        if(score >= 19)
        {
            winText.text = "You Win!";
            Invoke("RestartGame", timeoutForRestart);
        }
        
    }
    /*
     * This function Updates the lives of the player when it touches the spikes and displays it in the left top corner
     */ 
    void UpdateLives()
    {
        livesText.text = "Lives: " + lives.ToString();
    }
    /*
     * This function checks if the player has retries left, returns a boolean
     */
    bool CheckRetries(){
        if(lives>1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /*
     * This function restarts the game with the samplescene
     */ 
    void RestartGame(){
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
    /*
     * This function displays the text to collect pick ups to open the teleporter1
     */ 
    void ShowTeleporter1Msg()
    {
        winText.text = "Collect 12 Pick ups to open this teleporter";
    }
    /*
     * This function displays the text to collect pick ups to open the teleporter2
     */ 
    void ShowTeleporter2Msg()
    {
        winText.text = "Collect 16 Pick ups to open this teleporter";
    }
}
