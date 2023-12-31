using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputMang
{

    private static Controls _Controls;

    


    public static void Init(Player myPlayer)

    {

        _Controls = new Controls();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        int points = 0;

        _Controls.Game.Movement.performed += ctx =>
        {
            myPlayer.SetMovementDirection(ctx.ReadValue<Vector3>());
        };

        _Controls.Game.Ability.started += _ => //Ability cast code, i actually understand this though
        {
            Debug.Log("You have casted Nova Bomb");
        };

        _Controls.Game.Jump.started += _ =>
        {
            myPlayer.Jump();
        };

        _Controls.Game.Look.performed += ctx =>
        {
            myPlayer.SetLookDirection(ctx.ReadValue<Vector2>());
        };

        _Controls.Game.Shoot.performed += ctx =>
        {
            myPlayer.Shoot();
        };

        _Controls.Game.Reload.performed += ctx =>
        {
            myPlayer.Reload();
        };
        _Controls.Game.Swap.performed += ctx =>
        {
            myPlayer.Swap();
        };
        _Controls.Game.Swap2.performed += ctx =>
        {
            myPlayer.Swap2();
        };

        _Controls.Perm.Enable();

    }


    public static void GameMode()
    {
        _Controls.Game.Enable();
        _Controls.UI.Enable();
    }


    public static void UIMode()
    {
        _Controls.Game.Disable();
        _Controls.UI.Disable();
    }

}

