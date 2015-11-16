using UnityEngine;
using System.Collections;

public class Olhar : MonoBehaviour {

	public float velocidadeHorizontal;
	public float velocidadeVertical;
	public float maximaRotacaoVertical;
	private float anguloAtualX;
	private float anguloAtualY;
	private Vector3 direcaoOlhar;
	private Vector3 deslocamentoOlhar;
	private GameObject olhar;
	private GameObject mira;
	private Mira alvo;
	public float speed = 1;
	public float bordaHorizontal = 200;
	public float bordaVertical = 50;

	void Start (){
	
		olhar = GameObject.FindWithTag("MainCamera");
		mira = GameObject.Find("Mira");
		alvo = mira.GetComponent<Mira> ();

	}

	public void DirecaoOlhar () {

		if (alvo.Alvo.x > Screen.width - bordaHorizontal) {
			anguloAtualX += velocidadeHorizontal * Time.deltaTime;
			if (alvo.Alvo.x > Screen.width - bordaHorizontal / 2){
				anguloAtualX += velocidadeHorizontal * Time.deltaTime;
			}
		}

		if (alvo.Alvo.x < 0 + bordaHorizontal) {
			anguloAtualX -= velocidadeHorizontal * Time.deltaTime;
			if (alvo.Alvo.x < 0 + bordaHorizontal / 2) {
				anguloAtualX -= velocidadeHorizontal * Time.deltaTime;
			}
		}

		if (alvo.Alvo.y > Screen.height - bordaVertical) {
			anguloAtualY += velocidadeVertical * Time.deltaTime;
			if (alvo.Alvo.y > Screen.height - bordaVertical / 2) {
				anguloAtualY += velocidadeVertical * Time.deltaTime;
				
			}

		}

		if (alvo.Alvo.y < 0 + bordaVertical) {
			anguloAtualY -= velocidadeVertical * Time.deltaTime;
			if (alvo.Alvo.y < 0 + bordaVertical / 2) {
				anguloAtualY -= velocidadeVertical * Time.deltaTime;
			}
		}

		anguloAtualY = Mathf.Clamp(anguloAtualY, -maximaRotacaoVertical, maximaRotacaoVertical);
		
		direcaoOlhar = new Vector3(anguloAtualY, anguloAtualX, 0);
		
		olhar.transform.rotation = Quaternion.Euler (direcaoOlhar);

	}

	public float AtualizaDirecao{
		get { return anguloAtualX;}
	}

}