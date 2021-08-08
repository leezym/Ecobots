using UnityEngine;
using System.Collections;
using Globales;

public class MusicEnabled : MonoBehaviour {

	public void enabledAudioPrincipal (){
		// Si la musica esta activa, la desactivo.
		if (GameController.data.checkMusica.isOn == false) {
			GameController.data.audioSourceTheme.Stop ();
		} 
		else
		// Si la musica esta desactiva, la activo.
		if (GameController.data.checkMusica.isOn == true){
			GameController.data.audioSourceTheme.Play ();
		}
	}

	public void enabledDialogoPrincipal (){
		// Si la musica esta activa, la desactivo.
		if (GameController.data.checkDialogo.isOn == false) {
			GameController.data.audioSourceDialogue.Stop ();
		} 
		else
		// Si la musica esta desactiva, la activo.
			if (GameController.data.checkDialogo.isOn == true){
				GameController.data.audioSourceDialogue.Play ();
		}
	}

	public void enabledAudioPausa (){
		// Si la musica esta activa, la desactivo.
		if (!GameController.data.checkMusicaP.isOn) {
			GameController.data.checkMusica.isOn = false;
		}
		else
			// Si la musica esta desactiva, la activo.
		if (GameController.data.checkMusicaP.isOn) {
			GameController.data.checkMusica.isOn = true;
		}
	}

	public void enabledDialogoPausa (){
		// Si la musica esta activa, la desactivo.
		if (!GameController.data.checkDialogoP.isOn) {
			GameController.data.checkDialogo.isOn = false;
		}
		else
			// Si la musica esta desactiva, la activo.
			if (GameController.data.checkDialogoP.isOn) {
				GameController.data.checkDialogo.isOn = true;
			}
	}
}