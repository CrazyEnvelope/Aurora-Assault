using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up and down based upon player input.")]
    [SerializeField] float controlSpeed = 20f;
    [SerializeField] float xRange = 7f;
    [SerializeField] float yRange = 5f;

    [Header("Laser gun array")]
    [SerializeField] GameObject[] lasers;

    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -10f;

    [Header("Player input based tuning")]
    [SerializeField] float positionYawFactor = -2f;
    [SerializeField] float controlRollFactor = -10f;

    [SerializeField] AudioClip laserShoot;

    [SerializeField] GameObject PauseMenu;

    new AudioSource audio;

    float xThrow;
    float yThrow;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        Time.timeScale = 1f;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu.SetActive(!PauseMenu.activeSelf);
            
        }

        if (PauseMenu.activeSelf) 
        { 
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch =  pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3
            (clampedXPos,
            clampedYPos,
            transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            SetLaserActive(true);
            audio.PlayOneShot(laserShoot);
                

        }
        else if(Input.GetButtonUp("Fire1"))
        {
            SetLaserActive(false);
            audio.Stop();
        }
    }

    void SetLaserActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(isActive);
        }
    }

}
