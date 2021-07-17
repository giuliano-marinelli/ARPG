using UnityEngine;

public class Footsteps : MonoBehaviour
{
    private AudioManager audioManager;
    private AudioSource audioSource;
    private TerrainDetector terrainDetector;

    private void Awake()
    {
        terrainDetector = new TerrainDetector();
        audioSource = GetComponent<AudioSource>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Step()
    {
        audioSource.PlayOneShot(GetSound("Footstep"));
    }

    private void Land()
    {
        audioSource.PlayOneShot(GetSound("Land"));
    }

    private AudioClip GetSound(string action)
    {
        int terrainTextureIndex = terrainDetector.GetActiveTerrainTextureIdx(transform.position);

        switch (terrainTextureIndex)
        {
            case 0:
            default:
                return audioManager.Get(action + "Grass");
            case 1:
                return audioManager.Get(action + "Mud");
            case 2:
                return audioManager.Get(action + "Rock");
        }
    }

}