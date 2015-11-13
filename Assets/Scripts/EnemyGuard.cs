using UnityEngine;
using System.Collections;

public class EnemyGuard : MonoBehaviour
{
	public STATES cstate;
    void Awake()
    {
		rb = GetComponent<Rigidbody>();

        Messenger.AddListener<string>("entitydied", Die);	// Listens for the instance of its own death, _Stats
        Messenger.MarkAsPermanent("entitydied");

		fsm.AddState(STATES.INIT);
		fsm.AddState(STATES.IDLE);
		fsm.AddState(STATES.CHASING);
		fsm.AddState(STATES.DEAD);

		Callback<string> ToIdle;
		Callback<string> ToChasing;
		Callback<string> ToDead;
		ToIdle = Idle;
		ToChasing = Chasing;

		fsm.AddTransition(STATES.INIT, 		STATES.IDLE, 		ToIdle);
		fsm.AddTransition(STATES.IDLE,		STATES.CHASING,		ToChasing);
		fsm.AddTransition(STATES.IDLE, 		STATES.DEAD, 		ToIdle);
		fsm.AddTransition(STATES.CHASING, 	STATES.IDLE, 		ToIdle);
		fsm.AddTransition(STATES.CHASING, 	STATES.DEAD, 		ToIdle);

		fsm.m_currentState = STATES.INIT;
		fsm.MakeTransitionTo(STATES.IDLE);
    }
	
	void Idle(string a_string)
	{
		target = null;	// Set target to nothing
		StopAllCoroutines();
	}
	void Chasing(string a_string)
	{
		StartCoroutine(ChaseTarget());
	}
	
	IEnumerator ChaseTarget()
	{
		RaycastHit hit;

		while(fsm.m_currentState == STATES.CHASING)
		{
			yield return null;
			rb.velocity = new Vector3(0, rb.velocity.y, 0);

			Vector3 currentPos = new Vector3(transform.position.x, 0, transform.position.z);
			Vector3 targetPos  = new Vector3(target.transform.position.x, 0, target.transform.position.z);

			Vector3 heading = Vector3.Normalize(targetPos - currentPos); 

			float yDist = target.transform.position.y - transform.position.y;

			float dist = Vector3.Distance(transform.position, target.transform.position);                        	

			bool hasHit = Physics.Raycast(transform.position, heading, out hit, dist);	                       
			if (hasHit)
			{
				if(hit.transform.gameObject == target || hit.transform.CompareTag("Enemy"))                                                      
				{
					rb.AddForce(heading * speedConst * speed);

					if(yDist > 1f)
					{
						if(canJump)
						{
							rb.AddForce(transform.up * speed * yDist * 100);
							canJump = false;
						}
					}
				}
				Debug.DrawLine(transform.position, hit.transform.position, Color.blue);
			}

			Debug.DrawLine(transform.position, (transform.position + heading), Color.red);
		}
		fsm.MakeTransitionTo(STATES.IDLE);
	}

    void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("Player") && target == null	)
        {
            target = other.gameObject;
			fsm.MakeTransitionTo(STATES.CHASING);
        }
    }

    void OnTriggerExit(Collider other)
	{
		if (other.gameObject == target)
		{
			fsm.MakeTransitionTo(STATES.IDLE);
		}

    }

    void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Player"))
        {
            //Messenger.Broadcast("modstat", other.gameObject.GetInstanceID().ToString(), "health", -1f);
        }
		if(other.contacts[0].point.y <= transform.position.y)
		{
			canJump = true;
		}
    }

    void Die(string a_instance)
    {
		if(a_instance == gameObject.GetInstanceID().ToString())
			Destroy(gameObject);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener<string>("entitydied", Die);
    }

////// VARIABLES // VARIABLES // VARIABLES // VARIABLES // VARIABLES 
    public float speed;

    public GameObject target;

	bool canJump;

    const float speedConst = 100;

    Vector3 nxtPos = new Vector3();
    Vector3 forward = new Vector3();

    Rigidbody rb;


	public enum STATES
	{
		INIT,
		IDLE,
		TRACKING,
		CHASING,
		DEAD,
	}
	
	_FSM<STATES> fsm = new _FSM<STATES>();
}
