using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float currentSpeed;
    Vector2 reboundDirectionVector = Vector2.down;

    Rigidbody2D myRigidbody;
    Paddle myPaddle;
    [SerializeField] GameController myController;

    void Awake() 
    {
        this.myRigidbody = GetComponent<Rigidbody2D>();
        this.myPaddle = GameObject.FindObjectOfType<Paddle>();
    }
    
    void Start()
    {
        this.myController.UpdateReferences();
        this.LaunchBall();
    }

    void LaunchBall() 
    {
        this.myRigidbody.AddForce(reboundDirectionVector * currentSpeed, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.collider.tag == "Paddle")
        {
            float normalizedContactPoint = this.GetNormalizedContactPoint();
            this.UpdateReboundVector(normalizedContactPoint);

            this.myRigidbody.velocity = Vector2.zero;
            this.LaunchBall();
            // this.myRigidbody.AddForce(reboundDirectionVector * currentSpeed, ForceMode2D.Impulse);
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
            this.myController.HandleDeath();
        }
    }

    public void ResetPosition()
    {
        this.myRigidbody.velocity = Vector2.zero;
        this.reboundDirectionVector = Vector2.down;

        Vector2 homePosition = new Vector2(0, 4);
        transform.position = homePosition;

        this.LaunchBall();
    }
}
