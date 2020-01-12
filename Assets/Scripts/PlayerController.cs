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
    void Start() 
    {
        rigidbody = GetComponent<Rigidbody>();
        score = 0;
        lives = 3;
        UpdateScore();
        UpdateLives();
        winText.text = "";
        timeoutForRestart = 5;
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
    }
    void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("SizeUp"))
        {
            transform.localScale = new Vector3(1.5f,1.5f,1.5f);
        }
        if(other.gameObject.CompareTag("SizeDown"))
        {
            transform.localScale = new Vector3(1,1,1);
        }
        if(other.gameObject.CompareTag("Spike"))
        {
            lives -= 1;
            // SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
            UpdateLives();
            if(CheckRetries())
            {
                transform.position = new Vector3(0,0,0);
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
        if(score >= 17)
        {
            winText.text = "You Win!";
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
}
