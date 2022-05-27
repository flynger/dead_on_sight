using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Color color = Color.white;
    public Renderer card;
    // Start is called before the first frame update
    void Start()
    {
        card.material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
