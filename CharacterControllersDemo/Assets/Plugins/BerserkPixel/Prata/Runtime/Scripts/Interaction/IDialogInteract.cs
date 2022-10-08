namespace BerserkPixel.Prata
{
    public interface IDialogInteract
    {
        void ReadyForInteraction(Interaction newInteraction);

        void CancelInteraction();

        void Interact();
    }
}