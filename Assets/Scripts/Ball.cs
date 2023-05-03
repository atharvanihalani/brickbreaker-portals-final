using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float currentSpeed;
    [SerializeField] Vector2 homePosition;
    Vector2 reboundDirectionVector = Vector2.down;
    Rigidbody2D myRigidbody;
    ParticleSystem myParticleSystem;
    Scene myScene;
    Paddle myPaddle;
    TrailRenderer myTrail;



    void Awake() 
    {
        this.myRigidbody = GetComponent<Rigidbody2D>();
        this.myScene = GetComponentInParent<Scene>();
        this.myPaddle = GameObject.FindObjectOfType<Paddle>();
        this.myParticleSystem = GetComponentInChildren<ParticleSystem>();
        this.myTrail = GetComponentInChildren<TrailRenderer>();
    }
    
    void Start()
    {
        this.LaunchBall();
    }

    void LaunchBall() 
    {
        this.myRigidbody.AddForce(reboundDirectionVector * currentSpeed, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        this.myParticleSystem.Play();

        if (other.collider.tag == "Paddle")
        {
            float normalizedContactPoint = this.GetNormalizedContactPoint();
            this.UpdateReboundVector(normalizedContactPoint);

            this.myRigidbody.velocity = Vector2.zero;
            this.LaunchBall();
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
            this.StopTrail();
            this.myScene.HandleDeath();
        }
    }

    public void ResetPosition()
    {
        this.myRigidbody.velocity = Vector2.zero;
        this.reboundDirectionVector = Vector2.down;

        transform.position = this.homePosition;

        this.LaunchBall();
        this.ResumeTrail();
    }

    public void Teleport(Vector3 newPos)
    {
        transform.position = newPos;
    }

    public void StopTrail()
    {
        this.myTrail.time = 0;
    }

    public void ResumeTrail()
    {
        this.myTrail.time = 1;
    }
}
