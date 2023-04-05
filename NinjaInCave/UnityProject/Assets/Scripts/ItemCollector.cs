using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int diamonds = 0;
    [SerializeField] private AudioSource collectSound;
    [SerializeField] private TextMeshProUGUI diamondText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("diamond"))
        {
            Destroy(collision.gameObject);
            collectSound.Play();
            diamonds++;
            diamondText.text = "Diamonds: " + diamonds + "/3";
        }
    }
}
