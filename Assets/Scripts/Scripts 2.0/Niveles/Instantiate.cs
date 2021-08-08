using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Globales;

public class Instantiate : MonoBehaviour {

	public GameObject player, PosPlayer;
	public GameObject CobraEnemy, ArmadilloEnemy, LavaBotEnemy;
	public GameObject PezGlobo, Mina;
	public GameObject RoboAve, BoaBot;
	public GameObject Mascara,NoRobot,Espectro;
	public GameObject Estalactita, RoboAracnido, Yeti;
   
	public GameObject[] PosCobra;
    public GameObject[] PosLavaBot;
	public GameObject[] PosArmadillo;
    
	public GameObject[] PosGlobo;
	public GameObject[] PosMina;

	public GameObject[] PosAve;
	public GameObject[] PosBoa;

	public GameObject[] PosMascara;
	public GameObject[] PosEspectro;
	public GameObject[] PosNoRobot;

	public GameObject[] PosEstalactita;
	public GameObject[] PosAracnido;
	public GameObject[] PosYeti;

	public MusicEnabled musicaPlay;

	void Start() {
		musicaPlay = GameObject.Find ("MusicEnabled").GetComponent<MusicEnabled>();
        GameController.ActiveInstantiate = true;

		if (GameController.lvl == 1) {
			GameController.data.soundLvl = Resources.Load("Cycles fondo") as AudioClip;
		}
		if (GameController.lvl == 2) {
			GameController.data.soundLvl = Resources.Load("CodeBlue fondo") as AudioClip;
		}
		if (GameController.lvl == 3){
			GameController.data.soundLvl = Resources.Load("ThoughtBot fondo") as AudioClip;
		}
		if (GameController.lvl == 4){
			GameController.data.soundLvl = Resources.Load("WhatsItToYaPunk fondo") as AudioClip;
		}
		if (GameController.lvl == 5){
			GameController.data.soundLvl = Resources.Load("Boom fondo") as AudioClip;
		}

		InstantiateChekcPoint();
		InstantiatePlayer ();
		InstantiateMusic ();

	}

	void Update(){
		if (GameController.ActiveInputs && GameController.ActiveInstantiate){

            if (GameController.lvl == 1){
                InstantiateLvl1();
            }
            if (GameController.lvl == 2) {
                InstantiateLvl2();
			}
			if (GameController.lvl == 3){
				InstantiateLvl3 ();
			}
			if (GameController.lvl == 4){
				InstantiateLvl4 ();
			}
			if (GameController.lvl == 5){
				InstantiateLvl5 ();
			}
		GameController.ActiveInstantiate = false;
		}
	}

	public IEnumerator RespawnPlayer()	{
		yield return new WaitForSeconds (2);	
		InstantiatePlayer ();
	}

	public void InstantiateMusicBoss (){
		if (GameController.lvl == 1) {
			GameController.data.soundLvl = Resources.Load("The Hunter boss") as AudioClip;
		}
		if (GameController.lvl == 2) {
			GameController.data.soundLvl = Resources.Load("Periscope boss") as AudioClip;
		}
		if (GameController.lvl == 3){
			GameController.data.soundLvl = Resources.Load("GreenDaze boss") as AudioClip;
		}
		if (GameController.lvl == 4){
			GameController.data.soundLvl = Resources.Load("Pentagram boss") as AudioClip;
		}
		if (GameController.lvl == 5){
			GameController.data.soundLvl = Resources.Load("RP-FightScene boss") as AudioClip;
		}
	}

