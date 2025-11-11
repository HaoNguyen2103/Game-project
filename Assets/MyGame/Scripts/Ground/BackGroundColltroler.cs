using UnityEngine;

public class BackGroundColltroler : MonoBehaviour
{
    [Header("Background Settings")]
    private Renderer[] backGround;
    public float speedOne;
    public float speedTwo;
    public float speedThree;
    private Transform target;
    float startPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backGround = GetComponentsInChildren<Renderer>();
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        startPos = target.position.x;
        foreach (var item in backGround)
        {
            Debug.Log(item.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var x = target.position.x - startPos;
        if(backGround != null)
        {
           var offset = (x * speedOne) % 1;
           backGround[0].material.mainTextureOffset = new Vector2(offset, backGround[0].material.mainTextureOffset.y);
            var offset2 = (x * speedTwo) % 1;
            backGround[1].material.mainTextureOffset = new Vector2(offset2, backGround[1].material.mainTextureOffset.y);
            var offset3 = (x * speedThree) % 1;
            backGround[2].material.mainTextureOffset = new Vector2(offset3, backGround[2].material.mainTextureOffset.y);

        }
    }
}
