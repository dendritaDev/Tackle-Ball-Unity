using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public float moveForce;
    public float powerUpForce;

    private GameObject focalPoint;

    private bool hasPowerUp;
    public float powerUpTime = 7;

    public GameObject[] powerUpIndicators;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");

        _rigidbody.AddForce(focalPoint.transform.forward * moveForce * forwardInput);
        
        foreach(GameObject indicator in powerUpIndicators)
        {
            indicator.transform.position = this.transform.position + 0.5f*Vector3.down;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountDown());
        }

        if(other.gameObject.name.CompareTo("KillZone") == 0)
        {
            SceneManager.LoadScene("Prototype 4");
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(hasPowerUp && collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - this.transform.position;
            enemyRigidbody.AddForce(awayFromPlayer * powerUpForce, ForceMode.Impulse);
        }
           
    }

    IEnumerator PowerUpCountDown()
    {
        foreach(GameObject indicator in powerUpIndicators)
        {
            indicator.gameObject.SetActive(true);
            yield return new WaitForSeconds(powerUpTime / powerUpIndicators.Length);
            indicator.gameObject.SetActive(false);
        }

        hasPowerUp = false;

    }
}

