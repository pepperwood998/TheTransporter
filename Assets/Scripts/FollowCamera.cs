using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
  [SerializeField] GameObject Following;

  void LateUpdate()
  {
    this.transform.position = this.Following.transform.position + new Vector3(0, 0, -10);
  }
}
