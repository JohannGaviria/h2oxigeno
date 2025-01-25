using UnityEngine;

public class OxygenPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el jugador toc� el objeto
        if (collision.CompareTag("Player"))
        {
            // Accede al script OxygenManager desde el jugador
            OxygenManager oxygenManager = collision.GetComponent<OxygenManager>();
            if (oxygenManager != null)
            {
                oxygenManager.RestoreOxygen(); // Restaura el ox�geno al 100%
            }

            // Destruir el objeto despu�s de ser recogido
            Destroy(gameObject);
        }
    }
}
