namespace Item
{
    public class Ingredient : Pickable
    {
        public float timeToCook;
        public int interactionToProcesses;
        private bool isProcessed = false;

        public bool IsProcessed() => isProcessed;

        public void SetProcessed(bool state = true)
        {
            isProcessed = state;
        }

        public override void TransferData(Pickable pick)
        {
           Ingredient aux = pick as Ingredient;
           isProcessed = aux.IsProcessed();
           timeToCook = aux.timeToCook;
           interactionToProcesses = aux.interactionToProcesses;
        }
    }
}