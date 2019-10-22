using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField]
    private float _speed = 4.0f;
    private Player _player;
    private Animator _deathAnim;
    private BoxCollider2D _enemyCollider;
    private AudioSource _audio;
    [SerializeField]
    private GameObject _laserPrefab;
    private float _fireRate = 3.0f;
    private float _canFire = -1;
    private bool _canLoop = true;

    void Start() {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _deathAnim = GetComponent<Animator>();
        _enemyCollider = GetComponent<BoxCollider2D>();
        _audio = GetComponent<AudioSource>();

        if (_player == null)
            Debug.LogError("Player is null");
        if (_deathAnim == null)
            Debug.LogError("Animator is null");
        if (_enemyCollider == null)
            Debug.LogError("Box collider is null");
        if (_audio == null)
            Debug.LogError("Audio source is null");
    }

    // Update is called once per frame
    void Update() {
        CalculateMovement();

        if (Time.time > _canFire) {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;

            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            for (int i = 0; i < lasers.Length; i++)
                lasers[i].AssignEnemyLaser();
        }
    }

    void CalculateMovement() {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5.83f && _canLoop)
            transform.position = new Vector3(Random.Range(-8f, 8f), 7.0f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
                player.Damage();

            _speed = 3.0f;
            _enemyCollider.enabled = false;
            _canLoop = false;
            _deathAnim.ResetTrigger("OnEnemyDeath");
            _deathAnim.SetTrigger("OnEnemyDeath");
            _audio.Play();
            Destroy(this.gameObject, 2.8f);
        } 
        else if (other.tag == "Laser") {
            Laser laser = other.transform.GetComponent<Laser>();
            if (!laser.GetEnemyLaser()) {
                if (_player != null)
                    _player.AddScore(10);
                Destroy(other.gameObject);

                _speed = 3.0f;
                _enemyCollider.enabled = false;
                _canLoop = false;
                _deathAnim.ResetTrigger("OnEnemyDeath");
                _deathAnim.SetTrigger("OnEnemyDeath");
                _audio.Play();
                Destroy(this.gameObject, 2.8f);
            }
        }
    }
}
