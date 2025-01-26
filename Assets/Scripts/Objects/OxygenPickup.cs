using UnityEngine;

public class OxygenPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el jugador toc� el objeto
        if (collision.CompareTag("Player"))
        {
            // Accede al script OxygenManager desde un GameObject que contiene el script
            OxygenManager oxygenManager = FindObjectOfType<OxygenManager>();

            // Verifica si el OxygenManager est� presente en la escena
            if (oxygenManager != null)
            {
                oxygenManager.RestoreOxygen(); // Restaura el ox�geno al 100%
                Debug.Log("Ox�geno restaurado.");
            }
            else
            {
                Debug.LogWarning("No se encontr� OxygenManager en la escena.");
            }

            // Destruir el objeto despu�s de ser recogido
            Destroy(gameObject);
        }
    }
}
