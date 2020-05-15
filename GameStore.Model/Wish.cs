namespace GameStore.Model
{
    public class Wish
    {
        public int WishId { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}