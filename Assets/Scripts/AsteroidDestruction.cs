using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDestruction : BaseDestructibleObject
{
    public AudioSource destructionSound; // Viide helile

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet") // Kontrollime kokkupõrget "Bullet" tagiga
        {
            destructionSound.Play(); // Mängi hävitamise heli

            // Siin kutsume välja ülekirjutatud Die() meetodi, mis hõlmab sündmuse saatmist ja objekti hävitamist
            Die();
        }
    }

    protected override void Die()
    {
        // Siin saadetakse sündmus ja hävitatakse objekt
        base.Die(); // Kutsub välja BaseDestructibleObject klassi Die meetodi
        gameObject.SetActive(false); // Selle asemel et objekti hävitada, deaktiveerime selle, et parandada jõudlust
    }
}