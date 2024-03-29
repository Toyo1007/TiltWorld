using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Playersize : MonoBehaviour
{
    [SerializeField] private bool HitSnow;
    [SerializeField] private bool HitFloor;
    [SerializeField] private float GrowingSpeed;//大きくなる速さ
    [SerializeField] private float SmallerSpeed;//小さくなる速さ
    [SerializeField] private float SmallerSpeedPuddle;//水たまりで小さくなる速さ
    [SerializeField] private float SmallerSpeedAir;//空中の小さくなる速さ

     public bool dedflg;//死んだとき true
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        HitSnow = false;
        HitFloor = false;
        dedflg = false;
        rb = gameObject.GetComponent<Rigidbody>();
        GrowingSpeed = 0.02f;
        SmallerSpeed = 0.015f;
        SmallerSpeedPuddle = 0.03f;
        SmallerSpeedAir = 0.005f;

    }

    void FixedUpdate()
    {
        if (!HitFloor)
        {
            if (!HitSnow && System.Math.Abs(rb.velocity.magnitude) >= 10f)//移動速度
            {
                if (transform.localScale.magnitude >= 1f)//下限
                {
                    float size = SmallerSpeedAir * System.Math.Abs(rb.velocity.magnitude) / 30f;
                    transform.localScale -= new Vector3(size, size, size);
                }
                else
                {
                    dedflg = true;
                }
            }
        }
        HitFloor = false;
        HitSnow = false;

    }

    private void OnCollisionStay(Collision collision)
    {
        if (!HitSnow && System.Math.Abs(rb.velocity.magnitude) >= 10f)//移動速度
        {

            if (transform.localScale.magnitude >= 10f)//下限
            {
                float size = SmallerSpeed * System.Math.Abs(rb.velocity.magnitude) / 30f;
                transform.localScale -= new Vector3(size, size, size);

            }
            else
            {
                dedflg = true;
            }
        }
        else
        {
        }
        HitSnow = false;
        HitFloor = true;

        //soundSeting

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("snow") && rb.velocity.magnitude != 0f)
        {
            if (transform.localScale.magnitude <= 500)//大きさの上限
            {
                float size = GrowingSpeed * System.Math.Abs(rb.velocity.magnitude) / 30f;
                transform.localScale += new Vector3(size, size, size) ;
                transform.position += other.transform.forward * size;
            }
            HitSnow = true;
        }
        if (other.gameObject.CompareTag("puddle") && rb.velocity.magnitude != 0f)
        {
            if (transform.localScale.magnitude >= 10f)//下限
            {
                float size = SmallerSpeed * System.Math.Abs(rb.velocity.magnitude) / 30f;
                transform.localScale -= new Vector3(size * 1.5f, size * 1.5f, size * 1.5f);
            }
            else
            {
                dedflg = true;
            }
        }

    }
}
