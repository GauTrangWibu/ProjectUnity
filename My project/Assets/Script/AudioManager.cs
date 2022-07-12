using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioManager instance;
    private void Awake()
    {
        instance = this;
    }
    //SFX
    public List<AudioClip> sfxLibrary;
    //Music
    public AudioClip music_ingame;
    //sound object
    public GameObject soundObject;
    //Current music obj
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySFX(int sfxindex)
    {
        switch (sfxindex)
        {
            case 0:
                SoundObjectCreation(sfxLibrary[0]);
                break;
            case 1:
                SoundObjectCreation(sfxLibrary[1]);
                break;
            default:

                break;
        }
    }

    private void SoundObjectCreation(AudioClip clip)
    {
        GameObject newObject = Instantiate(soundObject, transform);
        newObject.GetComponent<AudioSource>().clip = clip;
        newObject.GetComponent<AudioSource>().Play();
    }

    private void SoundMusicCreation(AudioClip clip)
    {
        //create soundobj
        GameObject newObject = Instantiate(soundObject, transform);
        //assign audioclip to its audiosource
        newObject.GetComponent<AudioSource>().clip = clip;
        newObject.GetComponent<AudioSource>().loop = true;
        newObject.GetComponent<AudioSource>().Play();
    }

    public void playMusic()
    {
        SoundMusicCreation(music_ingame);
    }
}
