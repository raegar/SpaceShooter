using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    float speed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bulletLogic();
    }

    private void bulletLogic()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + speed * Time.deltaTime, this.transform.position.z);

        if (this.transform.position.y > 10)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
