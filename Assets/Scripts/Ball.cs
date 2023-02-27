using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float currentSpeed;
    Vector2 reboundDirectionVector = Vector2.down;

    Rigidbody2D myRigidbody;
    Paddle myPaddle;

    void Awake() 
    {
        this.myRigidbody = GetComponent<Rigidbody2D>();
        this.myPaddle = GameObject.FindGameObjectsWithTag("Paddle")[0].GetComponent<Paddle>();
    }
    
    void Start()
    {
        this.LaunchBall();
    }

    void LaunchBall() 
    {
        myRigidbody.AddForce(reboundDirectionVector * currentSpeed, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.collider.tag == "Paddle")
        {
            float normalizedContactPoint = this.GetNormalizedContactPoint();
            this.UpdateReboundVector(normalizedContactPoint);

            myRigidbody.velocity = Vector2.zero;
            myRigidbody.AddForce(reboundDirectionVector * currentSpeed, ForceMode2D.Impulse);
        }
    }

    void UpdateReboundVector(float normalizedContactPoint)
    {
        float outgoingAngle = normalizedContactPoint * 60;
        Quaternion rotateBy = Quaternion.AngleAxis(-outgoingAngle, Vector3.forward);
        Vector2 newDirection = rotateBy * Vector2.up;
        reboundDirectionVector = newDirection;
    }

    float GetNormalizedContactPoint()
    {
        float ballXPos = transform.position.x;
        float paddleXPos = myPaddle.GetXPos();
        float paddleHalfWidth = myPaddle.GetWidth() / 2;

        float normalizedContactPoint = (ballXPos - paddleXPos) / paddleHalfWidth;
        return normalizedContactPoint;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Ground") 
        {
            Debug.Log("oops, u died");
        }
    }
}
