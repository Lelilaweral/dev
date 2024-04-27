using System.Collections.Generic;
using UnityEngine;

namespace Plugin.Classes
{
    public class ExampleClass : Akequ.Base.PlayerClass
    {
        public override void Init()
        {
            player.InitHealth(100, new Color(1f, 0f, 0f, 1f));
            if (player.isLocalPlayer)
            {
                player.PlayBellSound(1);
                UIManager.SetMobileButtons(new List<string>() { "Move", "Rotate", "Pause", "PlayerList", "Interact", "Jump", "Run",
                    "Inventory", "Voice" });
                TransitionManager.ShowClass("#FF8E00", "Test Class Name", "Test Class Decription");
                player.SetSpeed(3.25f, 8.5f);
                player.SetJumpPower(2.5f);
                player.SetFootsteps(ResourcesManager.GetClips("Step1", "Step2", "Step3", "Step4", "Step5", "Step6",
                    "Step7", "Step8"));
                PlayerUtilities.SetVoiceChat("3D", "", false);
            }
            else
            {
                playerModel = GameObject.Instantiate(ResourcesManager.GetObject("ply_classD")) as GameObject;
                playerModel.transform.parent = player.transform;
                playerModel.transform.localPosition = new Vector3(0f, -1.075f, 0f);
                playerModel.transform.localRotation = Quaternion.identity;
                playerModel.transform.localScale = new Vector3(1.45f, 1.45f, 1.45f);
                PlayerUtilities.SpawnHitboxes(player, playerModel);
            }

            if (player.isServer)
            {
                if (player.isClient)
                {
                    Transform[] points = player.GetSpawnPoints("Zone1", "classDSpawn");
                    Vector3 point = points[Random.Range(0, points.Length)].position;
                    player.Teleport(new Vector3(point.x, point.y + 1.25f, point.z));
                }
                else
                {
                    GameObject[] points = GameObject.FindGameObjectsWithTag("classDSpawn");
                    Vector3 point = points[Random.Range(0, points.Length)].transform.position;
                    player.Teleport(new Vector3(point.x, point.y + 1.25f, point.z));
                }
                player.SetSpeed(3.25f, 8.5f);
            }
        }

        public override void OnStop()
        {
            if (playerModel != null)
            {
                PlayerUtilities.SpawnRagdoll(player, playerModel);
                GameObject.Destroy(playerModel);
            }
            else
            {
                PlayerUtilities.SpawnRagdoll(player, "ply_classD_ragdoll").transform.localScale =
                    new Vector3(1.45f, 1.45f, 1.45f);
            }
        }

        public override string GetHand()
        {
            return "ClassDHand";
        }

        public override bool OnOpenInventory()
        {
            return true;
        }

        public override string GetName()
        {
            return "Test Class Name";
        }

        public override string GetTeamID()
        {
            return "ClassD";
        }

        public override string GetClassColor()
        {
            return "FF8E00";
        }
    }
}