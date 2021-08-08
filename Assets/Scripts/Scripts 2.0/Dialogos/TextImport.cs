using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Globales;

public class TextImport : MonoBehaviour {

	public Instantiate inst;
	public GameObject textBox;
	public int currentLine;
	public int endLine;
	public Text dialogue; 
	public float speed;

	// Llamar segun el lvl
	public AudioClip[] voiceDialogue = null;
	public string[] textLines = null;
	public TextAsset textDialogue = null;

	public MusicEnabled habilitarDialogo;

	void Awake ()	{
		inst = GameObject.Find("Instanciate").GetComponent<Instantiate> ();
		habilitarDialogo = GameObject.Find("MusicEnabled").GetComponent<MusicEnabled>();
	}

	void Update()
	{
		
		if (!GameController.activarCarga && GameController.cancelarActivarCarga)
		{
			GameController.data.imagenCarga.enabled = false;
			GameController.isActive = true;
			GameController.ActivarDialogo = true;

			if (GameController.ActivarDialogo) 
			{
				textDialogueLvl ();
				asignarDialogo ();
				voiceStart ();
				firstLine ();
				GameController.data.gameArma.enabled = false;
				GameController.data.gameLife.enabled = false;
				GameController.data.HUD_canvas.enabled = false;

				GameController.ActivarDialogo = false;
			}
		}

		if (GameController.isActive) {
			GameController.data.HUD_canvas.enabled = false;
			GameController.data.gameLife.enabled = false;
			GameController.data.gameArma.enabled = false;
			GameController.data.audioSourceTheme.volume = GameController.data.volumenDialogo;
			textBox.SetActive (true);
		} else if (!GameController.isActive) {
			textBox.SetActive (false);
		}

		// Entra a la zona del jefe o cuando muera, desactivar la instancia de enemigos despues del dialogo
		if (GameController.isActiveBoss || GameController.isActiveBossDead) {
			GameController.zonaJefe = false;
		}       

		textDialogueBoss ();
		textDialogueMuerte();

		if (Input.GetKeyDown(KeyCode.Space))
        {
			if (!GameController.data.isTyping && GameController.isActive) 
            {
				currentLine++;
				if (currentLine > endLine) 
                {
					currentLine = endLine;
					GameController.isActive = false;
					GameController.data.audioSourceDialogue.clip = null;
					GameController.ActiveInputs = true;
					textDialogue = null;
					textLines = null;
					voiceDialogue = null;
					currentLine = 0;
					GameController.data.audioSourceTheme.volume = GameController.data.volumenTheme;

					GameController.data.gameArma.enabled = true;
					GameController.data.gameLife.enabled = true;
					GameController.data.HUD_canvas.enabled = true;
					GameController.data.HUD_canvas.enabled = true;
					GameController.data.gameLife.enabled = true;
					GameController.data.gameArma.enabled = true;

					// La instancia se ejecuta porque no ha pasado por la zona del jefe o ha muerto
					if (GameController.zonaJefe) 
                    {
						GameController.ActiveInstantiate = true;
						Debug.Log ("wamitras");
					}
					// Despues que entra en la zona del jefe
					if (!GameController.zonaJefe) 
                    {
						Debug.Log ("alskjdlkasjd");
						GameController.data.iconoJefe.SetActive(true);
						GameController.Mecanicas = true;
						inst.InstantiateMusicBoss ();
						habilitarDialogo.enabledAudioPrincipal();
					}
				} else {
					StartCoroutine (TextScroll (textLines [currentLine]));
					GameController.data.audioSourceDialogue.clip = voiceDialogue [currentLine];
					habilitarDialogo.enabledDialogoPrincipal ();
				}
			} else if (GameController.data.isTyping && !GameController.data.cancelTyping) {
				GameController.data.cancelTyping = true;
				GameController.data.isTyping = false;
			}
		}
	}

