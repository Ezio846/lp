using UnityEngine;

public class FreezePlayerAnim : MonoBehaviour
{
    [Header("玩家对象")]
    [SerializeField] private GameObject player;

    [Header("需要在冻结时关闭的脚本")]
    [SerializeField] private MonoBehaviour[] scriptsToDisable;

    private Rigidbody2D rb;
    private Animator anim;

    private void Awake()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

        if (player != null)
        {
            rb = player.GetComponent<Rigidbody2D>();
            anim = player.GetComponent<Animator>();
        }
    }

    public void FreezeAll()
    {
        Debug.Log("FreezeAll 被调用了");

        if (player == null) return;

        foreach (var script in scriptsToDisable)
        {
            if (script != null)
            {
                Debug.Log("关闭脚本: " + script.GetType().Name);
                script.enabled = false;
            }
        }

        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }

        if (anim != null)
        {
            anim.SetFloat("Speed", 0f);
            anim.SetBool("IsMoving", false);
        }
    }

    public void UnfreezeAll()
    {
        Debug.Log("UnfreezeAll 被调用了");

        if (player == null) return;

        foreach (var script in scriptsToDisable)
        {
            if (script != null)
            {
                Debug.Log("开启脚本: " + script.GetType().Name);
                script.enabled = true;
            }
        }
    }
}