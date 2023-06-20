using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Playermovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveH, moveV;
    private Playeranimation playeranimation;
    public float moveSpeed = 1.0f;
    public float sizeMultiplier = 2.0f;
    private Vector3 originalScale;
    public GameObject effectPrefab;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playeranimation = FindObjectOfType<Playeranimation>();
        originalScale = transform.localScale;
    }
    
    void Update()
    {
        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject e = Instantiate(effectPrefab);
            position.z = 0f;
            e.transform.position = position;
            Destroy(e, 1f);
        }
    }

    private void FixedUpdate()
    {
        Vector2 currentPos = rb.position;
        Vector2 inputVector = new Vector2(moveH, moveV).normalized * moveSpeed * Time.fixedDeltaTime;
        
        rb.MovePosition(currentPos + inputVector);

        playeranimation.SetDirection(inputVector);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("box"))
        {
            StartCoroutine(ApplySpeedBoost());
            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("box1"))
        {
           transform.localScale *= sizeMultiplier;
           StartCoroutine(ResetSize());
            collision.gameObject.SetActive(false);

        }

    }

    private IEnumerator ApplySpeedBoost()
    {
        moveSpeed *= 3f;
        yield return new WaitForSeconds(5f);
        moveSpeed /= 3f;
    }
    private IEnumerator ResetSize()
    {
        yield return new WaitForSeconds(10f);
        transform.localScale = originalScale;
    }
    
}
