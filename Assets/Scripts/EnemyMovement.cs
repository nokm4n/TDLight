using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
	private Transform target;
	private int wavepointIndex = 0;

	private Enemy enemy;
	private bool gameOver = false;

	

	void Start()
	{
		enemy = GetComponent<Enemy>();
		enemy.animator.SetTrigger("Go");
		//wayIndex = enemy.GetIndexWay();
		//target = Waypoints.instance.waypoints[wayIndex][0].transform;
	}

	void Update()
	{
		if (gameOver) return;

		Vector3 dir = target.position - transform.position;
		dir.x = 0;
		dir.y = 0;
		transform.Translate(dir.normalized * enemy.GetSpeed() * Time.deltaTime, Space.World);

		if(transform.position.z < 0)
        {
			UIController.instance.x2ButtonScale();
        }

		if (transform.position.z - target.position.z <= 0.1f)
		{
			enemy.animator.SetTrigger("Bite");
			gameOver = true;
			LevelManager.instance.GameOver();
			Debug.Log("game over");
			//GetNextWaypoint();
		}

		//enemy.speed = enemy.startSpeed;
	}

	public void SetTarget(Transform target)
    {
		this.target = target;
    }

	/*void GetNextWaypoint()
	{
		if (wavepointIndex >= Waypoints.instance.waypoints[wayIndex].Count - 1)
		{
			//Debug.Log(wavepointIndex);
			EndPath();
			return;
		}

		wavepointIndex++;
		//target = Waypoints.instance.waypoints[wavepointIndex][wayIndex].transform;
		target = Waypoints.instance.waypoints[wayIndex][wavepointIndex].transform;
	}*/

	void EndPath()
	{
		LevelManager.instance.enemies.Remove(gameObject.GetComponent<Enemy>());
		//Destroy(gameObject);
		//PlayerStats.Lives--;
		//WaveSpawner.EnemiesAlive--;

	}

}
