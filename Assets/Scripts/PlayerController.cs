using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
    // fixedupdate is called just before performing physics calculations, physics code goes here
    private Rigidbody rigidbody;
    public int timeoutForRestart;
    public Text scoreText;
    public Text winText;
    public Text livesText;
    private int score; 
    private int lives;
    public float speed;
    private float minspeed;
    private float maxspeed;
    void Start() 
    {
        rigidbody = GetComponent<Rigidbody>();
        score = 0;
        lives = 3;
        UpdateScore();
        UpdateLives();
        winText.text = "";
        timeoutForRestart = 5;
        minspeed = speed;
        maxspeed = speed + 5;
    }

    private void Update()
    {
        if(Input.GetKeyDown("r"))
        {
            lives -= 1;
            UpdateLives();
            if (CheckRetries())
            {
                transform.position = new Vector3(-9, 0.5f, -9);
            }
            else
            {
                winText.text = "You Lose!";
                Invoke("RestartGame", timeoutForRestart);
            }
        }
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rigidbody.AddForce (movement * speed);
    }
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
            lives -= 1;
            // SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
            UpdateLives();
            if(CheckRetries())
            {
                transform.position = new Vector3(-9,0.5f,-9);
            }
            else
            {
                winText.text = "You Lose!";
                Invoke("RestartGame", timeoutForRestart);
            }
        }
    }
    void UpdateScore()
    {
        scoreText.text = "Score: " + score.ToString();
        if(score >= 19)
        {
            winText.text = "You Win!";
            Invoke("RestartGame", timeoutForRestart);
        }
        
    }
    void UpdateLives()
    {
        livesText.text = "Lives: " + lives.ToString();
    }
    bool CheckRetries(){
        if(lives>0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void RestartGame(){
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
    void ShowTeleporter1Msg()
    {
        winText.text = "Collect 12 Pick ups to open this teleporter";
    }
    void ShowTeleporter2Msg()
    {
        winText.text = "Collect 16 Pick ups to open this teleporter";
    }
}
