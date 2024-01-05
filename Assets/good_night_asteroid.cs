using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
     public AudioSource destructionSound; // Viide AudioSource komponendile
    // Start is called before the first frame update
     void OnCollisionEnter(Collision collision)
    {
        // OnCollisionEnter on Unity sündmus, mis käivitub, kui see objekt põrkub teise objektiga.
        if (collision.gameObject.tag == "Bullet") // Kontrolli, kas kokkupõrge toimus objektiga, mille silt (tag) on "Bullet".
        {
            destructionSound.Play(); // Mängi heliefekti, Esmalt impordi helifail Unity projekti. Unitys loo uus AudioSource komponent ja määra see asteroidi objektile.
           // Destroy(gameObject); // Käsk 'Destroy' hävitab selle GameObject'i, millele skript on kinnitatud.
           // Selle asemel, joudluse parandamiseks
            gameObject.SetActive(false);
        }
    }
}
