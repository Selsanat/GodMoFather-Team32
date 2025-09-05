using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class WavesManager : MonoBehaviour
{
    [SerializeField] public Waves playerWave;
    [SerializeField] public Waves policeWave;
    [SerializeField, Min(0.1f)] float valueChange;
    [SerializeField, Min(1f)] float speed;
    [SerializeField] float offset;
    [SerializeField] float duration;
    private Gamepad gamepad;
    float timer;
    bool areFaded = false;
    bool wasAmplitudeMatching = false; // Nouveau : pour suivre l'état précédent

    public static WavesManager ins;
    private void Awake()
    {
        if (ins == null)
        {
            ins = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        timer = duration;
        RandomizePoliceWave();

        // Initialiser l'état de correspondance d'amplitude
        wasAmplitudeMatching = VerifyAmplitude();

        if (wasAmplitudeMatching)
        {
            playerWave.amplitude -= offset;
            // Mettre à jour l'état après modification
            wasAmplitudeMatching = VerifyAmplitude();
        }

        gamepad = Gamepad.current;
    }

    void Update()
    {
        policeWave.amplitude = Mathf.Clamp(policeWave.amplitude, 0, 1.85f);
        playerWave.amplitude = Mathf.Clamp(playerWave.amplitude, 0, 1.85f);

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            RandomizePoliceWave();
            timer = duration;
        }

        // Vérifier l'état actuel de l'amplitude
        bool currentAmplitudeMatching = VerifyAmplitude();

        // Détecter les changements d'état et appeler la fonction
        if (currentAmplitudeMatching != wasAmplitudeMatching)
        {
            
            if (currentAmplitudeMatching)
            {
                PoliceManager.Instance.FadeAllPoliceCars(1);
            }
            else
            {
                PoliceManager.Instance.FadeAllPoliceCars(0);
            }
                Debug.Log($"Changement d'état détecté : {(currentAmplitudeMatching ? "Amplitudes correspondent" : "Amplitudes ne correspondent plus")}");
        }

        // Mettre à jour l'état précédent
        wasAmplitudeMatching = currentAmplitudeMatching;

        if (!currentAmplitudeMatching)
        {
            if (!areFaded)
            {
                areFaded = true;
            }

            if (gamepad != null)
            {
                // Lire les valeurs des gâchettes (0.0 à 1.0)
                float rightTrigger = gamepad.rightTrigger.ReadValue();
                float leftTrigger = gamepad.leftTrigger.ReadValue();

                // Détection avec seuil
                if (rightTrigger > 0.1f)
                {
                    playerWave.amplitude -= valueChange * Time.deltaTime * speed;
                }
                if (leftTrigger > 0.1f)
                {
                    playerWave.amplitude += valueChange * Time.deltaTime * speed;
                }
            }
        }
        else
        {
            if (areFaded)
            {
                areFaded = false;
            }
        }
    }

    bool VerifyAmplitude()
    {
        if (playerWave.amplitude >= (policeWave.amplitude - offset) && playerWave.amplitude <= (policeWave.amplitude + offset))
        {
            CancelInvoke("RandomizePoliceWave");
            return true;
        }
        return false;
    }

    void RandomizePoliceWave()
    {
        policeWave.amplitude = Random.Range(0f, 1.85f);
    }
}