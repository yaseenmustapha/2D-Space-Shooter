using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _boostSpeed = 10.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float nextFire = 0.0f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    private bool _isTripleShotActive = false;
    private bool _isShieldActive = false;
    [SerializeField]
    private int _score = 0;
    private UIManager _uiManager;
    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private AudioClip _laserClip;
    private AudioSource _audio;

    // Start is called before the first frame update
    void Start() {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audio = GetComponent<AudioSource>();
        if (_spawnManager == null)
            Debug.LogError("Spawn Manager is null");
        if (_uiManager == null)
            Debug.LogError("UI Manager is null");
        if (_audio == null)
            Debug.LogError("Audio source is null");
        
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
        
        if (_isTripleShotActive) {
            Instantiate(_tripleShotPrefab, transform.position + new Vector3(-0.371f, 0.111f), Quaternion.identity);
            _audio.clip = _laserClip;
            _audio.pitch = 2;
            _audio.Play();
        }
        else {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f), Quaternion.identity);
            _audio.clip = _laserClip;
            _audio.pitch = 1;
            _audio.Play();
        }
        
    }

    public void Damage() {
        if (_isShieldActive) {
            _shield.SetActive(false);
            _isShieldActive = false;
            return;
        }

        _lives--;

        if (_lives == 2)
            _rightEngine.SetActive(true);
        else if (_lives == 1)
            _leftEngine.SetActive(true);

        _uiManager.UpdateLives(_lives);

        if (_lives < 1) {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
            GameObject explosion = Instantiate(_explosionPrefab, this.transform.position, Quaternion.identity);
            Destroy(explosion, 3.0f);
        }
    }

    public void TripleShotActive() {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine() {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedActive() {
        StartCoroutine(SpeedPowerDownRoutine());
    }

    IEnumerator SpeedPowerDownRoutine() {
        float oldSpeed = _speed;
        _speed = _boostSpeed;
        yield return new WaitForSeconds(5.0f);
        _speed = oldSpeed;
    }

    public void ShieldActive() {
        _shield.SetActive(true);
        _isShieldActive = true;
    }

    public void AddScore(int points) {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
