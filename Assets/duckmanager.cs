using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class duckmanager : MonoBehaviour
{
    public int difficulty;
    public GameObject DuckPrefab;

    private int[,] usedCombos;
    private int    n_usedCombos;

    int total_families;

    void Start()
    {
        duck_controller.OnDie += UpdateObituaries;
    }

    // Start is called before the first frame update
    public void StartTheGame()
    {
        Undatakah();

        usedCombos = new int[27, 5];
        n_usedCombos = 0;

        // Create Family of Families
        int cx_eye = 0;
        int cx_mouth = 0;
        int cx_foot = 0;

        int total_ducks = 0;
        total_families = 0;
        int max_ducks   = 2 * difficulty;

        do
        {
            GenerateUniqueCombo( ref cx_eye, ref cx_mouth, ref cx_foot);
            usedCombos[n_usedCombos,0] =  cx_eye;
            usedCombos[n_usedCombos,1] =  cx_mouth;
            usedCombos[n_usedCombos,2] =  cx_foot;
            n_usedCombos++;
            total_ducks += CreateFamilyOfDucks(cx_eye, cx_mouth, cx_foot);
            usedCombos[n_usedCombos, 3] = total_ducks;
            usedCombos[n_usedCombos, 4] = total_ducks;
            total_families++;
        } while (total_ducks < max_ducks);


        //difficulty++;
    }

    void UpdateObituaries(int eye_type, int mouth_type, int foot_type)
    {
        for (int j = 0; j < n_usedCombos; j++)
        {
            if (usedCombos[j, 0] == eye_type && usedCombos[j, 1] == mouth_type && usedCombos[j, 2] == foot_type)
            {
                usedCombos[j, 4]--;
                //Debug.Log(usedCombos[j, 4]);
            }
        }
    }

    public float Evaluate()
    {
        float numerator = 0.0f;
        float denominator = 0.0f;
        int uniqued = 0;

        for (int j = 0; j < n_usedCombos; j++)
        {
            numerator   += (float)usedCombos[j, 4];
            denominator += (float)usedCombos[j, 3];

            if ( usedCombos[j, 4] == 1 )
            {
                uniqued++;
            }
        }

        if (denominator <= 0.0f) {
            return 0;
        }
        else
        {
            Debug.Log(numerator);
            return numerator / denominator;
        }

    }

    void Undatakah()
    {
        GameObject[] remove_these = GameObject.FindGameObjectsWithTag("duck");

        foreach(GameObject rt in remove_these)
        {
            Destroy(rt);
        }
    }

    void GenerateUniqueCombo(ref int o_eye, ref int o_mouth, ref int o_foot)
    {
        if(n_usedCombos < 27)
        {
             o_eye = Random.Range(1, 3);
             o_mouth = Random.Range(1, 3);
             o_foot = Random.Range(1, 3);

            for (int j = 0; j < n_usedCombos; j++)
            {
                if (usedCombos[j,0] == o_eye && usedCombos[j,1] == o_mouth && usedCombos[j,2] == o_foot)
                {
                    GenerateUniqueCombo(ref o_eye, ref o_mouth, ref o_foot);
                }
            }
        }

    }

    int CreateFamilyOfDucks(int eye, int mouth, int foot)
    {
        int n_ducks_in_family = Random.Range(1, 4);
        //Debug.Log("n_ducks_in_family");
        //Debug.Log(n_ducks_in_family);

        for (int d = n_ducks_in_family; d > 0; d--)
        {
            CreateDuck(eye, mouth, foot);
        }

        return n_ducks_in_family;
    }

    void CreateDuck(int eye, int mouth, int foot)
    {
        GameObject duck_instance = Instantiate(DuckPrefab, new Vector3(Random.Range(-4.5f, 4.5f), Random.Range(-4.5f, 1.5f), 0), Quaternion.identity);
        Transform di_transform = duck_instance.GetComponent<Transform>();

        // tell the duck what it is
        duck_instance.GetComponent<duck_controller>().eye_type = eye;
        duck_instance.GetComponent<duck_controller>().mouth_type = mouth;
        duck_instance.GetComponent<duck_controller>().foot_type = foot;

        switch (eye)
        {
            case 3: di_transform.Find("eye_3").GetComponent<SpriteRenderer>().enabled = true;
                break;
            case 2:
                di_transform.Find("eye_2").GetComponent<SpriteRenderer>().enabled = true;
                break;
            case 1:
            default:
                di_transform.Find("eye_1").GetComponent<SpriteRenderer>().enabled = true;
                break;
        }

        switch (mouth)
        {
            case 3:
                di_transform.Find("mouth_3").GetComponent<SpriteRenderer>().enabled = true;
                break;
            case 2:
                di_transform.Find("mouth_2").GetComponent<SpriteRenderer>().enabled = true;
                break;
            case 1:
            default:
                di_transform.Find("mouth_1").GetComponent<SpriteRenderer>().enabled = true;
                break;
        }

        switch (foot)
        {
            case 3:
                di_transform.Find("foot_3").GetComponent<SpriteRenderer>().enabled = true;
                break;
            case 2:
                di_transform.Find("foot_2").GetComponent<SpriteRenderer>().enabled = true;
                break;
            case 1:
            default:
                di_transform.Find("foot_1").GetComponent<SpriteRenderer>().enabled = true;
                break;
        }
    }


}
