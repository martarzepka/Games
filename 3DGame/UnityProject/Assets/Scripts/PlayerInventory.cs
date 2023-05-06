using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public int numberOfDiamond { get; private set; }
    public UnityEvent<PlayerInventory> OnDiamondCollected;
    [SerializeField] private AudioSource collectSound;
    public void diamondCollected()
    {
        numberOfDiamond++;
        collectSound.Play();
        OnDiamondCollected?.Invoke(this);
    }
}
