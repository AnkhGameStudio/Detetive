using UnityEngine;
using System.Collections;

public class Slot : MonoBehaviour {

	private Inventario inventario;
	public Item item;
	public bool ocupado;
	public Vector3 tamanhoBoxColliderSlot;

	void Start(){
	
		inventario = GameObject.Find ("Inventario").GetComponent<Inventario> ();
		ocupado = false;

	}

	void OnMouseEnter(){
		if (!ocupado) {
			inventario.SlotSelecionado = this.transform;
		}
	}

	void OnMouseExit(){
		inventario.SlotSelecionado = inventario.SlotOriginal;
	}

	public Item SlotItem{
		get{return item;}
		set{
			this.item = value;
			Item xitem = Instantiate(item);
			xitem.gameObject.transform.SetParent(this.gameObject.transform);

			xitem.GetComponent<RectTransform>().localPosition = new Vector3(0,0,0);
			xitem.GetComponent<RectTransform>().anchorMin = new Vector2(0,0);
			xitem.GetComponent<RectTransform>().anchorMax = new Vector2(1,1);
			xitem.GetComponent<RectTransform>().sizeDelta = new Vector2(1,1);
			xitem.GetComponent<RectTransform>().localScale = new Vector3(0.8f, 0.8f, 1f);
			xitem.GetComponent<RectTransform>().localEulerAngles = new Vector3(0,0,0);

			xitem.GetComponent<BoxCollider>().size = new Vector3(tamanhoBoxColliderSlot.x - (tamanhoBoxColliderSlot.x * 0.02f),
			                                                     tamanhoBoxColliderSlot.y - (tamanhoBoxColliderSlot.y * 0.02f),10);
		}
	}

	public bool Ocupado{
		get{return ocupado;}
		set{ocupado = value;}
	}

	public Vector3 TamanhoBoxColliderSlot{
		get{return tamanhoBoxColliderSlot;}
		set{tamanhoBoxColliderSlot = value;}
	}

}
