using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Item : MonoBehaviour {

	private bool mover;
	private float distance;
	private Transform slot;
	private Inventario inventario;
	private Canvas infoPistas;

	public string nome;
	public enum Tipo { misc };
	[SerializeField] public Tipo tipo;
	public string descricao;
	[SerializeField] public Sprite sprite;


	void Start(){

		slot = transform.parent;
		inventario = GameObject.Find ("Inventario").GetComponent<Inventario> ();
		infoPistas = GameObject.Find ("InfoPistas").GetComponent<Canvas>();
	
	}

	// Update is called once per frame
	void Update () {

		if(mover){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector3 rayPoint = ray.GetPoint(distance);
			transform.position = rayPoint;
		}
	
	}

	void OnMouseEnter(){

		if (inventario.InventorioAtivo && !inventario.MovendoItem) {
			inventario.ItemSelecionado = this.transform;
			inventario.SlotSelecionado = slot;

			infoPistas.GetComponent<InfoPistas> ().ApresentacaoInv (this.gameObject);
			infoPistas.enabled = true;
		}

	}

	void OnMouseExit(){
		infoPistas.GetComponent<InfoPistas> ().Reset();
	}

	void OnMouseDown(){
		inventario.slotOriginal = slot;
		slot.GetComponent<Slot> ().ocupado = false;
		distance = Vector3.Distance(transform.position, Camera.main.transform.position);
		this.GetComponent<Collider> ().enabled = false;
		mover = true;
		inventario.MovendoItem = true;
	}

	void OnMouseUp()
	{
		if (!inventario.slotOriginal.gameObject.GetComponent<Slot> ().ocupado) {

			transform.parent = inventario.SlotSelecionado;
			slot = inventario.SlotSelecionado;
			slot.GetComponent<Slot> ().ocupado = true;
			transform.localPosition = Vector3.zero;
			this.GetComponent<Collider> ().enabled = true;
			mover = false;
			inventario.MovendoItem = false;

		} else {

			transform.parent = inventario.slotOriginal;
			slot = inventario.slotOriginal;
			slot.GetComponent<Slot> ().ocupado = true;
			transform.localPosition = Vector3.zero;
			this.GetComponent<Collider> ().enabled = true;
			mover = false;
			inventario.MovendoItem = false;
		}
	}

	public void Identidade (string n, Tipo t, string d){

		nome = n;
		tipo = t;
		descricao = d;

	}

	public string Nome{
		get { return nome;}
	}
	
	public string Descricao{
		get { return descricao;}
	}

}
