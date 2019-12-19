﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ExplosionController : NetworkBehaviour
{
    #region Public
    public enum Orientation { center, top, bottom, left, right, vertical, horizontal };
    public Orientation orientation = Orientation.center;
    #endregion

    #region Private
    private DynamicGridManager dynamicGridManager;
    private Animator animator;
    #endregion

    void Awake()
    {
        dynamicGridManager = GameObject.Find("DynamicGridManager").GetComponent<DynamicGridManager>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            if (isServer)
            {
                Die();
            }

            Kill();
        }
    }

    public void SetOrientation(Orientation orientation)
    {
        switch (orientation)
        {
            case Orientation.top:
                {
                    animator.SetBool("top", true);
                    animator.SetBool("bottom", false);
                    animator.SetBool("left", false);
                    animator.SetBool("right", false);
                    animator.SetBool("vertical", false);
                    animator.SetBool("horizontal", false);
                    break;
                }
            case Orientation.bottom:
                {
                    animator.SetBool("top", false);
                    animator.SetBool("bottom", true);
                    animator.SetBool("left", false);
                    animator.SetBool("right", false);
                    animator.SetBool("vertical", false);
                    animator.SetBool("horizontal", false);
                    break;
                }
            case Orientation.left:
                {
                    animator.SetBool("top", false);
                    animator.SetBool("bottom", false);
                    animator.SetBool("left", true);
                    animator.SetBool("right", false);
                    animator.SetBool("vertical", false);
                    animator.SetBool("horizontal", false);
                    break;
                }
            case Orientation.right:
                {
                    animator.SetBool("top", false);
                    animator.SetBool("bottom", false);
                    animator.SetBool("left", false);
                    animator.SetBool("right", true);
                    animator.SetBool("vertical", false);
                    animator.SetBool("horizontal", false);
                    break;
                }
            case Orientation.vertical:
                {
                    animator.SetBool("top", false);
                    animator.SetBool("bottom", false);
                    animator.SetBool("left", false);
                    animator.SetBool("right", false);
                    animator.SetBool("vertical", true);
                    animator.SetBool("horizontal", false);
                    break;
                }
            case Orientation.horizontal:
                {
                    animator.SetBool("top", false);
                    animator.SetBool("bottom", false);
                    animator.SetBool("left", false);
                    animator.SetBool("right", false);
                    animator.SetBool("vertical", false);
                    animator.SetBool("horizontal", true);
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    public void Die()
    {
        List<GameObject> playersOnTop = dynamicGridManager.GetPlayersOnTile(transform.position);
        foreach (GameObject playerOnTop in playersOnTop)
        {
            playerOnTop.GetComponent<Player>().Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
