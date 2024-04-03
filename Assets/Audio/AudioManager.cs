using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
// public:
    public static AudioManager instance;

// Serialazable:
    [SerializeField] Transform audioPoolParent;
    [SerializeField] Transform activeAudioPoolParent;
    [SerializeField] GameObject audioObject;
    [SerializeField] int poolSize;

// private:
    private List<GameObject> audioPool = new List<GameObject>();
    private List<GameObject> activeAudioPool = new List<GameObject>();

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
           instance = this;
        }
    }
    void Start()
    {
        audioPool.Clear();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject go = Instantiate(audioObject);
            audioPool.Add(go);
            go.transform.parent = audioPoolParent;
        }
    }

    public void PlayAudio(AudioClip clip, Vector3 position)
    {
        if(audioPool.Count > 0)
        {
            audioPool[0].transform.parent = activeAudioPoolParent;
            audioPool[0].transform.position = position;
            AudioSource source = audioPool[0].GetComponent<AudioSource>();
            source.PlayOneShot(clip);
            activeAudioPool.Add(audioPool[0]);
            audioPool.RemoveAt(0);

            // Wait for end of clip
            StartCoroutine(WaitForEndOfAudio(source));
            Debug.Log("End of clip");
        }
        else
        {
            Debug.Log("No Audio pool slot avalible");
        }

         
    }

    private bool IsReversePitch(AudioSource source)
    {
        return source.pitch < 0f;
    }

    private float GetClipRemainingTime(AudioSource source)
    {
        // Calculate how much time there is remaining for the audiosource
        // just source.clip.length doesnt work properly if we have diffrent pitches since it will shorten or lengthen
        float remainingTime = (source.clip.length - source.time) / source.pitch;
        return IsReversePitch(source) ? (source.clip.length + remainingTime) : remainingTime;
    }

    private IEnumerator WaitForEndOfAudio(AudioSource source)
    {
        var watForClipRemainingTime = new WaitForSeconds(GetClipRemainingTime(source));
        yield return watForClipRemainingTime;
    }
}
