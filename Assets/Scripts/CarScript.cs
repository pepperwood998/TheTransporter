using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
  [SerializeField] float SteerSpeed = 230.0f;
  [SerializeField] float TopSpeed = 30.0f;
  [SerializeField] float MoveSpeed = 20.0f;
  [SerializeField] float NeutralSpeed = 10.0f;
  [SerializeField] float BoostTime = 3.0f;
  [SerializeField] float BumpTime = 2.0f;

  [SerializeField] float _speed;
  private Coroutine _restoreCoroutine;

  void Start()
  {
    this._speed = this.MoveSpeed;
  }

  void Update()
  {
    float moveAmount = Input.GetAxis("Vertical") * this._speed * Time.deltaTime;
    int steerMark = 1;
    if (moveAmount < 0)
    {
      steerMark = -1;
    }
    float steerAmount = -Input.GetAxis("Horizontal") * this.SteerSpeed * Time.deltaTime * steerMark;
    this.transform.Rotate(0, 0, steerAmount);
    this.transform.Translate(0, moveAmount, 0);
  }

  void OnCollisionEnter2D(Collision2D other)
  {
    this.StopRestoreSpeed();
    this._speed = this.NeutralSpeed;
  }

  void OnCollisionExit2D(Collision2D other)
  {
    this._restoreCoroutine = this.StartCoroutine(this.RestoreSpeed(this.BumpTime));
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Boost"))
    {
      this.StopRestoreSpeed();
    }
  }

  void OnTriggerStay2D(Collider2D other)
  {
    if (other.CompareTag("Boost"))
    {
      this._speed = this.TopSpeed;
    }
  }

  void OnTriggerExit2D(Collider2D other)
  {
    if (other.CompareTag("Boost"))
    {
      this._restoreCoroutine = this.StartCoroutine(this.RestoreSpeed(this.BoostTime));
    }
  }

  IEnumerator RestoreSpeed(float time)
  {
    yield return new WaitForSeconds(time);

    this._speed = this.MoveSpeed;
  }

  private void StopRestoreSpeed()
  {
    if (this._restoreCoroutine != null)
    {
      this.StopCoroutine(this._restoreCoroutine);
    }
  }
}
