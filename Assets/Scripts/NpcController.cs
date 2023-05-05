using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    bool live = true;
    // Start is called before the first frame update
    SpriteRenderer sr;
    Rigidbody2D rb;
    Animator animator;
    public GameObject bala; 
    // Audio 
    public AudioClip saltosound;
    public AudioClip balasound;
    private AudioSource audioSource;

    public GameManagerController gameManagerController;
    // Estados del personaje
    const int DEAD = 100; 
    const int IDLE = 0; 
    const int RUN = 1;
    const int JUMP = 2;
    
    const int vStop = 0;
    const int vRun = 10;
    const int jumForce = 5;

    public int balas = 5;
    //public int llave = 0;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        gameManagerController = FindObjectOfType<GameManagerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(live) {
            // rb.velocity = new Vector2(vStop,rb.velocity.y);
            // SetAnimacion(IDLE);

            
            if(Input.GetKeyDown(KeyCode.RightArrow)) {
                Derecha();
            }
            if(Input.GetKeyUp(KeyCode.RightArrow)) {
                DetenerDerecha();
            }
            if(Input.GetKeyDown(KeyCode.LeftArrow)) {
                Izquierda();
            }
             if(Input.GetKeyUp(KeyCode.LeftArrow)) {
                DetenerIzquierda();
            }

            if(Input.GetKeyUp(KeyCode.F) && balas > 0) {
                Disparar();
            }
            if(Input.GetKeyUp(KeyCode.Space)) {
               Saltar();
            }
        }

       
    }
    void SetAnimacion(int animacion) {
        animator.SetInteger("Estado",animacion);
    }

    void crearBala(float velocidad) {
        
        if(sr.flipX == false){
            var posicion =transform.position + new Vector3(1.5f,0,0);
            var gb = Instantiate(bala, posicion ,Quaternion.identity);
            var controlador = gb.GetComponent<BalaControllar>();
            controlador.darvelocidadx(5f);
        } else {
            var posicion =transform.position + new Vector3(-1.5f,0,0);
            var gb = Instantiate(bala,posicion,Quaternion.identity);
            var controlador = gb.GetComponent<BalaControllar>();
            controlador.darvelocidadx(-5f);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag ==  "Enemy") {
            gameManagerController.perderVida();
        }
        if(other.gameObject.tag ==  "PBala") {
            this.ganarBalas();
            audioSource.PlayOneShot(balasound);
            gameManagerController.printBalas(balas);
        }
       
    }

    void ganarBalas() {
        balas = balas + 5;
    }
   
    public void Saltar() {
        rb.AddForce(new Vector2(0,jumForce),ForceMode2D.Impulse);
        SetAnimacion(JUMP);
        audioSource.PlayOneShot(saltosound);
    }

     public void Derecha(){
        sr.flipX = false ;
        rb.velocity = new Vector2(vRun,rb.velocity.y);
        SetAnimacion(RUN);
    }

    public void DetenerDerecha(){
        sr.flipX = false ;
        rb.velocity = new Vector2(0,rb.velocity.y);
        SetAnimacion(IDLE);
    }
    public void Izquierda() {
        sr.flipX = true;
        rb.velocity = new Vector2(-vRun,rb.velocity.y);
        SetAnimacion(RUN);
    }

    public void DetenerIzquierda(){
        sr.flipX = true;
        rb.velocity = new Vector2(0,rb.velocity.y);
        SetAnimacion(IDLE);
    }
    public void Disparar() {
        balas = balas-1;
        crearBala(vRun);
        gameManagerController.printBalas(balas);
    }

    
    
}
