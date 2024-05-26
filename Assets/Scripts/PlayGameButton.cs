using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGameButton : MonoBehaviour
{
    public void sahnegecis(int play_id)
    {
        SceneManager.LoadScene(play_id);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
