using System;
using System.Collections;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    [SerializeField] [Range(0f, 10f)] private float _speed;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _animationDelay;
    [SerializeField] private Transform _playerBody;

    private const float leftBorder = -2f;
    private const float rightBorder = 2f;

    private Vector3 _direction = new Vector3(0f, 0f, 1f);
    private Vector3 _jumpVector = new Vector3(0f, 1.5f, 0f);
    private bool _isJumping = false;

    public float PositionZ => transform.position.z;

    public void Move(float x)
    {
        Vector3 velocity = _direction;
        velocity.x = x;
        velocity *= _speed * Time.deltaTime;
        transform.position += velocity;
        float posX = Math.Clamp(transform.position.x, leftBorder, rightBorder);
        transform.position = new(posX, transform.position.y, transform.position.z);
    }

    public void Jump()
    {
        if (!_isJumping)
        {
            _playerBody.position += _jumpVector;
            SwitchJump(true);
            StartCoroutine(Fall());
        }
    }

    private void SwitchJump(bool state)
    {
        _isJumping = state;
        _animator.SetBool("Jump", state);
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(_animationDelay);
        SwitchJump(false);
    }
}

