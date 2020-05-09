using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMove : MonoBehaviour
{
    //public Transform pointA, pointB;
    //private Vector3 currentTarget;
    //private Animator animator;
    public GameObject startPoint, endPoint;
    public float enemySpeed = 2;
    private bool isGoingRight;



    //CODIGO MANUEL PARA ATACAR CUANDO SE ACERQUE
    /*public float distance;
    public float distanciaAtaque = 3.5f;
    private GameObject jugador;*/

    // Start is called before the first frame update
    void Start()
    {
        //jugador = GameObject.FindGameObjectWithTag("Player");



        if (isGoingRight)
        {
            transform.position = startPoint.transform.position;
        }
        else
        {
            transform.position = endPoint.transform.position;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //CALCULAR DISTANCIAS ENTRE DOS OBJETOS
        //Debug.Log(Vector2.Distance(jugador.transform.localPosition, transform.localPosition));

        if (!isGoingRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint.transform.position, enemySpeed * Time.deltaTime);
            if (transform.position == endPoint.transform.position)
            {
                isGoingRight = true;
                GetComponent<SpriteRenderer>().flipX = false;
                //PARA QUE NO GIRE EL SPRITE SI NO TODO LA LINEA DE ABAJO PERO NO SE PUEDE CON VECTOR 3 ASI QUE BUSCATE LA VIDA
                //transform.localPosition = Quaternion.Euler(0, 0, 0);
            }

        }

        if (isGoingRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPoint.transform.position, enemySpeed * Time.deltaTime);
            if (transform.position == startPoint.transform.position)
            {
                isGoingRight = false;
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }



    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


    }
    
}
