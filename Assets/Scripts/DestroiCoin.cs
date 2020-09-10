using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroiCoin : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        switch(col.gameObject.tag)
        {
            case "Personagem":
                print("BATEU");
                Destroy(this.gameObject, 1f);
                break;
        }
    }
}
