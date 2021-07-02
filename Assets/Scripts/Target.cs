using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    public AudioClip blastSound;
    public AudioClip gameOverSound;
    private float minSpeed = 15;
    private float maxSpeed = 17.5f;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -2;

    public int pointValue = 5;
    public ParticleSystem explosionPartical;
    // Start is called before the first frame update
    void Start()
    {
        targetRb = gameObject.GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque());
        targetRb.position = RandomSpawnPos();
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            gameManager.gameAudio.PlayOneShot(blastSound, 1.0f);
            Instantiate(explosionPartical, gameObject.transform.position, explosionPartical.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("good"))
        {
            gameManager.gameAudio.PlayOneShot(gameOverSound, 1.0f);
            gameManager.DecreaseLives();
        }
        Destroy(gameObject);
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    private Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}
