using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class Movimento : MonoBehaviour {

	// Propriedades do Movimento
	private CharacterController controller;
	private Passos passos;
	private Vector3 direcaoMovimento;
	private float gravidade = 9.8f;

	// Variaveis ajustaveis no Inspector do Unity
	public float velocidadeAndar;
	public float velocidadeCorrer;
	public float velocidadeDiagonal;

	private Mira mira;
	private Vector3 posicaoMira;

	// Use this for initialization
	void Start () {

		// Inicializaçao do Controle do Personagem
		controller = GetComponent<CharacterController> ();
		passos = GetComponent<Passos> ();
		mira = GameObject.Find ("Mira").GetComponent<Mira> ();

	}
	
	// Update is called once per frame
	public void Move () {

		// Normalizar direcoes
		direcaoMovimento = new Vector3 (0, 0, 0);


		// Gravidade
		if (!controller.isGrounded) {
			direcaoMovimento.y -= gravidade * Time.deltaTime;
		}

		// Deslocamento Lateral
		if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)){
			// Com velocidade de Corrida
			direcaoMovimento.x = Input.GetAxisRaw ("Horizontal") * velocidadeCorrer * Time.deltaTime;
		
		} else {
			// Com velocidade de normal
			direcaoMovimento.x = Input.GetAxisRaw ("Horizontal") * velocidadeAndar * Time.deltaTime;

		}

		// Deslocamento de avancar e recuar;
		if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)){
			// Com velocidade de Corrida
			direcaoMovimento.z = Input.GetAxisRaw ("Vertical") * velocidadeCorrer * Time.deltaTime;
		
		} else {
			// Com velocidade de normal
			direcaoMovimento.z = Input.GetAxisRaw ("Vertical") * velocidadeAndar * Time.deltaTime;
		
		}

		if (direcaoMovimento.x != 0 && direcaoMovimento.z != 0) {

			direcaoMovimento.x = direcaoMovimento.x * velocidadeDiagonal;
			direcaoMovimento.z = direcaoMovimento.z * velocidadeDiagonal;
		
		}

		if (direcaoMovimento.x != 0 || direcaoMovimento.z != 0) {
			passos.AnimaPassos ();
		}
			


		direcaoMovimento = transform.TransformDirection (direcaoMovimento);
		controller.Move(direcaoMovimento);
	
	}
}
