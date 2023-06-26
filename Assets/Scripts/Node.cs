using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Turret turret;
    public bool empty = true;
    private Color baseColor;
    [SerializeField, NotNull] private Renderer renderer;

    private void Awake()
    {
        baseColor = renderer.material.color;
    }
    public void CreateTurret(Turret turret, int lvl)
    {
        empty = false;
        this.turret = Instantiate(turret, transform.position, Quaternion.identity);
        this.turret.turretLvl = lvl;
        this.turret.text.text = lvl.ToString();
        LevelManager.instance.turretsOnScene.Add(this.turret);
    }

    public void SetTurret(Turret turret)
    {
        SetColor(false);
        empty = false;
        this.turret = turret;
        LevelManager.instance.turretsOnScene.Add(this.turret);
    }

    public Turret CmdTakeAndNullTurret()
    {
        LevelManager.instance.turretsOnScene.Remove(this.turret);
        Turret turret = TakeTurrut();
        this.turret = null;
        return turret;
    }

    public Turret TakeTurrut()
    {
        empty = true;
        return turret;
    }

    public void UpgrageTurret(Turret turretToBuild, GameObject upgradeParticle)
    {
        SetColor(false);
        turret.turretLvl++;
        int tempLvl = turret.turretLvl;
        LevelManager.instance.turretsOnScene.Remove(turret);
        Destroy(turret.gameObject);
        turret = Instantiate(turretToBuild, transform.position, Quaternion.identity);
        turret.text.text = tempLvl.ToString();
        turret.turretLvl = tempLvl;

        var p = Instantiate(upgradeParticle, turret.transform.position, turret.transform.rotation);
        p.transform.SetParent(turret.transform);
        //p.transform.DOScale(1, );

        LevelManager.instance.turretsOnScene.Add(turret);
        empty = false;
    }


    public bool GetEmpty()
    {
        return empty;
    }

    public void SetColor(bool color)
    {
        if(color)
        {
            renderer.material.color = Color.gray;
        }
        else
        {
            renderer.material.color = baseColor;
        }
    }

    public void SetTexture(Texture texture)
    {
        renderer.material.SetTexture("_BaseMap", texture);
    }
}
