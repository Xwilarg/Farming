public class ShopPower : AItemPower
{
    public override bool CanBeInteractedWith()
        => true;

    public override void Interact()
    {
        UIManager.uiManager.EnableShop();
    }
}