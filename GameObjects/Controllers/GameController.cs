using System;
using System.Collections.Generic;
using System.Linq;

namespace GameObjects
{
    public class GameController
    {
        private Map _map;
        private int _gameState;
        private RuleCollection _activeRules;

        public RuleCollection ActiveRules
        {
            get
            {
                RuleCollection collection = new RuleCollection();
                foreach(SingleRule rule in _activeRules)
                {
                    collection.Add(rule);
                }
                return collection;
            }

            set
            {
                RuleCollection collection = new RuleCollection();
                foreach (SingleRule rule in value)
                {
                    collection.Add(rule);
                }
                _activeRules = collection;
                _gameState = 0;
            }
        }

        public Map Map
        {
            get
            {
                Map newMap = new Map();
                foreach(LivingCell cell in _map.LivingCells.Values)
                {
                    newMap.AddCellAtCoordinate(cell.Coordinate);
                }
                return newMap;
            }
            set
            {
                Map newMap = new Map();
                foreach (LivingCell cell in value.LivingCells.Values)
                {
                    newMap.AddCellAtCoordinate(cell.Coordinate);
                }
                _map = newMap;
                _gameState = 0;
            }
        }

        public int GameState
        {
            get { return _gameState; }
        }

        public GameController() : this (new Map(), new RuleCollection()) { }
        
        public GameController(Map presetGrid, RuleCollection presetRules)
        {
            Map = presetGrid;
            ActiveRules = presetRules;
        }

        public bool ProgressState()
        {
            bool success = ParseRules();
            if(success) _gameState++;
            return success;
        }

        bool ParseRules()
        {

            var RollBack = new List<LivingCell>(_map.LivingCells.Values);
            try
            {
                if (_activeRules.Count > 0)
                {
                    var rawCellsToSpawn = new List<Coordinate>();
                    var CellsToRemove = new List<LivingCell>(_map.LivingCells.Values);

                    foreach (SingleRule rule in _activeRules)
                    {
                        switch (rule.RuleType)
                        {
                            case SpawnOrSurviveRule.SpawnRule:
                                rawCellsToSpawn.AddRange(ParseSpawnRule(rule.Number));
                                break;
                            case SpawnOrSurviveRule.SurviveRule:
                                CellsToRemove = ParseSurviveRule(rule.Number, CellsToRemove);
                                break;
                        }
                    }

                    var cellsToSpawn = rawCellsToSpawn.Distinct().ToList();
                    foreach (Coordinate coordinate in cellsToSpawn)
                    {
                        _map.AddCellAtCoordinate(coordinate);
                    }
                    foreach (LivingCell cell in CellsToRemove)
                    {
                        _map.RemoveCell(cell);
                    }
                }
                return true;
            }
            catch
            {
                _map = new Map();
                foreach (LivingCell cell in RollBack) {
                    _map.AddCellAtCoordinate(cell.Coordinate);
                }
                return false;
            }
        }
        
        var ParseSpawnRule(int ruleNumber)
        {
            if (ruleNumber < 1 || ruleNumber > 8 )
            {
                throw new ArgumentOutOfRangeException();
            }
            var rawCellsToSpawn = new List<Coordinate>();         
            foreach (LivingCell cell in _map.LivingCells.Values)                                         
            {
                foreach(Coordinate candidate in cell.Coordinate.SurroundingCoordinates())
                {
                    int surroundingNumber = candidate.NumberOfSurroundingLivingCells(_map);

                    bool candidateIsLivingCell = _map.FindCellAtCoordinate(candidate) != null;
                    if (surroundingNumber == ruleNumber && !candidateIsLivingCell)      //If the number matches, but also if the candidate doesn't exist already
                    {
                        rawCellsToSpawn.Add(candidate);                                     //we desire to only check the neighbours of the living cells from this state,
                    }                                                                       //not the neighbors of living cells being added in the foreach loop, hence the new list
                }
            }

            return rawCellsToSpawn;
        }


        List<LivingCell> ParseSurviveRule(int ruleNumber, List<LivingCell> removalCandidates)
        {
            if (ruleNumber < 0 || ruleNumber > 8)
            {
                throw new ArgumentOutOfRangeException();
            }
            var CellsToRemove = new List<LivingCell>(removalCandidates);
            foreach (LivingCell candidate in _map.LivingCells.Values)
            {
                int surroundingNumber = candidate.Coordinate.NumberOfSurroundingLivingCells(_map);

                if(surroundingNumber == ruleNumber)
                {
                    CellsToRemove.Remove(candidate);
                }
            }

            return CellsToRemove;
        }
    }
}
