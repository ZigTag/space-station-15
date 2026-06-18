using Content.Client.Light;
using Content.Shared._Test;
using Content.Shared.Clothing;
using Robust.Client.Graphics;

namespace Content.Client._Test;

/// <summary>
/// This handles...
/// </summary>
public sealed partial class NvgSystem : EntitySystem
{
    [Dependency] private IOverlayManager _overlayMan = default!;

    /// <inheritdoc/>
    public override void Initialize()
    {
        SubscribeLocalEvent<NvgComponent, ClothingGotEquippedEvent>(OnEquip);
        SubscribeLocalEvent<NvgComponent, ClothingGotUnequippedEvent>(OnUnequip);
    }

    private void OnEquip(Entity<NvgComponent> ent, ref ClothingGotEquippedEvent args)
    {
        _overlayMan.RemoveOverlay<BeforeLightTargetOverlay>();
        _overlayMan.RemoveOverlay<RoofOverlay>();
        _overlayMan.RemoveOverlay<TileEmissionOverlay>();
        _overlayMan.RemoveOverlay<LightBlurOverlay>();
        _overlayMan.RemoveOverlay<SunShadowOverlay>();
        _overlayMan.RemoveOverlay<AfterLightTargetOverlay>();
    }

    private void OnUnequip(Entity<NvgComponent> ent, ref ClothingGotUnequippedEvent args)
    {
        _overlayMan.AddOverlay(new BeforeLightTargetOverlay());
        _overlayMan.AddOverlay(new RoofOverlay(EntityManager));
        _overlayMan.AddOverlay(new TileEmissionOverlay(EntityManager));
        _overlayMan.AddOverlay(new LightBlurOverlay());
        _overlayMan.AddOverlay(new SunShadowOverlay());
        _overlayMan.AddOverlay(new AfterLightTargetOverlay());
    }
}
