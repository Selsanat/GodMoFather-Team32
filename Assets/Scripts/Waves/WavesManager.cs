using Unity.VisualScripting;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    [SerializeField] private Waves playerWave;
    [SerializeField] private Waves policeWave;
    [SerializeField,Min(0.1f)] float valueChange;
    [SerializeField,Min(1f)] float speed;
    [SerializeField] float offset;
    [SerializeField] float duration;
    float timer ;
 
    void Start()
    {
        timer = duration;
        RandomizePoliceWave();

        if (VerifyAmplitude())
        {
            playerWave.amplitude -= offset;
        }
    }

    // Update is called once per frame
    void Update()
    {
        policeWave.amplitude = Mathf.Clamp(policeWave.amplitude,0, 1.85f);
        playerWave.amplitude = Mathf.Clamp(playerWave.amplitude, 0, 1.85f);
        if(!VerifyAmplitude())
        {
            
             timer -= Time.deltaTime;
            if(timer <= 0)
            {
                RandomizePoliceWave();
                timer = duration;
            }


             if (Input.GetKey(KeyCode.A))
            {
                playerWave.amplitude -= valueChange *Time.deltaTime* speed;
                VerifyAmplitude();
            }
            if (Input.GetKey(KeyCode.E))
            {
                playerWave.amplitude += valueChange * Time.deltaTime*speed;
                VerifyAmplitude();
            }

            
        }

        
       

       
    }

    bool VerifyAmplitude()
    {
        if(playerWave.amplitude >=(policeWave.amplitude -offset) && playerWave.amplitude <= (policeWave.amplitude + offset))
        {
            CancelInvoke("RandomizePoliceWave");
            return true;
        }
        return false;
    }

    //void RandomizeWaves()
    //{
    //    policeWave.amplitude = Random.Range(0f, 1.85f);
    //    playerWave.amplitude = Random.Range(0f, 1.85f);
    //}

    void RandomizePoliceWave()
    {
        policeWave.amplitude = Random.Range(0f, 1.85f);
       
    }

}
