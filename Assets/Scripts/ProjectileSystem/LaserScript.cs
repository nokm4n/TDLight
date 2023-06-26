using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectileSystem
{
    public class LaserScript : ProjectileBase
    {
        private bool change = false;

        [SerializeField] public LineRenderer lineRenderer;

        [SerializeField, NotNull] Texture[] textures;
        float delay = 0.3f;
        //public float width = .2f;

        private bool songPlay = true;
        private bool isPlaying = false;

        private float fpsCounter = 0;
        private float fps = 20;
        private int animStep = 0;

        void Update()
        {
            if (CheckForNoTarget())
            {
                isPlaying = true;
                songPlay = false;
                lineRenderer.enabled = false;
                AudioController.instance.StopSound("laser");
                return;
            }
            else
            {
                songPlay = true;
            }

            lineRenderer.enabled = true;

            if (textures != null)
            {
                fpsCounter += Time.deltaTime;
                if (fpsCounter >= 1f / fps)
                {
                    animStep++;
                    if (animStep >= textures.Length)
                    {
                        animStep = 0;
                    }

                    lineRenderer.material.SetTexture("_MainTex", textures[animStep]);

                    fpsCounter = 0f;
                }
            }
            //HitTargetOnBase(type.damage * Time.deltaTime);
            if (songPlay && !isPlaying)
            {
                isPlaying = true;
                AudioController.instance.PlaySound("laser");
            }
            HitTargetOnBase(damage * Time.deltaTime * 1.5f);
            if (delay < 0)
            {
                HitEffects();
                delay = .5f;
            }
            delay -= Time.deltaTime;

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, _targetCenter.position);

            //ChangeWidth();
            //Vector3 dir = transform.position - _target.position;
        }

        void ChangeWidth()
        {
            AnimationCurve curve = new AnimationCurve();
            for (int i = 0; i < 21; i++)
            {
                curve.AddKey(i/20, Mathf.Sin((Mathf.PI / 180) * i));
                //curve.AddKey(1.0f, 1.0f);
            }
            //lineRenderer.widthCurve = curve;
          //  lineRenderer.widthMultiplier = width;
        }
    }
}
