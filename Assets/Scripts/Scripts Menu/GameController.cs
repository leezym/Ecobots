using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Globales {
	public class GameController : MonoBehaviour {

		//Principal
		public static GameController data;

		public Canvas HUD_canvas, gameLife, gameArma, Enemy_canvas, Main_canvas, gameOver_canvas;
		public Canvas gameLifeP, gameArmaP, gameMusicaP, hud_p, Pause_canvas;

		//Cargando
		public Canvas imagenCarga;
		public static bool activarCarga = false;
		public static bool cancelarActivarCarga = true;
		public Slider barraCarga;
		public static float valueC = 2;

		// Dialogos
		public AudioSource audioSourceDialogue;
		public static bool isActive = false;
		public bool isTyping = false;
		public bool cancelTyping = false;
		public static bool ActiveInputs = true;
		public static bool ActiveInstantiate = true;
		public static bool isActiveBoss = false;
		public static bool isActiveBossDead = false;
		public static bool zonaJefe = true;
        public static bool Mecanicas = false;
        public static bool ActivarDialogo = false;

		// HUD Jefes
		public GameObject iconoJefe;
		public Slider sliderHealthBoss;

		// Candileja
		public static int Fuego = 10;
		public static int EmbesCandi = 20;

		// Madre Agua
		public static int Burbujas = 10;
		public static int Burbujota = 20;

		// Madre Monte
		public static int Boom = 15;
		public static int Energia = 20;

		// Ave Oscura
		public static int EmbesPajaro = 20;
		public static int Rayos = 10;

		//Jefes
		public static bool muerto1=false;
		public static bool muerto2=false;
		public static bool muerto3=false;
		public static bool muerto4=false;
		public static bool muerto5=false;

		//Daños poderes Joe
		public static int DisparoBase = 5;
		public static int IceAndFire = 5;
		public static int ElectricShoot = 10;
		public static int Tornadito = 10;
		public static int EnergyBall = 15; 

		//Daño Enemigos
		public static int DañoCollision =10;
		public static int BulletEnemys =20;
		public static int CollisionArmadillo =25;
		public static int CollisionKamikaze = 30;

		//Guardar partida
		public static bool saved = false;

		//Vidas
		public GameObject[] vidasA;
		public static int vidas = 0;
		public Slider sliderHealth, sliderHealthP;
		public static float SaludMax = 100;

        // Musica
        public List<AudioSource> gameSounds;
		public AudioClip soundLvl;
		public AudioSource audioSourceTheme;
		public Toggle checkMusica;
		public Canvas check_canvas;
		public Toggle checkDialogo;
		public float volumenTheme;
		public float volumenDialogo;
		public Toggle checkEfecto;

		//Armas
		public GameObject[] itemsA;
		public Slider[] sliderA;
		public static int armas = 0;
		public static float MuniMax = 10;

		//Continuar # de level
		public static int lvl=0;

		//Checkpoint
		public GameObject[] checkpoint;
		public static float posX;
		public static float posY;
		public static float posZ;
		public static int cp = 0;
		public static bool actCP = false;

		//Armas Pausa
		public GameObject[] itemsPA;
		public Slider[] sliderPA;

		//Musica Pausa
		public Toggle checkMusicaP;
		public Canvas check_canvasP;
		public Toggle checkDialogoP;
		public Toggle checkEfectoP;

		//Vidas Pausa
		public GameObject[] vidasPA;

		void Awake(){
			if (data == null) {
				DontDestroyOnLoad (gameObject);
				data = this;
			}else 
				if(data != this){
					Destroy (gameObject);
				}
		}
	}
}