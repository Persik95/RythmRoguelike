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
    [SerializeField] private bool Flipped;
    [SerializeField] private bool KD;

    [SerializeField] private Vector3 vectorForFlip;

    [SerializeField] private GameObject bulletPrefab;
    void Start()
    {
        StartCoroutine("KDTimer");
    }
    IEnumerator KDTimer()
    {
        yield return new WaitForSecondsRealtime(2);
        KD = true;

    }
    void FixedUpdate()
    {
        Atack();
        CheckGround();
        PlayerMovement();
        transform.localScale = vectorForFlip;
    }

    /// <summary>
    /// ����� ��� ������������ ������
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
            Flipped = false;
        }
        else if (moveX < 0)
        {
            vectorForFlip.x = -Mathf.Abs(vectorForFlip.x);
            Flipped = true;
        }
    }
    /// <summary>
    /// ����� ��� �������� ����, �� ����� �� �����
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
    /// ����� ��� �����
    /// </summary>
    public void Atack()
    {
        if (Input.GetKeyDown(KeyCode.E))
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
