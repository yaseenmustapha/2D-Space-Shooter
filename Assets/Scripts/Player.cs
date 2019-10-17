﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float nextFire = 0.0f;

    // Start is called before the first frame update
    void Start() {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update() {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
            FireLaser();

    }

    void CalculateMovement() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontalInput, verticalInput) * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.9f, 0));

        if (transform.position.x > 11.2f)
            transform.position = new Vector3(-11.2f, transform.position.y);
        else if (transform.position.x < -11.2f)
            transform.position = new Vector3(11.2f, transform.position.y);
    }

    void FireLaser() {
        nextFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f), Quaternion.identity);
    }
}
