using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FlappyBirdController : MonoBehaviour {

    public Quaternion forwardPosition;
    public Quaternion fallingPosition;

    public AudioSource source;

    public static bool isGameStarted = false;

    bool isPaused;

    public bool IsDead { get; private set; }

	// Use this for initialization
	public void Start ()
    {
        transform.localPosition = new Vector3(-0.93f, 0, 10);
        source = GetComponent<AudioSource>();
        IsDead = false;
        isGameStarted = false;
        forwardPosition = Quaternion.Euler(0, 0, 20);
        fallingPosition = Quaternion.Euler(0, 0, -90);

        StartCoroutine(AutoFlap());
    }
	
	// Update is called once per frame
	void Update ()
    {
        MoveCameraForward();

        if (Input.GetMouseButtonDown(0) && isGameStarted && !isPaused && !IsDead)
        {
            source.Play();
            Flap();
        }
        //if its not paused, rotate towards the ground
        if(!isPaused)
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(transform.rotation.eulerAngles), fallingPosition, 0.05f);
	}

    void Flap()
    {
        transform.rotation = Quaternion.Lerp(Quaternion.Euler(transform.rotation.eulerAngles), forwardPosition, 7f);
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 150, ForceMode2D.Force);
    }

    void MoveCameraForward()
    {
        if(!isPaused && !IsDead)
            transform.parent.Translate(new Vector3(0.01f, 0));
    }

    IEnumerator AutoFlap()
    {
        while (!isGameStarted)
        {
            if (transform.position.y <= 0)
                Flap();
            yield return new WaitForSeconds(0.1f);
        }
    }

    //if its paused don't fall down(gravity stops working), if its not...do the opposite
    public void Pause(bool isPaused)
    {
        this.isPaused = isPaused;
        if (isPaused)
        {
            GetComponent<Rigidbody2D>().simulated = false;
        }
        else
        {
            GetComponent<Rigidbody2D>().simulated = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Pipe" || collider.gameObject.tag == "Ground")
            Die();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Game_Manager.instance.AddScorePoint();
    }

    void Die()
    {
        if (!IsDead && isGameStarted)
        {
            IsDead = true;
            Game_Manager.instance.Die();
        }
    }
}
