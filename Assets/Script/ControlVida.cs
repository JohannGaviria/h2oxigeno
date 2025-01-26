using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlVida : MonoBehaviour
{
    public Slider SliderVida;
    public GameObject FillRellenoVida;
    public float vidaMaxima = 100f;
    public float vidaActual;

    void Start()
    {
        vidaActual = vidaMaxima;
        SliderVida.maxValue = vidaMaxima;
        SliderVida.value = vidaActual;

        if(FillRellenoVida != null )
        {
            FillRellenoVida.SetActive(true);
        }
    }
    public void recibirDa�o(float cantidad)
    {
        vidaActual -= cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);
        SliderVida.value = vidaActual;

        if (vidaActual <= 0 && FillRellenoVida != null)
        {
            FillRellenoVida.SetActive(false);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            recibirDa�o(10);
            Debug.Log(vidaActual);
        }
    }
}
