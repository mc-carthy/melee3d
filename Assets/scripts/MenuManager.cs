using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour {

	[SerializeField]
	private GameObject hero;
	[SerializeField]
	private GameObject tanker;
	[SerializeField]
	private GameObject soldier;
	[SerializeField]
	private GameObject ranger;

	private Animator heroAnim;
	private Animator tankerAnim;
	private Animator soldierAnim;
	private Animator rangerAnim;

	private void Start () {
		heroAnim = hero.GetComponent<Animator>();
		tankerAnim = tanker.GetComponent<Animator>();
		soldierAnim = soldier.GetComponent<Animator>();
		rangerAnim = ranger.GetComponent<Animator>();

		StartCoroutine(Showcase());
	}

	public void Battle () {
		SceneManager.LoadScene("main");
	}

	public void Quit () {
		Application.Quit();
	}

	private IEnumerator Showcase () {
		yield return new WaitForSeconds(1f);
		heroAnim.Play("spinAttack");
		yield return new WaitForSeconds(1f);
		tankerAnim.Play("attack");
		yield return new WaitForSeconds(1f);
		soldierAnim.Play("attack");
		yield return new WaitForSeconds(1f);
		rangerAnim.Play("attack");
		StartCoroutine(Showcase());
	}

}
