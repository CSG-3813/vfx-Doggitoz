using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class WeatherSys : MonoBehaviour
{
    public GameObject rainGo;
    ParticleSystem rainPS;

    public float rainTime = 10;
    Timer rainTimer = new Timer(10f, false);

    public AudioMixerSnapshot raining;
    public AudioMixerSnapshot sunny;
    public Volume postProcess;

    float lerpValue;
    float lerpDuration = 10;
    float transtionTime;

    float timerTime;
    bool startTime;
    AudioSource audioSrc;

    bool isRaining;
    public bool IsRaining { get { return isRaining; } }


    // Start is called before the first frame update
    void Start()
    {
        rainPS = rainGo.GetComponent<ParticleSystem>();
        audioSrc = rainGo.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        rainTimer.Update(Time.deltaTime);
        if (startTime)
        {
            if (timerTime > 0)
            {
                timerTime -= Time.deltaTime;
                TintSky();
            }
            else
            {
                EndRain();
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter Rain");

        if (other.tag == "Player")
        {
            if (!startTime)
            {
                timerTime = rainTime;
                startTime = true;
                isRaining = true;
                rainPS.Play();
                audioSrc.Play();
                raining.TransitionTo(2f);
            }
        }
    }

    void TintSky()
    {

        if (transtionTime < lerpDuration)
        {
            lerpValue = Mathf.Lerp(0, 1, transtionTime / lerpDuration);
            Debug.Log(lerpValue);
            transtionTime += Time.deltaTime;
            postProcess.weight = lerpValue;

        }

    }//end TintSky()

    void EndRain()
    {
        startTime = false;
        isRaining = false;
        rainPS.Stop();
        audioSrc.Stop();
    }
}
