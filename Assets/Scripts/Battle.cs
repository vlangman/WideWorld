using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkyNet
{
    public Ship _ship;
    public int _level;
    public float _levelModifier;
    public List<Move> _currentSet;
    public List<Move> _lastSet;

    public SkyNet(ref Ship ship, int level)
    {
        _ship = ship;
        _level = level;
        ApplyLevelModifier();
        _lastSet = new List<Move>();
        _currentSet = new List<Move>();

        GenerateCrewList();
        GenerateMoveList();
    }

    public void GenerateMoveList()
    {
        foreach (Crew _man in _ship._crew)
        {
            //gen list of moves per crew member
            ABILITIES move = (ABILITIES)Random.Range(0, 3);
            int wIndex = -1;
            if (move == ABILITIES.ATTACK)
            {
                wIndex = Random.Range(0, _ship._weapons.Count);
            }
            _currentSet.Add(new Move(move, wIndex));
        }
        _ship._turnMoves = _currentSet;
    }

    public void GenerateCrewList()
    {
        int crewCount = (int)(2.0f * _levelModifier);
        _ship._crew = new List<Crew>();
        for (int i = 0; i < crewCount; i++)
        {
            SPECIES species = (SPECIES)Random.Range(0, 3);
            _ship._crew.Add(new Crew(species));
        }
    }

    public void update(Battle _currentBattle)
    {
        if (_ship._health < _currentBattle._ship._health)
        {
            GenerateMoveList();
        }
    }

    public void ApplyLevelModifier()
    {
        if (_level < 5)
        {
            _levelModifier = 1.5f;
        }
        if (5 < _level && _level <= 10)
        {
            _levelModifier = 2.0f;
        }
        if (_level > 10)
        {
            _levelModifier = 2.5f;
        }
    }

}

public class Battle : ScriptableObject
{

    public Ship _enemy;

    public bool _nextTurn = false;
    public SkyNet _skyNet;
    public Ship _ship;

    bool _startBattle = false;
    bool _battleOver = false;
    bool _nextRound = false;
    int turnIndex = 0;

    public void StartBattle(Ship ship)
    {
        _ship = ship;
        _enemy = new Ship("ENEMY", 20.0f, 5.0f, 5.0f);
        _enemy.AddWeapon(new Weapon(WEAPONS_TYPE.CANNON, EFFECT_TYPE.NONE));
        _enemy.AddWeapon(new Weapon(WEAPONS_TYPE.CANNON, EFFECT_TYPE.NONE));

        // prepare skynet lists
        _skyNet = new SkyNet(ref _enemy, _ship._level);

        //set targets 
        _enemy._target = _ship;
        _ship._target = _enemy;
        _startBattle = true;
    }


    public void StartNextRound()
    {
        _nextRound = true;
    }

    // Start is called before the first frame update
    public void Awake()
    {

    }

    // Update is called once per frame
    public bool Update()
    {
        if (_startBattle && !_battleOver)
        {
            if (_nextRound)
            {
                int MaxTurnsPerRound = _ship._crew.Count > _enemy._crew.Count ? _ship._crew.Count - 1 : _enemy._crew.Count - 1;
                if (turnIndex <= MaxTurnsPerRound)
                {
                    //ship turn first (player)
                    Move shipMove;
                    if (turnIndex <= _ship._turnMoves.Count)
                    {
                        shipMove = _ship._turnMoves[turnIndex];
                    }
                    else
                    {
                        shipMove = new Move(ABILITIES.NONE, 0);
                    }
                    //enemy turn second (

                    Move enemyMove;
                    if (turnIndex <= _enemy._turnMoves.Count)
                    {
                        enemyMove = _enemy._turnMoves[turnIndex];
                    }
                    else
                    {
                        enemyMove = new Move(ABILITIES.NONE, 0);
                    }

                    _ship.ExecuteTurn(turnIndex);
                    _enemy.ExecuteTurn(turnIndex);

                    _ship.update();
                    _enemy.update();
                    _skyNet.update(this);
                    turnIndex++;
                }
                else
                {
                    turnIndex = 0;
                    _nextRound = false;
                }
               
            }
        }
        //when battle over 
        return _battleOver;

    }

}

   public class _battlePopupTimer: MonoBehaviour{
        float timer = 0.0f;
         

        public void UpdateTimer(){
            timer+= Time.deltaTime;
        }

        public void ResetTimer(){
            timer = 0.0f;
        }

        public bool CheckTimer(float time){
            if (timer <= time){
                return true;
            }
            return false;
        }
    }
