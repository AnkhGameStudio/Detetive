using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Mira : MonoBehaviour {

	[Range(0, 1)] public float sensibilidade = 0.1f;
	private Vector3 foco;
	private Vector3 screenPos;
	private MeshRenderer alerta;
	private bool visivel;
	public Material materialVer;
	private bool ver;
	public Material materialPegar;
	private bool pegar;
	public Material materialMexer;
	private bool mexer;

	void Start (){
		alerta = GameObject.Find ("Alerta").GetComponent<MeshRenderer> ();
		ver = false;
		pegar = false;
	}

	void Update()
	{
		Mover ();

		if (ver) {
			alerta.material = materialVer;
		}

		if (pegar) {
			alerta.material = materialPegar;
		}

		if (mexer) {
			alerta.material = materialMexer;
		}
	
	}

	void Mover(){

		foco = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.25f));
		screenPos = Camera.main.WorldToScreenPoint(foco);
		screenPos.x = Mathf.Clamp (screenPos.x, 0, Screen.width);
		screenPos.y = Mathf.Clamp (screenPos.y, 0, Screen.height);
		foco = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0.25f));;
		this.transform.position = Vector3.Lerp(this.transform.position, foco, sensibilidade);
		this.transform.rotation = Quaternion.Euler (Camera.main.transform.eulerAngles);

	}

	public Vector3 Alvo{
		get {return screenPos;}
	}

	public bool Alertas{
		get { return alerta.enabled;}
		set { alerta.enabled = value;}
	}

	public bool Ver{
		get { return ver;}
		set { ver = value;}
	}

	public bool Pegar{
		get { return pegar;}
		set { pegar = value;}
	}

	public bool Mexer{
		get { return mexer;}
		set { mexer = value;}
	}


}
