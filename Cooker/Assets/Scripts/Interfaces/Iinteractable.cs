namespace Interfaces {
    public interface IInteractable {
        public abstract void Interact(Player player);
        public abstract void InteractAlt(Player player);
        public abstract void InteractAltHold(Player player);
        public abstract void InteractAltHoldCancel(Player player);
    }
}