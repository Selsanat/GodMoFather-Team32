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
    bool wasAmplitudeMatching = false; // Nouveau : pour suivre l'�tat pr�c�dent

    void Start()
    {
        timer = duration;
        RandomizePoliceWave();

        // Initialiser l'�tat de correspondance d'amplitude
        wasAmplitudeMatching = VerifyAmplitude();

        if (wasAmplitudeMatching)
        {
            playerWave.amplitude -= offset;
            // Mettre � jour l'�tat apr�s modification
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

        // V�rifier l'�tat actuel de l'amplitude
        bool currentAmplitudeMatching = VerifyAmplitude();

        // D�tecter les changements d'�tat et appeler la fonction
        if (currentAmplitudeMatching != wasAmplitudeMatching)
        {
            
            if (currentAmplitudeMatching)
            {
                AudioManager.instance.PlaySFX("Amplitude");
                PoliceManager.Instance.FadeAllPoliceCars(1);
            }
            else
            {
                PoliceManager.Instance.FadeAllPoliceCars(0);
            }
                Debug.Log($"Changement d'�tat d�tect� : {(currentAmplitudeMatching ? "Amplitudes correspondent" : "Amplitudes ne correspondent plus")}");
        }

        // Mettre � jour l'�tat pr�c�dent
        wasAmplitudeMatching = currentAmplitudeMatching;

        if (!currentAmplitudeMatching)
        {
            if (!areFaded)
            {
                areFaded = true;
            }

            if (gamepad != null)
            {
                // Lire les valeurs des g�chettes (0.0 � 1.0)
                float rightTrigger = gamepad.rightTrigger.ReadValue();
                float leftTrigger = gamepad.leftTrigger.ReadValue();

                // D�tection avec seuil
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