/***
 * Author: Akram Taghavi-Burris
 * Created: 10-30-22
 * Modified:
 * Description: Controls weather effects
 ***/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Weather : MonoBehaviour
{
    public GameObject rainGO;
    ParticleSystem rainPS;
    public float rainTime = 10;
    public AudioMixerSnapshot Raining;
    public AudioMixerSnapshot Sunny;
    public Volume postProcess;

    float lerpValue;
    float lerpDuration = 10;
    float transtionTime;


    Timer rainTimer;

    AudioSource audioSrc;
    bool isRaining = false;
    public bool IsRaining { get { return isRaining; } }

    // Start is called before the first frame update
    void Start()
    {
        rainPS = rainGO.GetComponent<ParticleSystem>();
        audioSrc= rainGO.GetComponent<AudioSource>();
        rainTimer = new Timer(10f, false);
    }

    // Update is called once per frame
    void Update()
    {
        rainTimer.Update(Time.deltaTime);
        if (rainTimer.IsRunning())
        {
            if (!rainTimer.IsDone())
            {
                TintSky();

            } else {
                EndRain();
            } //end if (timeRemaining > 0)
        }//end if (startTimer)
    }//end Update()


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter Rain");

        if(other.tag == "Player")
        {
            //if the timer has not yet started
            if (!rainTimer.IsRunning())
            {
                rainTimer.RestartTimer();
                rainTimer.Start();
                isRaining = true;
                rainPS.Play();
                audioSrc.Play();
                Raining.TransitionTo(2.0f);
            }//end if (!startTimer)

        }//end if(other.tag == "Player")
    }//end OnTriggerEnter()

    void TintSky()
    {
        
        if (transtionTime < lerpDuration)
        {
            lerpValue = Mathf.Lerp(0, 1, transtionTime / lerpDuration);
            transtionTime += Time.deltaTime;
            postProcess.weight = lerpValue;

        }

    }//end TintSky()

    void EndRain()
    {
        rainTimer.Stop();
        isRaining = false;
        rainTimer.RestartTimer();
        rainPS.Stop();
        audioSrc.Stop();
        Sunny.TransitionTo(2.0f);

    }//end EndRain()




}
