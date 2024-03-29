using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    SpriteRenderer myRenderer;
    Vector2 rawMoveInput = Vector2.zero;

    Vector2 minBounds;
    Vector2 maxBounds;
    float myWidth;
    AudioSource audioSource;

    void Awake()
    {
        this.myRenderer = GetComponent<SpriteRenderer>();
        this.audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.tag == "Ball")
        {
            this.audioSource.Play(0);
        }
    }

    void Start()
    {
        this.minBounds = Camera.main.ViewportToWorldPoint(Vector2.zero);
        this.maxBounds = Camera.main.ViewportToWorldPoint(Vector2.one);
        this.myWidth = this.myRenderer.bounds.size.x;
    }

    void Update()
    {
        this.Move();
    }

    void OnMove(InputValue value)
    {
        this.rawMoveInput.x = value.Get<Vector2>().x;
    }

    void Move()
    {
        Vector2 delta = Time.deltaTime * Constants.moveSpeed * this.rawMoveInput;

        Vector2 newPos;
        newPos.x = Mathf.Clamp(transform.position.x + delta.x,
                            this.minBounds.x + (this.myWidth / 2),
                            this.maxBounds.x - (this.myWidth / 2));
        newPos.y = transform.position.y;

        transform.position = newPos;
    }


    public float GetXPos()
    {
        return transform.position.x;
    }

    public float GetWidth()
    {
        return this.myWidth;
    }

    public void ResetPosition()
    {
        Vector2 homePosition = new Vector2(0, -3.8f);
        transform.position = homePosition;
    }

}