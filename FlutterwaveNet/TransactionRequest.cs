using System;
using System.Collections.Generic;
using System.Text;

namespace FlutterwaveNet
{
    public class TransactRequest
    {
        public string tx_ref { get; set; }
        public decimal amount { get; set; }
        public string currency { get; set; }
        public string redirect_url { get; set; }
        public string payment_options { get; set; }
        public MetaR meta { get; set; }
        public CustomerR customer { get; set; }
        public Customizations customizations { get; set; }

    }
   

    public class CustomerR
    {
        public string email { get; set; }
        public string phonenumber { get; set; }
        public string name { get; set; }

    }

    public class Customizations
    {
        public string title { get; set; }
        public string description { get; set; }
        public string logo { get; set; }

    }

    public class MetaR
    {
        public int consumer_id { get; set; }
        public string consumer_mac { get; set; }
        public string ExtraField { get; set; }
        public string ExtraFieldTwo { get; set; }

    }

}
