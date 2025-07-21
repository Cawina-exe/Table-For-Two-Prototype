using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsLoader : MonoBehaviour
{
    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits"); 
    }
}
