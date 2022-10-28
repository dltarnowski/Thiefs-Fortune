using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : MonoBehaviour
{
    [SerializeField] MeshDeformer meshDeformer;
    [SerializeField] ParticleSystem[] geyserBlast;
    [SerializeField] AudioClip geyserAudio;
    [SerializeField] Vector3 point;

    AudioSource geyserAudioSource;

    [SerializeField] float one;
    [SerializeField] float two;
    [SerializeField] float three;
    [SerializeField] float four;

    float damage;

    // Start is called before the first frame update
    void Start()
    {
        meshDeformer = this.gameObject.transform.GetComponent<MeshDeformer>();
        geyserAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        meshDeformer.Deform(point, one, two, three, four, Vector3.down);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (ParticleSystem geyser in geyserBlast)
            {
                geyser.Play();
            }
            geyserAudioSource.PlayOneShot(geyserAudio);
            damage = gameManager.instance.playerScript.HPOrig / 5;
            gameManager.instance.playerScript.takeDamage((int)damage);
        }
    }
}
