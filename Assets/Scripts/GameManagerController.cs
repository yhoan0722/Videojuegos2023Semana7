using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerController : MonoBehaviour
{
    public Text textovida;
    public Text textoBalas;
    public Text textoZombie;
    public Text textoLlave;

    public int contadorvida;
    // public int contadorBalas;
    public int contadorZombie;
    public int contadorllave;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        contadorvida = 3;
        // contadorBalas = 0;
        contadorZombie = 0;
        contadorllave = 0;
        printVidas();
        printBalas(5);
        printZombie();
        printLlave();
    }

    public void perderVida() {
        contadorvida--;
        printVidas();
        if(contadorvida == 0) {
            Time.timeScale = 0;
            audioSource.Stop();
        }
    }
    // public void ganarBala() {
    //     contadorBalas++;
    //     printBalas();
    // }

    private void printVidas() {
        textovida.text = "VIDAS: " + contadorvida;
    }
    private void printLlave() {
        textoLlave.text = "Llave: " + contadorllave;
    }
    public void printBalas(int contadorBalas) {
        textoBalas.text = "BALAS: " + contadorBalas;
    }
    private void printZombie() {
        textoZombie.text = "Puntos: " + contadorZombie;
    }
    public void matarZombie() {
        contadorZombie++;
        printZombie();
    }
    public void GanarLlave() {
        contadorllave++;
        printLlave();
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
