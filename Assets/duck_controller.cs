using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public delegate void duck_delegate(int a, int b, int c);

public class duck_controller : MonoBehaviour
{
    static int n_ducks = 0;
    int duck_number;

    private float time_to_move;
    private float time_left;

    public static event duck_delegate OnDie;

    public int eye_type;
    public int mouth_type;
    public int foot_type;

    public duck_controller()
    {
        duck_number = n_ducks;
        n_ducks++;
    }

    // Start is called before the first frame update
    void Start()
    {
        time_to_move = Random.Range(0.8f, 2.0f);
        time_left = time_to_move;

        foreach( Transform child in this.transform)
        {
            child.GetComponent<SpriteRenderer>().sortingOrder += 10 * duck_number;
        }
    }

    // Update is called once per frame
    void Update()
    {
        time_left -= Time.deltaTime;
        if(time_left <= 0.0f){
            time_left = time_to_move;
            Move();
        }
    }

    private void Move() {
        Vector3 temp_position = this.transform.position;
        float x_adjustment = Random.Range(-1.0f, 1.0f);
        temp_position.x = Mathf.Clamp(x_adjustment+ temp_position.x, -4.5f, 4.5f);
        // Flip sprite if going left/right
        temp_position.y = Mathf.Clamp(Random.Range(-1.0f,1.0f) + temp_position.y, -4.5f, 1.5f);
        this.transform.DOMove(temp_position,0.5f);
    }


    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnDie(eye_type,mouth_type,foot_type);
            Destroy(gameObject);
        }
    }
}
