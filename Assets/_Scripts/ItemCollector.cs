using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    private int collected = 0;
    public TextMeshProUGUI tMPro;
    [SerializeField] private AudioSource collectSound;
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pineapple"))
        {
            
            Debug.Log("Collision");
            Destroy(other.gameObject);
            collected++;
            tMPro.text = "Pineapples: " + collected;
            collectSound.Play();
        }
    }
    
}