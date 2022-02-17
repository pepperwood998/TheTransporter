using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
  [SerializeField] Color32 hasPackageColor = new Color32(30, 133, 255, 255);

  [SerializeField] Color32 noPackageColor = new Color32(255, 255, 255, 255);

  private bool hasPackage = false;

  private SpriteRenderer spriteRenderer;

  void Start()
  {
    this.spriteRenderer = this.GetComponent<SpriteRenderer>();
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Package") && !this.hasPackage)
    {
      this.hasPackage = true;
      Destroy(other.gameObject, 0.1f);
      this.spriteRenderer.color = this.hasPackageColor;
      Debug.Log("Picked Up");
    }
    else if (other.CompareTag("Customer") && this.hasPackage)
    {
      this.hasPackage = false;
      this.spriteRenderer.color = this.noPackageColor;
      Debug.Log("Delivered");
    }
  }
}
