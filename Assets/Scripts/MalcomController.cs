using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalcomController : MonoBehaviour {
	
	public UnityEngine.AI.NavMeshAgent _agent;
	private Animator _animator;

	public Transform _destination = null;
	public Transform _replace;
	public int _act;

	void Awake () {
		_animator = gameObject.GetComponent<Animator> ();
		_agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
	}

	void Update () {
		_animator.SetFloat ("speed", _agent.velocity.magnitude);
		if (!_agent.isStopped && _agent.remainingDistance == 0) {
            _agent.updateRotation = true;
            if (_destination != null && _destination.localRotation.y != 0) {
                transform.rotation = Quaternion.Slerp (transform.rotation, _destination.rotation, 2f);
			}

		}
	}

    public void _agentReset()
    {
        _agent.isStopped = true;
        _agent.ResetPath();
        _destination = null;
    }
	public void _agentAct() {
		_agent.isStopped = true;
		_agent.ResetPath ();

		if (_destination != null) {
            _agent.destination = _destination.position;
		}
        _animator.SetInteger ("animation", _act);
	}
	public void _cancelAnimations() {
		_act = 0;
		_animator.SetInteger ("animation", _act);
	}
}