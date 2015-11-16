using UnityEngine;
using System.Collections;

// Componentes necessarios para funcionamento do Script Player
[RequireComponent (typeof (Movimento))]
[RequireComponent (typeof (Olhar))]
[RequireComponent (typeof (Mira))]
[RequireComponent (typeof (Acoes))]

public class Player : MonoBehaviour {

	// Propriedades do Movimento
	private Movimento move;
	private Olhar olhar;
	private Mira mira;
	private Acoes acoes;

	private Vector3 foco;

	[SerializeField] private bool travarOlhar;
	[SerializeField] private bool travarMovimento;
	[SerializeField] private bool travarAcoes;
	[SerializeField] private bool analisando;

	// Use this for initialization
	void Start () {

		// Ocultar Cursor do Mouse
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Confined;

		// Inicializaçao da variavel de Movimento
		move = GetComponent<Movimento> ();
		olhar = GetComponent<Olhar> ();
		acoes = GetComponent<Acoes> ();
		mira = GameObject.Find ("Mira").GetComponent<Mira> ();

		travarOlhar = false;
		travarMovimento = false;
		travarAcoes = false;

	}
	
	// Update is called once per frame
	void Update () {

		transform.rotation = Quaternion.Euler (0, olhar.AtualizaDirecao, 0);

		// Atualizaçao do Movimento
		if (!travarMovimento) {
			move.Move ();
		}

		// Atualizaçao da direçao do Olhar do Player
		if (!travarOlhar) {
			olhar.DirecaoOlhar ();
		}

		if (!travarAcoes) {
			acoes.Fazer();
		}
	
	}

	public bool TravarOlhar{
		set {travarOlhar = value;}
	}

	public bool TravarMovimento{
		set {travarMovimento = value;}
	}

	public bool Analisando{
		get {return analisando;}
		set {
			analisando = value;
			travarOlhar = (analisando) ? true : false;
			travarMovimento = (analisando) ? true : false;
			}
	}

}
