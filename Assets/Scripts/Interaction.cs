using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Interaction : MonoBehaviour
{

    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;
    public GameObject displayText;

    // Start is called before the first frame update
    void Start()
    {
        isInRange = false;
        displayText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
            displayText.SetActive(true);
            if (Input.GetKeyDown(interactKey))
            {
                gameObject.GetComponent<Animator>().Play("Button");
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;

            Debug.Log("Player is in range");
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            displayText.SetActive(false);
            Debug.Log("Player is out of range");
        }
    }
}
