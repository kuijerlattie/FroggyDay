using UnityEngine;
using System.Collections;

public class AttackCollider{

	public static void MakeAttack(GameObject gameObject, Vector3 position, float radius, Transform attacker)
    {
       GameObject _gameObject = GameObject.Instantiate(gameObject);
        //_gameObject.transform.SetParent(attacker);
        _gameObject.transform.position = position;
        _gameObject.GetComponent<SphereCollider>().radius = radius;
        _gameObject.GetComponent<HitboxScript>().layer = attacker.gameObject.layer;
        _gameObject.GetComponent<HitboxScript>().Spawn(0, 0);
    }

    public static void MakeSpell(GameObject gameObject, Vector3 position, float radius, Transform attacker)
    {
        Vector3 _forward = new Vector3(attacker.forward.x, attacker.forward.y, attacker.forward.z);
        for (int i = 0; i < 10; i++)
        {
            GameObject _gameObject = GameObject.Instantiate(gameObject);

            _gameObject.transform.position = position + _forward * i;
            _gameObject.GetComponent<SphereCollider>().radius = radius;
            _gameObject.GetComponent<HitboxScript>().layer = attacker.gameObject.layer;
            _gameObject.GetComponent<HitboxScript>().Spawn((float)i/10, 1);
        }
        
    }



}
