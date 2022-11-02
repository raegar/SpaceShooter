using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{

    public delegate void OnGameOverAction();
    public OnGameOverAction OnGameOver;
    public float speed = 15.0f;

    public Camera MainCamera;
    public GameObject BulletPrefab;

    Vector3 leftBound;
    Vector3 rightBound;

    // Start is called before the first frame update
    void Start()
    {
        leftBound = MainCamera.ViewportToWorldPoint(new Vector3(0, 0, -MainCamera.transform.localPosition.z));
        rightBound = MainCamera.ViewportToWorldPoint(new Vector3(1, 0, -MainCamera.transform.localPosition.z));
    }

    // Update is called once per frame
    void Update()
    {
        processInput();
        keepInBounds();
    }

    private void keepInBounds()
    {
        if (this.transform.position.x < leftBound.x)
        {
            this.transform.position = new Vector3(leftBound.x, this.transform.position.y, this.transform.position.z);
        }

        if (this.transform.position.x > rightBound.x)
        {
            this.transform.position = new Vector3(rightBound.x, this.transform.position.y, this.transform.position.z);
        }
        /*
        if (this.transform.position.y < leftBound.y)
        {
            this.transform.position = new Vector3(this.transform.position.x, leftBound.y, this.transform.position.z);
        }

        if (this.transform.position.x > rightBound.y)
        {
            this.transform.position = new Vector3(this.transform.position.x, rightBound.y, this.transform.position.z);
        }*/
    }

    private void processInput()
    {
        /*
        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + speed * Time.deltaTime, this.transform.position.z);
        }

        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - speed * Time.deltaTime, this.transform.position.z);
        }
        */

        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            this.transform.position = new Vector3(this.transform.position.x - speed * Time.deltaTime, this.transform.position.y, this.transform.position.z);
        }

        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            this.transform.position = new Vector3(this.transform.position.x + speed * Time.deltaTime, this.transform.position.y, this.transform.position.z);
        }

        if (Input.GetKeyDown("up") || Input.GetKeyDown("space"))
        {
            GameObject bullet = GameObject.Instantiate<GameObject>(BulletPrefab);
            bullet.transform.SetParent(this.transform.parent);
            bullet.transform.position = this.transform.position;
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<EnemyController>() != null)
        {
            if (OnGameOver != null)
            {
                OnGameOver();
            }
        }
        GameObject.Destroy(this.gameObject);
    }
}
