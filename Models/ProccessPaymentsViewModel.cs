﻿namespace PROG_PART_2.Models
{
    public class ProcessPaymentsViewModel
    {
        public List<Claim> Claims { get; set; }
        public int TotalClaims { get; set; }
        public decimal TotalAmountToPay { get; set; }
        public int PendingPayments { get; set; }
    }
}
