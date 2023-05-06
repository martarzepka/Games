using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondDisplay : MonoBehaviour
{
    public DiamondData diamondData;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = diamondData.position;
    }
}
