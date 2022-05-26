using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shell : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField]
    private float _minForce = 90f;
    [SerializeField]
    private float _maxForce = 120f;

    float lifeTime = 4;
    float fadeTime = 2;

    private Renderer _renderer;
    //private MaterialPropertyBlock _propBlock;
    
    public void Start ()
    {
        gameObject.SetActive(true);
        _rb = GetComponent<Rigidbody>();
        
        _renderer = GetComponent<Renderer>();
        //_propBlock = new MaterialPropertyBlock();

        float force = Random.Range(_minForce, _maxForce);
        _rb.AddForce(transform.right * force);
        _rb.AddTorque(Random.insideUnitSphere * force);

        StartCoroutine(Fade());
    }


    IEnumerator Fade()
    {
        yield return new WaitForSeconds(lifeTime);

        // _renderer.GetPropertyBlock(_propBlock);
        // Color initialColor = _propBlock.GetColor("_BaseColor");
        // initialColor.a = 1; //Not sure why I need this

        Material mat = _renderer.material;
        Color initialColor = mat.color;
        
        float percent = 0;
        float fadeSpeed = 1 / fadeTime;

        while(percent < 1)
        {
            percent += Time.deltaTime * fadeSpeed;

            Color c = Color.Lerp(initialColor, Color.clear, percent);
            mat.color = c;
            //_propBlock.SetColor("_BaseColor", c);
            //_renderer.SetPropertyBlock(_propBlock);
            
            yield return null;
        }

        Destroy(gameObject);
        //gameObject.SetActive(false);
        
    }
}
