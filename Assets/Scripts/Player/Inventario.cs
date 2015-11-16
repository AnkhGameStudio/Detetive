using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class Inventario : MonoBehaviour {

	public Transform itemSelecionado;
	public Transform slotSelecionado;
	public Transform slotOriginal;

	private Transform inventario;
	public GameObject slotPrefab;
	public GameObject itemPrefab;
	public Vector2 capacidadeInventario = new Vector2(4,2);
	public float tamanhoSlot;
	public Vector2 dimensoesInventario;

	private List<Slot> slotsList = new List<Slot>();

	private bool movendoItem = false;
	private bool inventarioCheio = false;

	public Vector2 tamanhoTela;
	public Vector2 tamanhoBack;
	public Vector2 tamanhoFundo;
	public Vector3 boxColliderNovoSlot;

	void Start(){

		CalcularDimensoes ();
		DimensionarSlots ();

	}

	void Update(){


	}

	public Transform ItemSelecionado{
		get{return itemSelecionado;}
		set{itemSelecionado = value;}
	}

	public Transform SlotSelecionado{
		get{return slotSelecionado;}
		set{slotSelecionado = value;}
	}

	public Transform SlotOriginal{
		get{return slotOriginal;}
		set{slotOriginal = value;}
	}

	public void AdicionaItem(Item item){

		int contagem = 0;

		for (int x = 0; x < slotsList.Count; x++) {
			contagem++; 
			if(!slotsList[x].ocupado){
				slotsList[x].ocupado = true;
				slotsList[x].SlotItem = item;
				break;
			} 
		}

		if (contagem == slotsList.Count) {
			inventarioCheio = true;
		} else {
			inventarioCheio = false;
		}
	}

	public bool InventorioAtivo {
		get { return this.GetComponent<Canvas> ().enabled;}
		set { this.GetComponent<Canvas> ().enabled = value;}
	}

	public bool MovendoItem {
		get{return movendoItem;}
		set{movendoItem = value;}
	}

	public bool InventarioCheio {
		get{return inventarioCheio;}
		set{inventarioCheio = value;}
	}

	public void DimensionarSlots (){

		inventario = GameObject.Find ("Fundo").GetComponent<Transform> ();
		
		for (int x = 0; x < capacidadeInventario.x; x++) {
			for (int y = 0; y < capacidadeInventario.y; y++) {
				
				GameObject slot = Instantiate(slotPrefab) as GameObject;
				slot.transform.parent = inventario;
				slot.name = "slot_"+x+"_"+y;
				
				slot.GetComponent<RectTransform>().localPosition = new Vector3 (0,0,0);
				
				float tamanhoX = 1 / capacidadeInventario.x;
				float tamanhoY = 1 / capacidadeInventario.y;

				// tamanho do painel do slot
				slot.GetComponent<RectTransform>().anchorMin = new Vector2(tamanhoX * x, tamanhoY * y);
				slot.GetComponent<RectTransform>().anchorMax = new Vector2(tamanhoX + (tamanhoX * x), tamanhoY + (tamanhoY * y));


				slot.GetComponent<RectTransform>().sizeDelta = new Vector2(1,1);
				slot.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
				slot.GetComponent<RectTransform>().localEulerAngles = new Vector3(0,0,0);

				boxColliderNovoSlot = new Vector3 (tamanhoFundo.x * tamanhoX * 3, tamanhoFundo.y * tamanhoY * 3, 1);
				slot.GetComponent<BoxCollider>().size = boxColliderNovoSlot;
				slot.GetComponent<Slot>().TamanhoBoxColliderSlot = boxColliderNovoSlot;

				slotsList.Add(slot.GetComponent<Slot>());
				
			}
		}

	}

	public void CalcularDimensoes (){

		// tamanho da tela
		tamanhoTela = new Vector2 (Screen.width, Screen.height);
	
		// tamanho do inventario
		tamanhoBack = new Vector2 (GameObject.Find ("BackgroundInventario").GetComponent<RectTransform> ().anchorMax.x
			- GameObject.Find ("BackgroundInventario").GetComponent<RectTransform> ().anchorMin.x,
	                                  GameObject.Find ("BackgroundInventario").GetComponent<RectTransform> ().anchorMax.y
			- GameObject.Find ("BackgroundInventario").GetComponent<RectTransform> ().anchorMin.y);

		tamanhoBack = new Vector2 (tamanhoTela.x * tamanhoBack.x, tamanhoTela.y * tamanhoBack.y);
	
		// tamanho do fundo
		tamanhoFundo = new Vector2 (GameObject.Find ("Fundo").GetComponent<RectTransform> ().anchorMax.x
			- GameObject.Find ("Fundo").GetComponent<RectTransform> ().anchorMin.x,
	                                   GameObject.Find ("Fundo").GetComponent<RectTransform> ().anchorMax.y
			- GameObject.Find ("Fundo").GetComponent<RectTransform> ().anchorMin.y);

		tamanhoFundo = new Vector2 (tamanhoBack.x * tamanhoFundo.x, tamanhoBack.y * tamanhoFundo.y);

	}

}