	public void InstantiateMusic (){
		GameController.data.audioSourceTheme.clip = GameController.data.soundLvl;
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

	void InstantiateChekcPoint(){
		GameController.data.checkpoint = GameObject.FindGameObjectsWithTag("checkPoint");
	}

	void InstantiatePlayer (){
		//Jugador
		player = Resources.Load("Personaje") as GameObject;
		PosPlayer = GameObject.FindWithTag ("SpawnPlayer");

		// Instanciar Player
		if (GameController.actCP) {
			player = Instantiate (player, new Vector3(GameController.posX, GameController.posY, GameController.posZ), Quaternion.identity) as GameObject;
		} else {
			player = Instantiate (player, PosPlayer.transform.position, PosPlayer.transform.rotation) as GameObject;
		}
	}

	void InstantiateLvl1(){	
        // Enemigos
		CobraEnemy = Resources.Load("Cobra") as GameObject;
		ArmadilloEnemy = Resources.Load ("Armadillo") as GameObject;
		LavaBotEnemy = Resources.Load ("Lava") as GameObject;
		PosCobra = GameObject.FindGameObjectsWithTag ("SpawnCobra");
		PosArmadillo = GameObject.FindGameObjectsWithTag ("SpawnArmadillo");
		PosLavaBot = GameObject.FindGameObjectsWithTag ("SpawnLavaBots");

		//Instanciar enemigos	
		for( int i = 0; i < PosCobra.Length; i ++)
		{
			CobraEnemy = Instantiate(CobraEnemy,PosCobra[i].transform.position,Quaternion.identity) as GameObject;
		}

		for( int i = 0; i < PosArmadillo.Length; i ++)
		{
			ArmadilloEnemy = Instantiate(ArmadilloEnemy,PosArmadillo[i].transform.position,Quaternion.identity) as GameObject;
		}

		for( int i = 0; i < PosLavaBot.Length; i ++)
		{
			LavaBotEnemy = Instantiate(LavaBotEnemy,PosLavaBot[i].transform.position,Quaternion.identity) as GameObject;
		}
	}

    void InstantiateLvl2() 
    {
        PezGlobo = Resources.Load("PezGlobo") as GameObject;
        PosGlobo = GameObject.FindGameObjectsWithTag("SpawnGlobo");
		Mina = Resources.Load ("Mina") as GameObject;
		PosMina =  GameObject.FindGameObjectsWithTag("SpawnMina");

        //Instanciar enemigos	
        for (int i = 0; i < PosGlobo.Length; i++)
        {
            PezGlobo = Instantiate(PezGlobo, PosGlobo[i].transform.position, Quaternion.identity) as GameObject;
        }

		for (int i = 0; i < PosMina.Length; i++)
		{
			Mina = Instantiate(Mina, PosMina[i].transform.position, Quaternion.identity) as GameObject;
		}
    }
       

	void InstantiateLvl3()
	{
		RoboAve = Resources.Load ("RoboAve") as GameObject;
		PosAve = GameObject.FindGameObjectsWithTag("SpawnAve");
		BoaBot = Resources.Load ("BoaBot") as GameObject; 
		PosBoa = GameObject.FindGameObjectsWithTag("SpawnBoa");

		for (int i = 0; i < PosAve.Length; i++)
		{
			RoboAve = Instantiate(RoboAve, PosAve[i].transform.position, Quaternion.identity) as GameObject;
		}

		for (int i = 0; i < PosBoa.Length; i++)
		{
			BoaBot = Instantiate(BoaBot, PosBoa[i].transform.position, Quaternion.identity) as GameObject;
		}
	}

	void InstantiateLvl4()
	{
		Espectro = Resources.Load ("Espectro") as GameObject;
		NoRobot = Resources.Load ("NoRobot") as GameObject;
		Mascara = Resources.Load ("Mascara") as GameObject;

		PosMascara = GameObject.FindGameObjectsWithTag ("SpawnMascara");
		PosNoRobot = GameObject.FindGameObjectsWithTag ("SpawnNoRobot");
		PosEspectro = GameObject.FindGameObjectsWithTag ("Espectro");

		for( int i = 0; i < PosMascara.Length; i ++)
		{
			Mascara= Instantiate(Mascara,PosMascara[i].transform.position,Quaternion.identity) as GameObject;
		}

		for( int i = 0; i < PosNoRobot.Length; i ++)
		{
			NoRobot = Instantiate(NoRobot,PosNoRobot[i].transform.position,Quaternion.identity) as GameObject;
		}

		for( int i = 0; i < PosEspectro.Length; i ++)
		{
			Espectro = Instantiate(Espectro,PosEspectro[i].transform.position,Quaternion.identity) as GameObject;
		}
	}

	void InstantiateLvl5()
	{
		Estalactita = Resources.Load ("Estalastita") as GameObject;	
		RoboAracnido = Resources.Load ("RoboAraña") as GameObject;
		Yeti = Resources.Load ("Yeti") as GameObject;

		PosEstalactita = GameObject.FindGameObjectsWithTag ("SpawnEstalactita");
		PosAracnido = GameObject.FindGameObjectsWithTag ("SpawnAraña");
		PosYeti = GameObject.FindGameObjectsWithTag ("SpawnYeti");

		for(int i = 0; i < PosEstalactita.Length; i ++)
		{
			Estalactita = Instantiate (Estalactita,PosEstalactita[i].transform.position,Quaternion.identity) as GameObject;
		}

		for(int i = 0; i < PosAracnido.Length; i ++)
		{
			RoboAracnido = Instantiate (RoboAracnido,PosAracnido[i].transform.position,Quaternion.identity) as GameObject;
		}

		for(int i = 0; i < PosYeti.Length; i ++)
		{
			Yeti = Instantiate (Yeti,PosYeti[i].transform.position,Quaternion.identity) as GameObject;
		}
	}
}