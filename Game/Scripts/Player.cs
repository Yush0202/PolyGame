using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public GameObject m_ButtonRestart;
    public Transform m_PlayerModel;

    void Start () {
        m_ButtonRestart.SetActive(false);//Initially hide the Restart button
    }

    void OnDeath()
    {
        m_ButtonRestart.SetActive(true); //Show the Restart button when player dies
        m_PlayerModel.localRotation = Quaternion.Euler(90, 90, 0); //Lay player model down on its side to indicate death
    }
	
	public void RestartGame()
    {
        SceneManager.LoadScene(0); //Load the scene to restart the game
    }

}
