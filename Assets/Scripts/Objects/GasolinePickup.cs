using UnityEngine;

public class GasolinePickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el jugador toc� el objeto
        if (collision.CompareTag("Player"))
        {
            // Accede al script GasolineManager desde un GameObject que contiene el script
            GasolineManager gasolineManager = FindObjectOfType<GasolineManager>();
            if (gasolineManager != null)
            {
                gasolineManager.FillGasoline(); // Llenar gasolina
            }

            // Destruir el objeto despu�s de ser recogido
            Destroy(gameObject);
        }
    }
}
