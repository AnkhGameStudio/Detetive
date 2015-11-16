using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Bau : MonoBehaviour {

	public float alcanceVisao = 5;
	public float distanciaMao = 2;
	private float distancia;
	private bool consegueVer;
	private bool consegueMexer;

	public Canvas infoVisivel;
	[SerializeField] private float tempoInfoVisivel = 2;
	private float tempoAtualInfoVisivel;

	private GameObject player;
	private Mira mira;

	// Use this for initialization
	void Start () {

		player = GameObject.Find("Player");
		mira = GameObject.FindWithTag("Mira").GetComponent<Mira>();
	
	}
	
	// Update is called once per frame
	void Update () {

		distancia = Mathf.Abs(Vector3.Distance(this.transform.position, player.transform.position));
		
		if(distancia <= alcanceVisao){
			consegueVer = true;					

			if( distancia <= distanciaMao) {
				consegueMexer = true;
			} else {
				consegueMexer = false;
			}
			
		} else {
			consegueVer = false;
		}
		
		if (infoVisivel.enabled) {
			tempoAtualInfoVisivel -= Time.deltaTime;

			if(tempoAtualInfoVisivel < 0){
				infoVisivel.enabled = false;
			}
		}
	
	}

	public bool ConsegueVer {
		get{ return consegueVer;}
	}
	
	public bool ConsegueMexer {
		get { return consegueMexer;}
	}

	void OnMouseEnter(){

		if (consegueVer && !consegueMexer) {

			mira.Alertas = true;
			mira.Ver = true;
			mira.Pegar = false;
			mira.Mexer = false;
			
		}

		if (consegueMexer) {

			mira.Alertas = true;
			mira.Ver = true;
			mira.Pegar = false;
			mira.Mexer = true;
			
		}

	}

	void OnMouseOver(){

		if (consegueVer && !consegueMexer) {
			
			mira.Alertas = true;
			mira.Ver = true;
			mira.Pegar = false;
			mira.Mexer = false;
			
		}
		
		if (consegueMexer) {
			
			mira.Alertas = true;
			mira.Ver = true;
			mira.Pegar = false;
			mira.Mexer = true;
			
		}
	
	}
	
	void OnMouseExit(){

		mira.Alertas = false;

	}
	
	void OnMouseDown(){

		if (consegueMexer) {
			tempoAtualInfoVisivel = 0;
			player.GetComponent<Player> ().Analisando = true;
			this.GetComponent<Collider> ().enabled = false;
		} else {
			infoVisivel.enabled = true;
			tempoAtualInfoVisivel = tempoInfoVisivel;
		}

	}
}
