namespace GameStore.Web.ViewModels.LiqPay
{
    public class LiqPayCheckout
    {
        public int version { get; set; }
        public string public_key { get; set; }
        public string action { get; set; }
        public decimal amount { get; set; }
        public string currency { get; set; }
        public string description { get; set; }
        public string order_id { get; set; }

        public string expired_date { get; set; }
        public string language { get; set; }
        public string paytypes { get; set; }
        public string result_url { get; set; }
        public string server_url { get; set; }
        public string verifycode { get; set; }

        public string info { get; set; }

        public int sandbox { get; set; }
    }
}