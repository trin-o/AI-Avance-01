using UnityEngine;

public class GameController : MonoBehaviour
{
    static public GameController GC;
    public GAME_STATE state;

    void Awake()
    {
        GC = this;
    }

    void Start()
    {
        state = GAME_STATE.JUGANDO; //Cambiar a INICIO una vez que se incluya algun tipo de introduccion idk
    }
}

public enum GAME_STATE { INICIO, JUGANDO, PAUSA, FIN }
public enum PLAYER_STATE { CONTROLANDO, INACTIVO, MUERTO }
public enum ENEMY_STATE { APUNTANDO, NAVEGANDO, MUERTO }
