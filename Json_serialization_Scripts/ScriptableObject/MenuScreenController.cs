using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName ="ScriptableObject/ButtonFunction/MenuScreenController")]
public class MenuScreenController : ScriptableObject
{
	public void StartGame()
	{
		SceneManager.LoadScene("Game");
	}

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuScreen");
    }
}