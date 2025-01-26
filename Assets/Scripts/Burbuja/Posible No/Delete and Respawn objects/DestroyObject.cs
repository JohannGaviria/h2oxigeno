using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private float timeLimit = 5f; // Tiempo l�mite antes de que el objeto desaparezca
    [SerializeField] private Vector2 respawnAreaMin; // L�mite inferior del �rea de reaparici�n
    [SerializeField] private Vector2 respawnAreaMax; // L�mite superior del �rea de reaparici�n
    [SerializeField] private Collider2D triggerZone; // Trigger alrededor del objeto

    private Rigidbody2D rb; // Componente Rigidbody2D del objeto
    private float timeSinceLastTouch; // Tiempo desde el �ltimo contacto del jugador
    private bool isPlayerInsideTrigger = false; // Verifica si el jugador est� dentro del trigger

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ResetObject(); // Inicializa el objeto

        // Asegura que el trigger est� configurado como un trigger
        if (triggerZone != null)
        {
            triggerZone.isTrigger = true;
        }
    }

    private void Update()
    {
        // Solo cuenta tiempo si el jugador no est� en el trigger
        if (!isPlayerInsideTrigger)
        {
            timeSinceLastTouch += Time.deltaTime;

            // Si el tiempo supera el l�mite, reaparece el objeto
            if (timeSinceLastTouch >= timeLimit)
            {
                Respawn();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Detecta si el jugador entra en el trigger
        if (collision.CompareTag("Player"))
        {
            isPlayerInsideTrigger = true;
            timeSinceLastTouch = 0; // Reinicia el tiempo de desaparici�n
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Detecta si el jugador sale del trigger
        if (collision.CompareTag("Player"))
        {
            isPlayerInsideTrigger = false;
        }
    }

    private void Respawn()
    {
        // Desactiva temporalmente el objeto
        gameObject.SetActive(false);

        // Genera una nueva posici�n dentro del �rea de reaparici�n
        float newX = Random.Range(respawnAreaMin.x, respawnAreaMax.x);
        float newY = Random.Range(respawnAreaMin.y, respawnAreaMax.y);
        transform.position = new Vector2(newX, newY);

        // Reinicia el objeto
        ResetObject();

        // Reactiva el objeto
        gameObject.SetActive(true);
    }

    private void ResetObject()
    {
        if (rb != null)
        {
            rb.gravityScale = 0; // Reinicia la gravedad a 0 cuando reaparece
        }
        timeSinceLastTouch = 0; // Reinicia el tiempo
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja el �rea de reaparici�n en el editor
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(
            (Vector3)(respawnAreaMin + respawnAreaMax) / 2,
            new Vector3(respawnAreaMax.x - respawnAreaMin.x, respawnAreaMax.y - respawnAreaMin.y, 0)
        );
    }
}
