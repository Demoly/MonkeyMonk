using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speedMove = 3f;
    private float speedMoveInitial;
    // public int collisionDistance = 1;
    
    // public float mouseAngle = 0f;
    // public float mouseAngleX = 0f;
    // public float mouseAngleY = 0f;

    float horizontalMove = 0f;
    float verticalMove = 0f;
    bool isFacingRight = true;

    public Animator anim;
    private Rigidbody2D body;
    private Camera cam;

    // Start is called before the first frame update
    private void Start()
    {
        // Atribuindo valor das variaves privadas
        body = GetComponent<Rigidbody2D>();
        if (body==null)
        {
            Debug.LogError("errorodoo body null");
        }

        cam = Camera.main;
        speedMoveInitial = speedMove;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        // if inline opcao mais resumida, mas dificil de ler anim.SetBool("Horizontal", horizontalMove != 0 ? true : false);
        // Controle de animação
        if(horizontalMove != 0)
        {
            anim.SetBool("Horizontal", true);
        } else
        {
            anim.SetBool("Horizontal", false);
        }

        if (verticalMove != 0)
        {
            if (verticalMove < 0)
            {
                anim.SetBool("Vertical", true);
                anim.SetBool("Back", false);
            } else
            {
                anim.SetBool("Back", true);
                anim.SetBool("Vertical", false);
            }
        }
        else
        {
            anim.SetBool("Vertical", false);
            anim.SetBool("Back", false);
        }

        if (horizontalMove != 0 || verticalMove != 0)
        {
            //RaycastHit2D collisionTest = Physics2D.Raycast(transform.position, new Vector2(horizontalMove, verticalMove),
            //    speedMove * Time.deltaTime * collisionDistance);

            //Debug.Log("!collisionTest " + !collisionTest);

            //Move if no collision found
            //if (collisionTest)
            //{
                body.MovePosition(new Vector2((transform.position.x + horizontalMove * speedMove * Time.deltaTime),
                    (transform.position.y + verticalMove * speedMove * Time.deltaTime)));
                
                // O player está indo para esquerda e olhando para direita
                if (horizontalMove > 0 && isFacingRight)
                {
                    Flip();
                }
                // O player está indo para direita e olhando para esquerda
                else if (horizontalMove < 0 && !isFacingRight)
                {
                    Flip();
                }
            //}
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
