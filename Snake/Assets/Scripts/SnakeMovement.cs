using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SnakeMovement : MonoBehaviour
{
    public Vector3 _direction=Vector3.right;
    public float Speed;
    float TimeInterval;
    float TimeElapse;
    // Growing Functionlity
    public GameObject bodyPrefab;
    public List<Transform> _parts;

    private Vector3 initialPosition;
    [SerializeField]Food food;

    //TouchInputs
    Vector3 StartTouchpos, EndTouchPos;

    public bool canTeleport;
    BoxCollider2D col;
    [SerializeField] Transform Holder;
    public int StartBodyCount = 2;
    Color mycolor;
    [SerializeField] AudioClip DeathSound;
    
    private void Start()
    {
        mycolor = GetComponent<SpriteRenderer>().color;
        col = GetComponent<BoxCollider2D>();
        initialPosition = transform.position;
        TimeInterval = 1 / Speed;
        _parts.Add(this.transform);
       
        for(int i=0; i < StartBodyCount; i++)
        {
            Transform newpart = Instantiate(bodyPrefab.transform);

            _parts.Add(newpart);
            newpart.position = new Vector3(initialPosition.x - i, initialPosition.y, initialPosition.z);
            newpart.parent = Holder;
            newpart.GetComponent<SpriteRenderer>().color = mycolor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        TimeElapse += Time.deltaTime;

# if UNITY_EDITOR
        //gatheringInputs
        if (Input.GetKeyDown(KeyCode.A) && _direction!=Vector3.right)
        {
            _direction = Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.D) && _direction != Vector3.left)
        {
            _direction = Vector3.right;
        }
        if (Input.GetKeyDown(KeyCode.W) && _direction != Vector3.down)
        {
            _direction = Vector3.up;
        }
        if (Input.GetKeyDown(KeyCode.S) && _direction != Vector3.up)
        {
            _direction = Vector3.down;
        }
#endif

        HandleInput();

        //Movement Logic
        if (TimeElapse >= TimeInterval)
        {         
            transform.position = new Vector3(Mathf.Round(transform.position.x + _direction.x), Mathf.Round(transform.position.y + _direction.y), 0);
            TimeElapse = 0;

            for(int i = _parts.Count - 1; i > 0; i--)
            {
                _parts[i].position = _parts[i - 1].position;
            }
        }
    }


    void HandleInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                StartTouchpos = touch.position;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                EndTouchPos = touch.position;
                HandleSwipe();
            }
        }
    }

    void HandleSwipe()
    {
        float Xswipe = EndTouchPos.x - StartTouchpos.x;
        float Yswipe = EndTouchPos.y - StartTouchpos.y;
        if (Mathf.Abs(Xswipe) > Mathf.Abs(Yswipe))
        {
            //swipe in x direction
            if (Xswipe > 0 && _direction!=Vector3.left)
            {
                _direction = Vector3.right;
            }
            if(Xswipe<0 &&_direction!=Vector3.right)
            {
                _direction = Vector3.left;
            }
        }
        else if(Mathf.Abs(Xswipe) < Mathf.Abs(Yswipe))
        {
            if (Yswipe > 0 && _direction != Vector3.down)
            {
                _direction = Vector3.up;
            }
            if (Yswipe < 0 && _direction != Vector3.up)
            {
                _direction = Vector3.down;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            Grow();
        }
        if (collision.CompareTag("Boundary"))
        {
            //SceneManager.LoadScene("SampleScene");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            if (!canTeleport)
            {
                for (int i = 1; i < _parts.Count; i++)
                {
                    Destroy(_parts[i].gameObject);
                }
                _parts.Clear();
                _parts.Add(transform);
                transform.position = initialPosition;
                _direction = Vector3.right;
                food.ResetScore();
                SoundManager.instance.PlayAudio(DeathSound);
            }
            else
            {
                StartCoroutine(TimelyCollider());
                Vector3 temp = transform.position;
                if (_direction == Vector3.right || _direction == Vector3.left)
                    temp.x =-temp.x;
                if (_direction == Vector3.up || _direction == Vector3.down)
                    temp.y = -temp.y;
                transform.position = temp ;
            }
        }
    }

    IEnumerator TimelyCollider()
    {
        col.enabled = false;
        yield return new WaitForSeconds(0.1f);
        col.enabled = true;
    }

    void Grow()
    {
        Transform newpart= Instantiate(bodyPrefab.transform);
        newpart.position = _parts[_parts.Count - 1].position;

        _parts.Add(newpart);
        newpart.gameObject.transform.parent = Holder;
        newpart.GetComponent<SpriteRenderer>().color = mycolor;
        
    }

  
    public List<Transform> ReturnBodyParts()
    {
        return _parts;
    }
}
