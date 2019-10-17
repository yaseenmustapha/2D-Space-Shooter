﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
    [SerializeField]
    private float _speed = 8.0f;

    // Update is called once per frame
    void Update() {
        transform.Translate(new Vector3(0, 1) * _speed * Time.deltaTime);

        if (transform.position.y >= 8.0)
            Destroy(gameObject);
    }
}
