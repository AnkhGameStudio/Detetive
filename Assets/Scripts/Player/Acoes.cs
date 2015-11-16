using UnityEngine;
using System.Collections;

public class Acoes : MonoBehaviour {

	private Inventario inventario;
	private Player player;

	void Start(){
		inventario = GameObject.Find ("Inventario").GetComponent<Inventario> ();
		player = GameObject.Find ("Player").GetComponent<Player> ();
	}

	public void Fazer () {

		if (Input.GetKeyDown ("i") && !player.Analisando) {

			inventario.InventorioAtivo = (inventario.InventorioAtivo) ? false : true;
			player.TravarMovimento = (inventario.InventorioAtivo) ? true : false;
			player.TravarOlhar = (inventario.InventorioAtivo) ? true : false;
		
		}

		if (Input.GetKeyDown (KeyCode.Escape) && inventario.InventorioAtivo) {
			
			inventario.InventorioAtivo = false;
			player.TravarMovimento = false;
			player.TravarOlhar = false;
			
		}

		if (Input.GetKeyDown ("e")) {
		


		} 

	}
}
