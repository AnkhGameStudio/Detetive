using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))] 
[RequireComponent(typeof(CharacterController))]

public class Passos : MonoBehaviour {

	public AudioClip SomDePassos;
	private AudioSource audioControle;
	private CharacterController controller;
	private bool Esperando;
	public float TempoDeEspera;
	public float TempoDoPasso = 0.6f;

	//variaveis de movimento da camera
	private GameObject CameraDoPlayer;
	public float intensidadeDoMovimento;
	private Vector3 PosicaoInicialDaCamera;
	public float movimentoDaCamera;
	public bool comecarContagem;

	void Start (){

		audioControle = GetComponent<AudioSource> ();
		CameraDoPlayer = GameObject.FindWithTag("MainCamera");
		comecarContagem = false;
		PosicaoInicialDaCamera = CameraDoPlayer.transform.localPosition;
		controller = GetComponent<CharacterController> ();

	}

	public void AnimaPassos (){

		CameraDoPlayer.transform.localPosition = Vector3.Lerp (CameraDoPlayer.transform.localPosition,
		                                                       PosicaoInicialDaCamera * movimentoDaCamera + PosicaoInicialDaCamera,
		                                                       intensidadeDoMovimento * Time.deltaTime);  

			if (!audioControle.isPlaying) {
				TocarSons ();
				if (comecarContagem == false) {
					movimentoDaCamera += Time.deltaTime;
				}

				if (comecarContagem == true) {
					movimentoDaCamera -= Time.deltaTime;
				}
			}

		if (movimentoDaCamera >= TempoDeEspera) {
			comecarContagem = true;
		}
		if (movimentoDaCamera <= 0) {
			comecarContagem = false;
		}                                                         
		if (Esperando == true) { 
			TempoDeEspera -= Time.deltaTime;
		}
		if (TempoDeEspera <= 0) {
			Esperando = false;
		}
	
	}

	void TocarSons (){
		if (Esperando == false) {
			audioControle.Stop ();
			TempoDeEspera = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? TempoDoPasso * 0.6f : TempoDoPasso;
			Esperando = true;
			audioControle.PlayOneShot (SomDePassos);
		}
	}
}