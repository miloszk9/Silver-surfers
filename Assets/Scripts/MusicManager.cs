using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager musicManagerInstance;
    void Awake()
    {
        DontDestroyOnLoad(this);

        if(musicManagerInstance == null){
            musicManagerInstance = this;
        }else{
            Destroy(gameObject);
        }
    }
}
