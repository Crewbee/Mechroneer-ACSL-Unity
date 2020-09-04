using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mechroneer/GameModeData")]
public class MechroneerGameModeData : ScriptableObject
{
    public MechroneerGameMode gameMode;
    public MechroneerGameState gameState;
    public MechroneerUI UI;
    public Robot robot;
    public MechroneerController controller;
    public MechroneerController aiController;
}
