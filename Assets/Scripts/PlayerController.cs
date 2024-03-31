using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    [SerializeField] private Rigidbody2D rigidbodyPlayer;

    [SerializeField] private Transform startPosLine;
    [SerializeField] private Transform endPosLine;

    [SerializeField] private LayerMask layerMask;

    [SerializeField] private bool isGround;

    [SerializeField] private Vector3 vectorForFlip;

    [SerializeField] private GameObject bulletPrefab;
    void Start()
    {

    }
    void FixedUpdate()
    {
        Atack();
        CheckGround();
        PlayerMovement();
        transform.localScale = vectorForFlip;
    }

    /// <summary>
    /// Метод для передвижения игрока
    /// </summary>
    public void PlayerMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        
        rigidbodyPlayer.velocity = new Vector2 (moveX * speed, rigidbodyPlayer.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(isGround == true)
            {
                rigidbodyPlayer.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            }
        }
        
        if(moveX > 0)
        {
            vectorForFlip.x = Mathf.Abs(vectorForFlip.x);
        }
        else if (moveX < 0)
        {
            vectorForFlip.x = -Mathf.Abs(vectorForFlip.x);
        }
    }
    /// <summary>
    /// Метод для проверки того, на земле ли игрок
    /// </summary>
    public void CheckGround()
    {
        RaycastHit2D hit= Physics2D.Linecast(startPosLine.position, endPosLine.position, layerMask);
        if(hit.collider)
        {
            if(hit.collider.gameObject.tag == "Ground")
            {
                isGround = true;
            }
        }
        if(hit.collider == null)
        {
            isGround = false;
        }
        
    }
    /// <summary>
    /// Метод для атаки
    /// </summary>
    public void Atack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            mousePos.z = 0;
            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

            GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            newBullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
