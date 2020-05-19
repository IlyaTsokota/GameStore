using GameStore.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace GameStore.Web.ViewModels.LiqPay
{
    public class LiqPayHelper
    {
        private const string PrivateKey = "sandbox_Vl9gqjSkureXb385ow6swTUlKdPF8nFuMLtktZIv";
        private const string PublicKey = "sandbox_i31917086285";

        public static LiqPayCheckoutFormModel GetLiqPayModel(Order order, string returnUrl)
        {
            // if it's localhost returnUrl doesn't work
            if (!returnUrl.StartsWith("http"))
            {
                returnUrl = "http://" + returnUrl;
            }

            var amount = order.OrderDetails.Sum(x => x.Price * x.Quantity);
            var signatureSource = new LiqPayCheckout
            {
                public_key = PublicKey,
                version = 3,
                action = "pay",
                amount = amount,
                currency = "UAH",
                description = "Оплата заказа №" + order.OrderId,
                order_id = order.OrderId.ToString(),
                sandbox = 1,
                result_url = returnUrl
            };

            var jsonString = JsonConvert.SerializeObject(signatureSource);
            var dataHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonString));
            var signatureHash = GetLiqPaySignature(dataHash);

            var model = new LiqPayCheckoutFormModel { Data = dataHash, Signature = signatureHash };
            return model;
        }

        public static string GetLiqPaySignature(string data)
        {
            return Convert.ToBase64String(
                SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(PrivateKey + data + PrivateKey)));
        }
    }
}