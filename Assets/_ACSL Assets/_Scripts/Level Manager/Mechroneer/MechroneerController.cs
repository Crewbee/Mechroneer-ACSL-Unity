using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechroneerController : Controller
{
    public override void PossessPlayer(Controller.IActions actions)
    {
        if (actions == null)
            base.PossessPlayer(null);
        else if (!(actions is IActions))
            base.PossessPlayer(null);
        else
        {
            base.PossessPlayer(actions);
        } 
    }

    protected override void FixedUpdateController()
    {
        if (!m_cachedGameObject)
        {
            PossessPlayer(null);
            return;
        }
    }

    protected override void UpdateController()
    {
        if (!m_cachedGameObject)
        {
            PossessPlayer(null);
            return;
        }

        IActions player = possessedPlayer as IActions;
        player.RotateCamera(new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")));

        player.ZoomCamera(Input.GetAxisRaw("Mouse ScrollWheel"));

        if (Input.GetMouseButton(0))
            player.MovePlayer(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Escape))
            player.PauseGame();

        if (Input.GetKeyDown(KeyCode.Tab))
            player.ChangePerspective();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            player.SelectAbility1();

        if (Input.GetKeyDown(KeyCode.Alpha2))
            player.SelectAbility2();

        if (Input.GetKeyDown(KeyCode.Alpha3))
            player.SelectAbility3();

        if (Input.GetKeyDown(KeyCode.Alpha4))
            player.SelectAbility4();
    }

    new public interface IActions : Controller.IActions
    {
        void ZoomCamera(float input);
        void RotateCamera(Vector2 input);
        void ChangePerspective();

        void MovePlayer(Vector3 mousePos);

        void SelectAbility1();
        void SelectAbility2();
        void SelectAbility3();
        void SelectAbility4();

        void PauseGame();

    }
}