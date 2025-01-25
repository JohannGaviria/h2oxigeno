using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OxygenManager : MonoBehaviour
{
    [Header("Oxygen Settings")]
    public Slider oxygenSlider; // El slider que muestra el nivel de ox�geno
    public float maxOxygen = 100f; // Nivel m�ximo de ox�geno
    public float oxygenDepletionRate = 5f; // Cu�nto ox�geno se pierde por segundo

    [Header("Illumination Settings")]
    public float maxIllumination = 1f; // Iluminaci�n m�xima
    public float minIllumination = 0.2f; // Iluminaci�n m�nima
    public Light playerLight; // Luz del jugador

    [Header("Oxygen Restore Settings")]
    public GameObject nearestOxygenSource; // Objeto m�s cercano que restaura ox�geno
    public float detectionRadius = 10f; // Radio para buscar la fuente m�s cercana

    private float currentOxygen;
    private float currentIllumination;

    void Start()
    {
        // Inicializar ox�geno e iluminaci�n
        currentOxygen = maxOxygen;
        currentIllumination = maxIllumination;
        oxygenSlider.maxValue = maxOxygen;
        oxygenSlider.value = currentOxygen;
    }

    void Update()
    {
        SimulateOxygenDepletion();
        UpdateIllumination();
        CheckOxygenLevel();
    }

    void SimulateOxygenDepletion()
    {
        // Reducir ox�geno con el tiempo
        currentOxygen -= oxygenDepletionRate * Time.deltaTime;
        currentOxygen = Mathf.Clamp(currentOxygen, 0, maxOxygen);
        oxygenSlider.value = currentOxygen;
    }

    void UpdateIllumination()
    {
        // Reducir la iluminaci�n en funci�n del ox�geno
        float illuminationFactor = Mathf.InverseLerp(0, maxOxygen, currentOxygen);
        currentIllumination = Mathf.Lerp(minIllumination, maxIllumination, illuminationFactor);

        if (playerLight != null)
        {
            playerLight.intensity = currentIllumination;
        }
    }

    void CheckOxygenLevel()
    {
        if (currentOxygen <= 0)
        {
            HighlightNearestOxygenSource();
        }
    }

    void HighlightNearestOxygenSource()
    {
        // Encontrar el objeto m�s cercano dentro del radio
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
        float closestDistance = detectionRadius;
        GameObject closestSource = null;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("OxygenSource"))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestSource = collider.gameObject;
                }
            }
        }

        if (closestSource != null)
        {
            nearestOxygenSource = closestSource;
            // Aqu� puedes a�adir efectos visuales como un destello
            Debug.Log($"Destacando fuente de ox�geno: {nearestOxygenSource.name}");
            // Ejemplo de destello:
            StartCoroutine(OxygenSourceBlinkEffect(closestSource));
        }
    }

    IEnumerator OxygenSourceBlinkEffect(GameObject source)
    {
        Renderer renderer = source.GetComponent<Renderer>();
        if (renderer != null)
        {
            for (int i = 0; i < 5; i++)
            {
                renderer.enabled = !renderer.enabled;
                yield return new WaitForSeconds(0.2f);
            }
            renderer.enabled = true;
        }
    }
}
