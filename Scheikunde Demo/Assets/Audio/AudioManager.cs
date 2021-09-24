using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject glassDrop, metalDrop, fluidDrop, explosion;
    public AudioSource objective, quest;
    public static AudioManager instance;

    public void Awake()
    {
        instance = this;
    }

    public void PlaceSoundAt(SoundType sound, Vector3 position, float volume = 1)
    {
        GameObject s = EnumToObject(sound);

        var g = Instantiate(s, null, true);

        g.transform.SetParent(null);
        g.transform.position = position;
        var a = g.GetComponent<AudioSource>();
        a.volume = volume;

        Destroy(g, a.clip.length);
    }

    public float GetVolumeFromSpeed(float velocity, float maxVolume)
    {
        return 1;
    }

    public GameObject EnumToObject(SoundType enumType)
    {
        switch (enumType)
        {
            case SoundType.Glass:
                return glassDrop;
            case SoundType.Metal:
                return metalDrop;
            case SoundType.Fluid:
                return fluidDrop;
            case SoundType.Explosion:
                return explosion;
            default:
                break;
        }

        return null;
    }
}


[SerializeField]
public enum SoundType
{
    Glass,
    Metal,
    Fluid,
    Explosion
}
