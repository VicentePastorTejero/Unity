using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const double V = 0.8;

    // Start is called before the first frame update
    [SerializeField] private float _velocidad = 3f;
    [SerializeField] private float _fuerzaSalto = 20f;
    [SerializeField] private bool _inGround;
    [SerializeField] private bool _isWin;
    [SerializeField] private Transform spawnPoint;
    
    [SerializeField] private LayerMask layerMask;
    //[SerializeField] private FixedJoystick joystick;
    //[SerializeField] private Button botonSalto;
    //[SerializeField] private Button botonDisparo;

    //VIDA MANUEL
    //[SerializeField] private float vidaTotal = 100f;
    private float vidaActual;
    private bool coolDownSalto = true;
    //private bool coolDownDisparo = true;
    private bool coolDownMuerte;

    //DISPARO
    //public Transform bulletSpawn;
    //public GameObject bulletPrefab;


    private Rigidbody2D _rigid;
    private bool isJump = false;
    private Animator animator;



    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //vidaActual = vidaTotal;
        coolDownMuerte = true;
    }


    void Update()
    {

        /*if (vidaActual <= 0 && coolDownMuerte)
        {            
            animator.SetBool("isDead", true);
            StartCoroutine(CoroutineMuerte());
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            animator.SetBool("isDead", false);
        }*/


        // Update is called once per frame
        //personajeDisparo();


        if (Input.GetKey(KeyCode.Space))
        {
            isJump = true;

        }
        else
        {
            isJump = false;
        }


    }

    /*
    public float vidaAct { get => vidaActual; set => vidaActual = value; }
    public float vidaTot { get => vidaTotal; set => vidaTotal = value; }
    */

    //PARA HACER QUE FUNCIONE EL BOTON DE SALTO
    public void saltarBoton()
    {
        isJump = true;

    }

    public void saltaSiSuelo()
    {
        if (isJump && _inGround)
        {
            _rigid.AddForce(new Vector2(0, _fuerzaSalto));
            // StartCoroutine(CoroutineSalto());
        }
    }

    private void FixedUpdate()
    {

        _inGround = comprobarSuelo3();/////VARIABLE PARA COMPROBAR SI TOCA SUELO
        animator.SetBool("isGround", _inGround);//VARIABLE PARA EL ANIMATOR
                                                //_rigid.velocity = new Vector2(Input.GetAxis("Horizontal") * _velocidad, Input.GetAxis("Vertical") * _velocidad); PARA HORIZONTAL Y VERTICAL


        //LINEA PARA MOVERLO CON JOYSTICK
        /*
        if (joystick.Horizontal > 0)
        {
            _rigid.velocity = new Vector2(joystick.Horizontal * _velocidad, _rigid.velocity.y);
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (joystick.Horizontal < 0)
        {
            _rigid.velocity = new Vector2(joystick.Horizontal * _velocidad, _rigid.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        animator.SetFloat("velx", Mathf.Abs(_rigid.velocity.x));
        saltaSiSuelo();
        */
        // LA LINEA PARA MOVERLO CON LAS TECLAS
        /*if (Input.GetAxis("Horizontal") >= 0)
        {
            _rigid.velocity = new Vector2(Input.GetAxis("Horizontal") * _velocidad, _rigid.velocity.y);
            //transform.localScale = new Vector3(1, 1, 1);
        }*/
        /*if (Input.GetAxis("Horizontal") <= 0)
        {
            _rigid.velocity = new Vector2(Input.GetAxis("Horizontal") * _velocidad, _rigid.velocity.y);
            //transform.localScale = new Vector3(-1, 1, 1);
        }*/
        _rigid.velocity = new Vector2(Input.GetAxis("Horizontal") * _velocidad, _rigid.velocity.y);
        if (_rigid.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(_rigid.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        animator.SetFloat("Speed", Mathf.Abs(_rigid.velocity.x));
        saltaSiSuelo();
    }

    /*private bool comprobarSuelo1()
    {
        return Physics2D.OverlapCircle(suelo.position, 0.04f, layerMask);
    }*/

    private bool comprobarSuelo2()
    {
        float tamanyoRayo = 0.5f;
        //SOLO DIBUJAR
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 0.2f), new Vector2(0, tamanyoRayo * -1), Color.cyan);
        return Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y + 0.2f), Vector2.down, tamanyoRayo, layerMask);
    }

    private bool comprobarSuelo3()
    {
        BoxCast(new Vector2(transform.position.x - 0.015f, transform.position.y + 0.01f),//ESTE ES SOLO PARA COMPROBAR LUEGO HAY QUE PASARLO AABAJO
            new Vector2(0.5f, 0.2f), 0f, new Vector2(0, 0), 0f, layerMask);

        return Physics2D.BoxCast(new Vector2(transform.position.x - 0.015f, transform.position.y + 0.01f),
            new Vector2(0.5f, 0.2f), 0f, new Vector2(0, 0), 0f, layerMask);//EL LAYER MASK HAY QUE PASARSELO AL ATRIBUTO PUBLIC LAYERMASK 
    }

    //SOLO DIBUJA
    static public RaycastHit2D BoxCast(Vector2 origen, Vector2 size, float angle, Vector2 direction, float distance, int mask)
    {
        RaycastHit2D hit = Physics2D.BoxCast(origen, size, angle, direction, distance, mask);

        //Setting up the points to draw the cast
        Vector2 p1, p2, p3, p4, p5, p6, p7, p8;
        float w = size.x * 0.5f;
        float h = size.y * 0.5f;
        p1 = new Vector2(-w, h);
        p2 = new Vector2(w, h);
        p3 = new Vector2(w, -h);
        p4 = new Vector2(-w, -h);

        Quaternion q = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
        p1 = q * p1;
        p2 = q * p2;
        p3 = q * p3;
        p4 = q * p4;

        p1 += origen;
        p2 += origen;
        p3 += origen;
        p4 += origen;

        Vector2 realDistance = direction.normalized * distance;
        p5 = p1 + realDistance;
        p6 = p2 + realDistance;
        p7 = p3 + realDistance;
        p8 = p4 + realDistance;


        //Drawing the cast
        Color castColor = hit ? Color.red : Color.green;
        Debug.DrawLine(p1, p2, castColor);
        Debug.DrawLine(p2, p3, castColor);
        Debug.DrawLine(p3, p4, castColor);
        Debug.DrawLine(p4, p1, castColor);

        Debug.DrawLine(p5, p6, castColor);
        Debug.DrawLine(p6, p7, castColor);
        Debug.DrawLine(p7, p8, castColor);
        Debug.DrawLine(p8, p5, castColor);

        Debug.DrawLine(p1, p5, Color.grey);
        Debug.DrawLine(p2, p6, Color.grey);
        Debug.DrawLine(p3, p7, Color.grey);
        Debug.DrawLine(p4, p8, Color.grey);
        if (hit)
        {
            Debug.DrawLine(hit.point, hit.point + hit.normal.normalized * 0.2f, Color.yellow);
        }

        return hit;
    }

    /*public void personajeDisparo()
    {
       
        if (Input.GetButtonDown("Fire1") && coolDownDisparo)
        {
            animator.SetBool("isShooting", true);
            Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            StartCoroutine(CoroutineDisparo());
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            animator.SetBool("isShooting", false);
        }
    }*/

    IEnumerator CoroutineSalto()
    {
        Debug.Log("Iniciada corrutina");
        coolDownSalto = false;
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Finalizada corrutina");
        coolDownSalto = true;
    }

    /*IEnumerator CoroutineDisparo()
    {
        Debug.Log("Iniciada corrutina");
        coolDownDisparo = false;
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Finalizada corrutina");
        coolDownDisparo = true;
    }

    IEnumerator CoroutineMuerte()
    {
        Debug.Log("Iniciada corrutina");
        coolDownMuerte = false;
        yield return new WaitForSeconds(3.0f);
        Debug.Log("Finalizada corrutina");
        coolDownMuerte = true;
    }*/



}