	private IEnumerator TextScroll(string lineText)
	{
		int letter = 0;
		dialogue.text = "";
		GameController.data.isTyping = true;
		GameController.data.cancelTyping = false;
		while (GameController.data.isTyping && !GameController.data.cancelTyping && (letter < lineText.Length - 1))
		{
			dialogue.text += lineText[letter];
			letter++;
			yield return new WaitForSeconds (speed);
		}
		dialogue.text = lineText;
		GameController.data.isTyping = false;
		GameController.data.cancelTyping = false;
		GameController.cancelarActivarCarga = false;
	}

	void asignarDialogo (){
		textLines = (textDialogue.text.Split ('\n'));
		endLine = textLines.Length - 1;
		voiceDialogue = new AudioClip[endLine + 1];
	}

	void firstLine (){
		StartCoroutine (TextScroll (textLines [currentLine]));
		GameController.data.audioSourceDialogue.clip = voiceDialogue [0];
		habilitarDialogo.enabledDialogoPrincipal ();

	}

	void voiceStart(){
		if (GameController.lvl == 1){
			voiceDialogue [0] = Resources.Load ("Doc 1") as AudioClip;
			voiceDialogue [1] = Resources.Load ("Doc 1.2") as AudioClip;
			voiceDialogue [2] = Resources.Load ("Doc 1.3") as AudioClip;
			voiceDialogue [3] = Resources.Load ("Doc 1.4") as AudioClip;
			voiceDialogue [4] = Resources.Load ("Doc 1.5") as AudioClip;
		}

		if (GameController.lvl == 2){
			voiceDialogue [0] = Resources.Load ("Doc 2") as AudioClip;
			voiceDialogue [1] = Resources.Load ("Joe 2") as AudioClip;
			voiceDialogue [2] = Resources.Load ("Doc 2.1") as AudioClip;
			voiceDialogue [3] = Resources.Load ("Doc 2.2") as AudioClip;
			voiceDialogue [4] = Resources.Load ("Doc 2.3") as AudioClip;
		}

		if (GameController.lvl == 3){
			voiceDialogue [0] = Resources.Load ("Doc 3") as AudioClip;
			voiceDialogue [1] = Resources.Load ("Doc 3.1") as AudioClip;
			voiceDialogue [2] = Resources.Load ("Joe 3") as AudioClip;
		}

		if (GameController.lvl == 4){

			voiceDialogue [0] = Resources.Load ("Doc 4") as AudioClip;
			voiceDialogue [1] = Resources.Load ("Doc 4.1") as AudioClip;
			voiceDialogue [2] = Resources.Load ("Joe 4") as AudioClip;
			voiceDialogue [3] = Resources.Load ("ave 1") as AudioClip;
			voiceDialogue [4] = Resources.Load ("Joe 4.1") as AudioClip;
			voiceDialogue [5] = Resources.Load ("ave 2") as AudioClip;
			voiceDialogue [6] = Resources.Load ("Joe 4.2") as AudioClip;
			voiceDialogue [7] = Resources.Load ("ave 3") as AudioClip;
			voiceDialogue [8] = Resources.Load ("Joe 4.3") as AudioClip;
		}

		if (GameController.lvl == 5){
			voiceDialogue [0] = Resources.Load ("Doc 5") as AudioClip;
			voiceDialogue [1] = Resources.Load ("Joe 5") as AudioClip;
		}
	}

