using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

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
    public List<GameObject> audioPool = new();
    public List<GameObject> activeAudioPool = new();

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

            // Wait for end of clip
            //Debug.Log(clip.length);

            StartCoroutine(WaitForEndOfAudio(clip, activeAudioPool[activeAudioPool.Count - 1]));
            audioPool.Remove(audioPool[0]);
        }
        else
        {
            Debug.Log("No Audio pool slot avalible");
        }
    }
    private void GiveBackAudioPool(GameObject audioGameObject)
    {
        Debug.Log("called");
        audioGameObject.transform.position = Vector3.zero;
        audioGameObject.transform.parent = audioPoolParent;
        activeAudioPool.Remove(audioGameObject);
        audioPool.Add(audioGameObject);
    }

    private IEnumerator WaitForEndOfAudio(AudioClip clip, GameObject audioGameObject)
    {
        //yield return new WaitUntil(() => !audioGameObject.GetComponent<AudioSource>().isPlaying);
        yield return new WaitForSeconds(clip.length);
        GiveBackAudioPool(audioGameObject);
    }
}
