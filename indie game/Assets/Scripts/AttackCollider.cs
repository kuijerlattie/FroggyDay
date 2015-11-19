using UnityEngine;
using System.Collections;

public class AttackCollider{

	public static void MakeAttack(GameObject gameObject, Vector3 position, float radius, Transform attacker)
    {
        GameObject _gameObject = GameObject.Instantiate(gameObject);
        _gameObject.transform.position = position;
        _gameObject.transform.localScale = new Vector3(radius, radius, radius);
        _gameObject.GetComponent<HitboxScript>().layer = attacker.gameObject.layer;
        _gameObject.GetComponent<HitboxScript>().Spawn(0, 0.5f);
    }

    public static void MakeSpellBasic(GameObject gameObject, Vector3 position, float radius, Transform attacker)
    {
        Vector3 _forward = new Vector3(attacker.forward.x, attacker.forward.y, attacker.forward.z); ;
        RaycastHit hit;
        LayerMask layermask = ~((1 << 8) | (1 << 9));
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, layermask))
        {
            if ((hit.point - attacker.position).magnitude > 0.5f)
            {
                _forward = (hit.point - attacker.position).normalized;
            }
        }
        
        for (int i = 1; i < 8; i++)
        {
            GameObject _gameObject = GameObject.Instantiate(gameObject);
            _gameObject.transform.position = position + _forward * (1 + (i*2));
            _gameObject.transform.localScale = new Vector3(radius * 1, radius * 1, radius * 1);
            _gameObject.GetComponent<HitboxScript>().layer = attacker.gameObject.layer;
            _gameObject.GetComponent<HitboxScript>().Spawn((float)i / 10.0f, 0.5f);
        }
        
    }


    public static void MakeSpell(GameObject gameObject, Vector3 position, float radius, Transform attacker)
    {
        Vector3 _forward = new Vector3(attacker.forward.x, attacker.forward.y, attacker.forward.z);
        Vector3 _right = new Vector3(attacker.right.x, attacker.right.y, attacker.right.z);
        for (int i = 1; i < 8; i++)
        {
            GameObject _gameObject = GameObject.Instantiate(gameObject);

            _gameObject.transform.position = position + _forward * i*2 + _right * i + _forward;
            _gameObject.transform.localScale = new Vector3(radius, radius, radius);
            _gameObject.GetComponent<HitboxScript>().layer = attacker.gameObject.layer;
            _gameObject.GetComponent<HitboxScript>().Spawn((float)i/10, 0.5f);
        }
        for (int i = 1; i < 8; i++)
        {
            GameObject _gameObject = GameObject.Instantiate(gameObject);

            _gameObject.transform.position = position + _forward * i * 2 - _right * i + _forward;
            _gameObject.transform.localScale = new Vector3(radius, radius, radius);
            _gameObject.GetComponent<HitboxScript>().layer = attacker.gameObject.layer;
            _gameObject.GetComponent<HitboxScript>().Spawn((float)i / 10, 0.5f);
        }

    }



}
