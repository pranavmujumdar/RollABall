using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Text scoreText;
    public Text winText;
    private int score; 
    public float speed; 
    void Start() 
    {
        rigidbody = GetComponent<Rigidbody>();
        score = 0;
        UpdateScore();
        winText.text = "";
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
    }
    void UpdateScore()
    {
        scoreText.text = "Score: " + score.ToString();
        if(score >= 17)
        {
            winText.text = "You Win!";
        }
        
    }
}
