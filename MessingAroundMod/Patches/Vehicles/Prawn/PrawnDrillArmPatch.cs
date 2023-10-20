using UnityEngine;
using HarmonyLib;

namespace VipereSolide.Subnautica.MessingAroundMod.Patches.Vehicles.Prawn
{
    using Items.Modules.Prawn;
    using Utility.Vehicles;

    [HarmonyPatch(typeof(ExosuitDrillArm))]
    internal class PrawnDrillArmPatch
    {
        public static readonly float BASE_DRILL_ARM_DAMAGE = 5F;

        public static float GetDamage(Exosuit exosuit)
        {
            return BASE_DRILL_ARM_DAMAGE * ExosuitDrillArmDamageModules.GetDamageMultiplier(exosuit);
        }

        [HarmonyPatch(nameof(ExosuitDrillArm.OnHit))]
        [HarmonyPrefix]
        public static bool OnHit_Prefix(ExosuitDrillArm __instance)
        {
            if (__instance.exosuit.CanPilot() && __instance.exosuit.GetPilotingMode())
            {
                __instance.drillTarget = null;

                Vector3 fpsPosition = Vector3.zero;
                GameObject fpsTarget = null;
                UWE.Utils.TraceFPSTargetPosition(__instance.exosuit.gameObject, 5f, ref fpsTarget, ref fpsPosition, true);

                if (fpsTarget == null)
                {
                    InteractionVolumeUser interactionVolumeUser = Player.main.gameObject.GetComponent<InteractionVolumeUser>();
                    if (interactionVolumeUser != null && interactionVolumeUser.GetMostRecent() != null)
                    {
                        fpsTarget = interactionVolumeUser.GetMostRecent().gameObject;
                    }
                }

                if (fpsTarget && __instance.drilling)
                {
                    Drillable drillable = fpsTarget.FindAncestor<Drillable>();
                    __instance.loopHit.Play();

                    if (!drillable)
                    {
                        LiveMixin liveMixin = fpsTarget.FindAncestor<LiveMixin>();

                        if (liveMixin)
                        {
                            liveMixin.IsAlive();
                            liveMixin.TakeDamage(GetDamage(__instance.exosuit), fpsPosition, DamageType.Drill, null);
                            __instance.drillTarget = fpsTarget;
                        }

                        VFXSurface vfxSurface = fpsTarget.GetComponent<VFXSurface>();

                        if (__instance.drillFXinstance == null)
                        {
                            __instance.drillFXinstance = VFXSurfaceTypeManager.main.Play(vfxSurface, __instance.vfxEventType, __instance.fxSpawnPoint.position, __instance.fxSpawnPoint.rotation, __instance.fxSpawnPoint);
                        }
                        else if (vfxSurface != null && __instance.prevSurfaceType != vfxSurface.surfaceType)
                        {
                            __instance.drillFXinstance.GetComponent<VFXLateTimeParticles>().Stop();
                            Object.Destroy(__instance.drillFXinstance.gameObject, 1.6f);

                            __instance.drillFXinstance = VFXSurfaceTypeManager.main.Play(vfxSurface, __instance.vfxEventType, __instance.fxSpawnPoint.position, __instance.fxSpawnPoint.rotation, __instance.fxSpawnPoint);
                            __instance.prevSurfaceType = vfxSurface.surfaceType;
                        }

                        fpsTarget.SendMessage("BashHit", __instance, SendMessageOptions.DontRequireReceiver);
                        return false;
                    }

                    drillable.OnDrill(__instance.fxSpawnPoint.position, __instance.exosuit, out GameObject hitDrilledObject);
                    __instance.drillTarget = hitDrilledObject;

                    if (__instance.fxControl.emitters[0].fxPS != null && !__instance.fxControl.emitters[0].fxPS.emission.enabled)
                    {
                        __instance.fxControl.Play(0);
                        return false;
                    }
                }
                else
                {
                    __instance.StopEffects();
                }
            }

            return false;
        }
    }
}
