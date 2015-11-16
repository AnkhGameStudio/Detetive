using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Pista : MonoBehaviour {

	[SerializeField] private string nome = "";
	[SerializeField] private string descricaoVisivel = "";
	[SerializeField] private string descricaoInicial = "";
	[SerializeField] private string descricaoInventario = "";
	private string descricao = "";
	private Vector3 posicaoOriginal;
	private Vector3 rotacaoOriginal;

	private GameObject player;
	private GameObject scriptMira;
	public Canvas infoVisivel;
	[SerializeField] private float tempoInfoVisivel = 2;
	private float tempoAtualInfoVisivel;
	public Text texto;

	private Mira mira;
	private Inventario inventario;

	public Material OverColor;
	public Material originalColor;
	private bool dragging = false;
	private MeshRenderer renderer;

	private float distance;
	public float alcanceVisao = 5;
	public float distanciaMao = 2;
	private Canvas InfoPistas;
	private bool consegueVer;
	private bool conseguePegar;
	private float distancia;
	private bool itemPego;

	public float dist;

	[Range(0, 1)] public float sensibilidade = 0.1f;

	public Item item;

	void Start(){
	
		posicaoOriginal = this.transform.position;
		rotacaoOriginal = this.transform.eulerAngles;
		renderer = GetComponent<MeshRenderer> ();
		player = GameObject.Find("Player");
		scriptMira = GameObject.FindWithTag("Mira");
		mira = scriptMira.GetComponent<Mira> ();
		inventario = GameObject.Find("Inventario").GetComponent<Inventario>();
		InfoPistas = GameObject.Find ("InfoPistas").GetComponent<Canvas>();
		itemPego = false;
		infoVisivel.enabled = false;
		descricao = descricaoInicial;

	}

	void Update(){
	
		distancia = Mathf.Abs(Vector3.Distance(this.transform.position, player.transform.position));
			
		if(distancia <= alcanceVisao){

			consegueVer = true;					
			if( distancia <= distanciaMao) {

				conseguePegar = true;
			} else {
				conseguePegar = false;
			}
				
		} else {
			consegueVer = false;
		}

		if (itemPego) {
			ItemPego ();
		}

		if (infoVisivel.enabled) {

			tempoAtualInfoVisivel -= Time.deltaTime;
			if(tempoAtualInfoVisivel < 0){
				infoVisivel.enabled = false;
			}
		
		}
	}

	void OnMouseEnter(){

		if (consegueVer && !conseguePegar && !InfoPistas.enabled && !inventario.InventorioAtivo) {
			renderer.material = OverColor;
			mira.Alertas = true;
			mira.Ver = true;
			mira.Pegar = false;
		}

		if (conseguePegar && !InfoPistas.enabled && !inventario.InventorioAtivo) {
			renderer.material = OverColor;
			mira.Alertas = true;
			mira.Ver = true;
			mira.Pegar = true;
		}

	}

	void OnMouseOver(){
		
		if (consegueVer && !conseguePegar && !InfoPistas.enabled && !inventario.InventorioAtivo) {
			renderer.material = OverColor;
			mira.Alertas = true;
			mira.Ver = true;
			mira.Pegar = false;
		}
		
		if (conseguePegar && !InfoPistas.enabled && !inventario.InventorioAtivo) {
			renderer.material = OverColor;
			mira.Alertas = true;
			mira.Ver = true;
			mira.Pegar = true;
		}
		
	}
	
	void OnMouseExit(){

		renderer.material = originalColor;
		mira.Alertas = false;

	}

	void OnMouseDown(){

		if (consegueVer && conseguePegar && !inventario.InventorioAtivo && !player.GetComponent<Player> ().Analisando) {
			tempoAtualInfoVisivel = 0;
			player.GetComponent<Player> ().Analisando = true;
			this.GetComponent<Collider> ().enabled = false;
			itemPego = true;
		}

		if (consegueVer && !conseguePegar && !inventario.InventorioAtivo && !player.GetComponent<Player> ().Analisando) {
			texto.text = descricaoVisivel;
			infoVisivel.enabled = true;
			tempoAtualInfoVisivel = tempoInfoVisivel;
		}
	}

	public bool ConsegueVer {
		get{ return consegueVer;}
	}
	
	public bool ConseguePegar {
		get { return conseguePegar;}
	}

	void ItemPego(){

		this.transform.parent = GameObject.Find ("AlvoCentral").transform;
		this.transform.localPosition = transform.parent.transform.localPosition;
		InfoPistas.GetComponent<InfoPistas> ().ApresentacaoInfo (this.gameObject);
		InfoPistas.enabled = true;

	}

	public string Nome{
		get { return nome;}
	}

	public string Descricao{
		get { return descricao;}
	}

	public void ItemLargado(){

		itemPego = false;
		player.GetComponent<Player> ().Analisando = false;
		this.GetComponent<Collider> ().enabled = true;
		this.transform.parent = null;
		this.transform.position = posicaoOriginal;
		this.transform.rotation = Quaternion.Euler (rotacaoOriginal);
		InfoPistas.GetComponent<InfoPistas> ().Reset();
		InfoPistas.enabled = false;

	}

	public void ItemGuardado(){

		if (!inventario.InventarioCheio) {
			descricao = descricaoInventario;
			player.GetComponent<Player> ().Analisando = false;
			this.transform.parent = null;
			mira.Alertas = false;
			// passar identidade ao item para slot do inventario.
			item.Identidade (nome, Item.Tipo.misc, descricaoInventario);
			inventario.AdicionaItem (this.item);
			Destroy (gameObject);
		} else {
		
			Debug.Log("Cheio");

		}

	}

}