	void voiceEnemy(){
		if (GameController.lvl == 1) {
			if (GameController.isActiveBossDead) {
				voiceDialogue [0] = Resources.Load ("Candileja 5") as AudioClip;
			}else if (GameController.isActiveBoss) {
				voiceDialogue [0] = Resources.Load ("Candileja 1") as AudioClip;
				voiceDialogue [1] = Resources.Load ("Joe 1") as AudioClip;
				voiceDialogue [2] = Resources.Load ("Candileja 2") as AudioClip;
				voiceDialogue [3] = Resources.Load ("Joe 1.2") as AudioClip;
				voiceDialogue [4] = Resources.Load ("Candileja 3") as AudioClip;
				voiceDialogue [5] = Resources.Load ("Joe 1.3") as AudioClip;
				voiceDialogue [6] = Resources.Load ("Candileja 4") as AudioClip;
			}
		}

		if (GameController.lvl == 2) {
			if (GameController.isActiveBossDead) {
				voiceDialogue [0] = Resources.Load ("madreagua 3") as AudioClip;
			}else if (GameController.isActiveBoss) {
				voiceDialogue [0] = Resources.Load ("madreagua 1") as AudioClip;
				voiceDialogue [1] = Resources.Load ("Joe 2.1") as AudioClip;
				voiceDialogue [2] = Resources.Load ("madreagua 2") as AudioClip;
				voiceDialogue [3] = Resources.Load ("Joe 2.2") as AudioClip;
			}
		}

		if (GameController.lvl == 3) {
			if (GameController.isActiveBossDead) {
				voiceDialogue [0] = Resources.Load ("madremonte 4") as AudioClip;
			}else if (GameController.isActiveBoss) {
				voiceDialogue [0] = Resources.Load ("madremonte 1") as AudioClip;
				voiceDialogue [1] = Resources.Load ("Joe 3.1") as AudioClip;
				voiceDialogue [2] = Resources.Load ("madremonte 2") as AudioClip;
				voiceDialogue [3] = Resources.Load ("Joe 3.2") as AudioClip;
				voiceDialogue [4] = Resources.Load ("madremonte 3") as AudioClip;
				voiceDialogue [5] = Resources.Load ("Joe 3.3") as AudioClip;
			}
		}

		if (GameController.lvl == 4) {
			if (GameController.isActiveBossDead) {
				voiceDialogue [0] = Resources.Load ("ave 5") as AudioClip;
			}  else if (GameController.isActiveBoss) {
				voiceDialogue [0] = Resources.Load ("ave 4") as AudioClip;
				voiceDialogue [1] = Resources.Load ("Joe 4.4") as AudioClip;
			}
		}

		if (GameController.lvl == 5) {
			if (GameController.isActiveBoss) {
				voiceDialogue [0] = Resources.Load ("Llorona 1") as AudioClip;
				voiceDialogue [1] = Resources.Load ("Joe 5.1") as AudioClip;
				voiceDialogue [2] = Resources.Load ("Llorona 2") as AudioClip;
				voiceDialogue [3] = Resources.Load ("Joe 5.2") as AudioClip;
				voiceDialogue [4] = Resources.Load ("Llorona 3") as AudioClip;
				voiceDialogue [5] = Resources.Load ("Joe 5.3") as AudioClip;
				voiceDialogue [6] = Resources.Load ("Llorona 4") as AudioClip;
			}

			if (GameController.isActiveBossDead) {
			//	voiceDialogue [0] = Resources.Load ("ave 5") as AudioClip;
			} 
		}
	}

	void textDialogueLvl(){
		
		if (GameController.lvl == 1){
			if (textDialogue == null){
				textDialogue = Resources.Load ("Inicio1") as TextAsset;	
			}

		}
		if (GameController.lvl == 2){
			if (textDialogue == null){
				textDialogue = Resources.Load ("Inicio2") as TextAsset;
				asignarDialogo ();
			}
			voiceStart();
			firstLine();
		}
		if (GameController.lvl == 3){
			if (textDialogue == null){
				textDialogue = Resources.Load ("Inicio3") as TextAsset;
				asignarDialogo ();
			}
			voiceStart();
			firstLine();
		}
		if (GameController.lvl == 4){
			if (textDialogue == null){
				textDialogue = Resources.Load ("Inicio4") as TextAsset;
				asignarDialogo ();
			}
			voiceStart();
			firstLine();
		}
		if (GameController.lvl == 5){
			if (textDialogue == null){
				textDialogue = Resources.Load ("Inicio5") as TextAsset;
				asignarDialogo ();
			}
			voiceStart();
			firstLine();
		}
	}

