using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos;
    public float parallexEffect;

    public GameObject cam;
    public float paralaxMod = 1.0f;
    float prevPosX;

    void Start()
    {
        cam = Camera.main.gameObject;
        startpos = transform.position.x;
        prevPosX = cam.transform.position.x;
        if(TryGetComponent<SpriteRenderer>(out SpriteRenderer sr))
            length = sr.bounds.size.x;
    }

    void Update()
    {
        Vector3 pos = transform.position;
        float delta = cam.transform.position.x - prevPosX;
        pos.x += delta * -paralaxMod;
        transform.position = pos;
        prevPosX = cam.transform.position.x;
    }
}
