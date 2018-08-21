using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class littleGamesPlayerSwitchColor : MonoBehaviour {
    public float jumpForce = 10f;
    public float rotationPlayerSpeed = 100f;
    public Rigidbody2D circle;

    public string currentColor;
    public SpriteRenderer srPlayer;
    public Color blue;
    public Color yellow;
    public Color pink;
    public Color purple;

    public static int score = 0;
    public Text scoreText;
    public GameObject[] obsticle;
    public GameObject colorChanger;

    // Use this for initialization
    void Start () {
        setReadomColor();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("Jump")|| Input.GetMouseButtonDown(0))
        {
            circle.velocity = Vector2.up * jumpForce;
           // circle.velocity = transform.up * jumpForce;
            rotationPlayerSpeed = -1f * rotationPlayerSpeed;
        }

        transform.Rotate(0f, 0f, rotationPlayerSpeed * Time.deltaTime);

        scoreText.text = score.ToString();
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Scored")
        {
            score++;
            Destroy(collision.gameObject);
            int randomIntNumber = Random.Range(0, obsticle.Length);
            Instantiate(obsticle[randomIntNumber], new Vector2(transform.position.x, transform.position.y + 8f), transform.rotation);
            return;
        }

        if(collision.tag!="ColorChanger" && collision.name!= currentColor)
        {
            Debug.Log("You're Died");
            score = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if(collision.tag=="ColorChanger")
        {
            setReadomColor();
            Destroy(collision.gameObject);
            Instantiate(colorChanger, new Vector2(transform.position.x, transform.position.y + 8f), transform.rotation);
            return;
        }
    }


    void setReadomColor()
    {
        int rand = Random.Range(0, 4);

        switch(rand)
        {
            case 0:
                {
                    currentColor = "Blue";
                    srPlayer.color = blue;
                    break;
                }
            case 1: 
                {
                    currentColor = "Yellow";
                    srPlayer.color = yellow;
                    break;
                }
            case 2:
                {
                    currentColor = "Pink";
                    srPlayer.color = pink;
                        break;

                }
            case 3:
                {
                    currentColor = "Purple";
                    srPlayer.color = purple;
                    break;
                }
            default: { break; }
        }
    }
}
