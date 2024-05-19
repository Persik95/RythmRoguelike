using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    [SerializeField] private Rigidbody2D PlrRB;

    [SerializeField] private LayerMask groundLayerMask;

    [SerializeField] private bool Flipped;
    [SerializeField] private bool KD;

    [SerializeField] private Vector3 vectorForFlip;
    [SerializeField] private Vector2 groundPointSize;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject groundPoint;

    private Animator Animator;
    void Start()
    {
        StartCoroutine("KDTimer");
        Animator = GetComponent<Animator>();
        PlrRB = GetComponent<Rigidbody2D>();
    }
    IEnumerator KDTimer()
    {
        yield return new WaitForSeconds(0.5f);
        KD = true;

    }
    void FixedUpdate()
    {
        Attack();
        PlayerMovement();
        transform.localScale = vectorForFlip;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundPoint.transform.position, groundPointSize);
    }
    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(groundPoint.transform.position, groundPointSize, 0, Vector2.zero, 0, groundLayerMask);
        return hit.collider != null;
    }
    /// <summary>
    /// Метод для передвижения игрока
    /// </summary>
    public void PlayerMovement()
    {
        float moveX = Input.GetAxis("Horizontal");

        Animator.SetFloat("Speed", Mathf.Abs(moveX * speed));

        PlrRB.velocity = new Vector2 (moveX * speed, PlrRB.velocity.y);

        print(IsGrounded());
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {

            
            PlrRB.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            string currentAnimState = Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
            if (Input.GetKeyDown(KeyCode.Space) && currentAnimState != "WizardJump")
            {
                Animator.SetTrigger("Jump");
            }
        }

        if (moveX > 0)
        {
            vectorForFlip.x = Mathf.Abs(vectorForFlip.x);
            Flipped = false;
        }
        else if (moveX < 0)
        {
            vectorForFlip.x = -Mathf.Abs(vectorForFlip.x);
            Flipped = true;
        }
    }
    /// <summary>
    /// Метод для проверки того, на земле ли игрок
    /// </summary>
    
    /// <summary>
    /// Метод для атаки
    /// </summary>
    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            float moveX = Input.GetAxis("Horizontal");
            float angle = 0;
            if(Flipped != true)
            {
                angle = 0;
            }
            else if (Flipped == true)
            {
                angle = 180;
            }
            if (KD == true)
            {
                GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                newBullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                KD = false;
                StartCoroutine("KDTimer");
            }
        }
    }
    
}
