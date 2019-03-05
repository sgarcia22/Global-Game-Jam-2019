using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WispsController : EventTrigger
{
    [SerializeField] private float seconds = 10f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("DestroyAfterSeconds");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime);

        Vector3 ghostPosition = Random.insideUnitSphere;
        ghostPosition = gameObject.transform.position - ghostPosition;
        float speed = Random.Range(0,1f);
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, ghostPosition, Time.deltaTime * speed);
    }

    IEnumerator DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(seconds);
        if (gameObject)
        {
            Destroy(gameObject);
            WispsSpawner.currentWisps--;
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        Destroy(gameObject);
        WispsSpawner.currentWisps--;
        WispsSpawner.score++;
    }

}