	void textDialogueBoss ()
	{
		if (GameController.lvl == 1) {
			if (GameController.isActiveBoss && GameController.ActivarDialogo) {
				if (textDialogue == null) {
					textDialogue = Resources.Load ("Mechitas") as TextAsset;
					asignarDialogo ();
				}
				voiceEnemy ();
				firstLine ();
				GameController.ActiveInstantiate = false;
                GameController.ActivarDialogo = false;
			}
		}

		if (GameController.lvl == 2){
			if (GameController.isActiveBoss && GameController.ActivarDialogo) {
				if (textDialogue == null) {
					textDialogue = Resources.Load ("Madreagua") as TextAsset;
					asignarDialogo ();
				}
				voiceEnemy ();
				firstLine ();
				GameController.ActiveInstantiate = false;
				GameController.ActivarDialogo = false;
			}
		}

		if (GameController.lvl == 3){
			if (GameController.isActiveBoss && GameController.ActivarDialogo) {
				if (textDialogue == null) {
					textDialogue = Resources.Load ("Montesito") as TextAsset;
					asignarDialogo ();
				}
				voiceEnemy ();
				firstLine ();
				GameController.ActiveInstantiate = false;
				GameController.ActivarDialogo = false;
			}
		}

		if (GameController.lvl == 4){
			if (GameController.isActiveBoss && GameController.ActivarDialogo) {
				if (textDialogue == null) {
					textDialogue = Resources.Load ("Ave") as TextAsset;
					asignarDialogo ();
				}
				voiceEnemy ();
				firstLine ();
				GameController.ActiveInstantiate = false;
				GameController.ActivarDialogo = false;
			}
		}

		if (GameController.lvl == 5){
			if (GameController.isActiveBoss && GameController.ActivarDialogo) {
				if (textDialogue == null) {
					textDialogue = Resources.Load ("Llorona") as TextAsset;
					asignarDialogo ();
				}
				voiceEnemy ();
				firstLine ();
				GameController.ActiveInstantiate = false;
				GameController.ActivarDialogo = false;
			}
		}
	}

	void textDialogueMuerte()
	{
		if (GameController.lvl == 1) {
			if (GameController.isActiveBossDead) {				
				if (textDialogue == null) {
					Debug.Log("olaoidjaoisud");
					textDialogue = Resources.Load ("Candileja muerte") as TextAsset;
					asignarDialogo ();
				}
				voiceEnemy ();
				firstLine ();
				GameController.isActiveBossDead = false;
				GameController.ActiveInstantiate = false;
			}
		}

		if (GameController.lvl == 2) {
			if (GameController.isActiveBossDead) {				
				if (textDialogue == null) {
					textDialogue = Resources.Load ("Madreagua muerte") as TextAsset;
					asignarDialogo ();
				}
				voiceEnemy ();
				firstLine ();
				GameController.isActiveBossDead = false;
				GameController.ActiveInstantiate = false;
			}
		}

		if (GameController.lvl == 3) {
			if (GameController.isActiveBossDead) {				
				if (textDialogue == null) {
					textDialogue = Resources.Load ("Madremonte muerte") as TextAsset;
					asignarDialogo ();
				}
				voiceEnemy ();
				firstLine ();
				GameController.isActiveBossDead = false;
				GameController.ActiveInstantiate = false;
			}
		}

		if (GameController.lvl == 4) {
			if (GameController.isActiveBossDead) {				
				if (textDialogue == null) {
					textDialogue = Resources.Load ("Ave muerte") as TextAsset;
					asignarDialogo ();
				}
				voiceEnemy ();
				firstLine ();
				GameController.isActiveBossDead = false;
				GameController.ActiveInstantiate = false;
			}
		}

		if (GameController.lvl == 5) {
			if (GameController.isActiveBossDead) {				
				if (textDialogue == null) {
					textDialogue = Resources.Load ("Llorona muerte") as TextAsset;
					asignarDialogo ();
				}
				voiceEnemy ();
				firstLine ();
				GameController.isActiveBossDead = false;
				GameController.ActiveInstantiate = false;
			}
		}
	}
}