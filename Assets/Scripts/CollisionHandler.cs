using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem crashVFX;
    [SerializeField] AudioClip explosion;
    new AudioSource audio;

    [SerializeField] GameObject EndMenu;
    [SerializeField] GameObject scoreObject;
    ScoreBoard scoreScript;
    [SerializeField] TMP_Text endScore;

    PlayerControls playerControls;
    

    void Start()
    {
        audio = GetComponent<AudioSource>();
        scoreScript = scoreObject.GetComponent<ScoreBoard>();
        playerControls = GetComponent<PlayerControls>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Finish")
        {
            EndingGame();
        }
        else
        {
            StartCrashSequence();
        }
    }

    void StartCrashSequence()
    {
        crashVFX.Play();

        if (!audio.isPlaying)
        {
            audio.PlayOneShot(explosion);
        }
        else
        {
            audio.Stop();
        }

        GetComponent<PlayerControls>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
    }

    void EndingGame()
    {
        string finalScore = scoreScript.score.ToString();
        scoreObject.SetActive(false);
        EndMenu.SetActive(true);
        endScore.text = $"You have a score of {finalScore}!";
        playerControls.enabled = false;
        Time.timeScale = 0f;
    }

    void ReloadLevel()
    {
        gameObject.SetActive(false);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
