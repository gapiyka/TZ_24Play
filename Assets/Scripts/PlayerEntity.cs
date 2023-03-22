using System;
using System.Collections;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] [Range(0f, 10f)] private float _speed;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _animationDelay;
    [SerializeField] private Transform _player;

    private Vector3 _direction = new Vector3(0f, 0f, 1f);
    private bool _isJumping = false;

    public void Move(float x)
    {
        Vector3 velocity = _direction;
        velocity.x = x;
        velocity *= _speed * Time.deltaTime; // what about <<v * Time.deltaTime>>
        _player.position += velocity;
    }

    public void Jump()
    {
        if (!_isJumping)
        {
            SwitchJump(true);
            StartCoroutine(Fall());
        }
    }

    private void SwitchJump(bool state)
    {
        _isJumping = state;
        _animator.SetBool("IsJumping", state);
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(_animationDelay);
        SwitchJump(false);
    }

}

