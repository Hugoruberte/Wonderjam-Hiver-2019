

namespace Interactive.Engine
{
	public class FoodChainEngine : InteractiveExtensionEngine
	{
		public override void InteractionBetween(InteractiveEntity main, InteractiveEntity other)
		{
			if(main is IFoodChainEntity m && other is IFoodChainEntity o) {

				// if my rank > other rank -> eat it : add its food chain value to my life
				// if my rank < other rank -> get eat : remove its food chain value from my life
				if(m.foodChainRank > o.foodChainRank) {
					main.life += o.foodChainValue;
				} else if(m.foodChainRank < o.foodChainRank) {
					main.life -= o.foodChainValue;
				}
			}
		}
	}
}

