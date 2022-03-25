using UnityEngine;

public class Boss3WallOpener : MonoBehaviour
{
    public GameObject wall;

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Fighter").Length == 0)
            OpenWall();
    }
    private void OpenWall()
    {
        wall.SetActive(false);
    }
}
