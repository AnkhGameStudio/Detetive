using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoPistas : MonoBehaviour {

	private GameObject item;
	public GameObject titulo;
	public GameObject descricao;
	private Inventario inventario;
	private Canvas canvas;
	[SerializeField] private GameObject botoes;
	[SerializeField] private Button guardar;
	[SerializeField] private Text cheio;
	void Start(){
	
		inventario = GameObject.Find ("Inventario").GetComponent<Inventario> ();
		canvas = GetComponent<Canvas> ();

	}

	void Update(){

		if (item == null) {
			this.GetComponent<Canvas> ().enabled = false;
		} else {
			this.GetComponent<Canvas> ().enabled = true;
		}

		if (inventario.InventorioAtivo && canvas.enabled) {
			botoes.SetActive(false);
			cheio.enabled = false;

		} 

		if (!inventario.InventorioAtivo && canvas.enabled){
			botoes.SetActive(true);

			if(inventario.InventarioCheio){
				cheio.enabled = true;
				guardar.interactable = false;
			} else {
				cheio.enabled = false;
				guardar.interactable = true;
			}

		}

	}

	public void ApresentacaoInfo(GameObject i){

		item = i;
		titulo.GetComponent<Text> ().text = item.GetComponent<Pista>().Nome;
		descricao.GetComponent<Text> ().text = item.GetComponent<Pista>().Descricao;
	
	}

	public void ApresentacaoInv(GameObject i){

		item = i;
		titulo.GetComponent<Text> ().text = item.GetComponent<Item>().Nome;
		descricao.GetComponent<Text> ().text = item.GetComponent<Item>().Descricao;
	
	}

	public void BotaoLargar (){
		item.GetComponent<Pista> ().ItemLargado ();
		Reset ();
	}

	public void BotaoGuardar (){
		item.GetComponent<Pista> ().ItemGuardado ();
		Reset ();
	}

	public void Reset (){

		titulo.GetComponent<Text> ().text = "";
		descricao.GetComponent<Text> ().text = "";
		item = null;
	}
	
}
